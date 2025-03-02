using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;      // Question text
        public List<string> answers;     // Answer options
        public int correctAnswerIndex;   // Index of the correct answer
    }

    public RoomModuleBGameManager roomModuleBGameManager;

    public List<GameObject> welcomePanels;  // List of welcome panels
    public List<Button> startButtons;       // List of start buttons

    public List<GameObject> quizPanels;     // List of quiz panels
    public List<TMP_Text> questionTexts;    // List of question texts
    public List<TMP_Text> feedbackTexts;    // List of feedback texts
    public List<TMP_Text> timerTexts;       // List of timer texts
    public List<TMP_Text> playerScoreTexts; // List of player score texts
    public List<TMP_Text> teamScoreTexts;   // List of team score texts

    public List<Button> answerButtons;     // List of answer buttons

    public Sprite defaultSprite; // Default sprite

    public Sprite correctSprite;     // Sprite for correct answer
    public Sprite incorrectSprite;   // Sprite for incorrect answer
    public AudioClip correctSound;   // Sound for correct answer
    public AudioClip incorrectSound; // Sound for incorrect answer
    public AudioClip victorySound; // Sound for game over
    private AudioSource audioSource; // AudioSource component to play sounds

    private List<Question> questions = new List<Question>();         // List of questions
    private List<Question> selectedQuestions = new List<Question>(); // List of randomly selected questions
    private Question currentQuestion;                                // Current question
    private int currentQuestionIndex = 0;

    private int playersReady = 0; // Counter of ready players
    private int totalPlayers = 5; // Total number of players (configurable)

    private int teamScore = 0; // Team score
    private List<int> playerScores = new List<int>(); // Player scores

    private float timer = 5.0f; // Time to answer each question
    private bool isTimerRunning = false; // Indicator if the timer is active

    // Variables to submit points to the API
    public string challengeName; // Challenge name
    public string challengeItemItem; // Challenge item name

    private AchievementItemService achievementItemService; // Service to submit points
    public ActivityChallengeConfigItemService activityChallengeConfigItemService;

    public string GameMode;

    void Start()
    {
        // Initialize the points service
        achievementItemService = gameObject.AddComponent<AchievementItemService>();
        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

        // Initialize my AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Initialize scores for all players
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0);
        }

        // Hide all quiz and welcome panels at the start
        foreach (var panel in quizPanels)
        {
            panel.SetActive(false);
        }

        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        // Set up the number of players
        if (totalPlayers < 2)
        {
            Debug.LogError("The number of players must be at least 2.");
            return;
        }

        // Show panels and assign events to the start buttons
        for (int i = 0; i < totalPlayers; i++)
        {
            welcomePanels[i].SetActive(true);
            startButtons[i].onClick.AddListener(() => OnPlayerReady(i));
        }
    }

    // Method to submit points to the API
    private IEnumerator OnQuizCompletedUpdatePoints(int points)
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            throw new System.Exception("Error: Only students get points");
        }

        int studentId = MainManager.GetUser().id;
        int activityId = CurrentActivityManager.GetCurrentActivityId();

        yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
             challengeName, challengeItemItem, studentId, activityId, points);

        if (achievementItemService.responseCode != 200)
        {
            yield break;
        }

    }

    // Method called when a player presses the start button
    public void OnPlayerReady(int playerIndex)
    {
        roomModuleBGameManager.ChangeStartedPanelsServerRpc(playerIndex, true);

        playersReady++;
        if (playersReady == totalPlayers)
        {
            Invoke("StartQuiz", 1.5f);
        }
    }

    // Starts the quiz game
    void StartQuiz()
    {
        // Hide welcome panels and show quiz panels
        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        for (int i = 0; i < totalPlayers; i++)
        {
            quizPanels[i].SetActive(true);
        }

        StartCoroutine(InitializeQuestionsSelectRandomQuestionsAndLoadQuestion());
    }

    // Initialize questions and answers
    private IEnumerator InitializeQuestionsSelectRandomQuestionsAndLoadQuestion()
    {
        yield return StartCoroutine(GetAllById());
        SelectRandomQuestions(); // Select 10 random questions
        LoadQuestion();
    }

    private IEnumerator GetAllById()
    {
        yield return activityChallengeConfigItemService.GetAllById(CurrentActivityManager.GetCurrentActivityId());

        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
        {
            foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
            {
                ActivityChallengeConfigItemValue value = JsonUtility.FromJson<ActivityChallengeConfigItemValue>(item.value);

                List<string> answers = new();

                for (int i = 0; i < value.answers.Length; i++)
                {
                    answers.Add(value.answers[i]);
                }

                questions.Add(new Question
                {
                    questionText = item.item,
                    answers = answers,
                    correctAnswerIndex = value.correctAnswerIndex
                });

            }
        }
        else
        {
            Debug.LogError("No se recibieron datos o el array está vacío.");
        }
    }


    // Select 10 random questions
    void SelectRandomQuestions()
    {
        List<Question> tempQuestions = new List<Question>(questions);
        for (int i = 0; i < 10 && tempQuestions.Count > 0; i++)
        {
            int randomIndex = i;
            if (GameMode == "RANDOM")
            {
                randomIndex = Random.Range(0, tempQuestions.Count);
            }

            selectedQuestions.Add(tempQuestions[randomIndex]);
            tempQuestions.RemoveAt(randomIndex);
        }
    }

    // Upload a question and answers to the panels
    void LoadQuestion()
    {
        if (currentQuestionIndex >= selectedQuestions.Count)
        {
            foreach (var text in questionTexts)
            {
                text.text = "Game over. Team score: " + teamScore;
            }

            // Find the player with the highest score
            int maxScore = playerScores.Max();
            int bestPlayerIndex = playerScores.ToList().IndexOf(maxScore);

            for (int i = 0; i < feedbackTexts.Count; i++)
            {
                if (i == bestPlayerIndex)
                {
                    feedbackTexts[i].text = $"Congratulations! You’ve been the MVP, leading your team with the highest score.";
                }
                else
                {
                    feedbackTexts[i].text = ""; // Leave empty so only the mvp can see the message.
                }
            }

            audioSource.PlayOneShot(victorySound); // Play victory sound

            // Call the method to introduce points to the API
            StartCoroutine(OnQuizCompletedUpdatePoints(teamScore));

            DisableButtons();
            return;
        }

        foreach (var button in answerButtons)
        {
            button.image.sprite = defaultSprite; // Set my default sprite.
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];

        foreach (var feedback in feedbackTexts)
        {
            feedback.text = ""; // Clear feedback messages
        }

        EnableButtons();

        // Show the question
        foreach (var text in questionTexts)
        {
            text.text = currentQuestion.questionText;
        }

        // Mix up the answers and assign them to the panels
        List<int> numbers = new List<int> { 0, 1, 2, 3, 4 };
        System.Random random = new System.Random(); // Random Number Generator

        // Remove the index of the correct answer
        numbers.Remove(currentQuestion.correctAnswerIndex);

        // Create the mixed response list
        List<int> answers = new List<int>();

        // Mixing the rates of incorrect answers
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // Add incorrect answers
        for (int i = 0; i < totalPlayers - 1; i++)
        {
            answers.Add(numbers[i]);
        }

        // Add the correct answer
        answers.Add(currentQuestion.correctAnswerIndex);

        // Shuffle the final answers again
        answers = answers.OrderBy(x => random.Next()).ToList();

        // Assign responses to text panels
        for (int i = 0; i < totalPlayers; i++)
        {
            feedbackTexts[i].text = currentQuestion.answers[answers[i]];
        }

        timer = 5.0f; // Reset the stopwatch
        isTimerRunning = true;

        // Clean and add listeners to buttons
        for (int i = 0; i < totalPlayers; i++)
        {
            int index = i;
            // Clear any old listeners
            answerButtons[i].onClick.RemoveAllListeners();
            // Add the current listener
            answerButtons[i].onClick.AddListener(() => CheckAnswer(i, index, answers[index]));
        }

    }

    // Method to check if the answer is correct
    public void CheckAnswer(int playerIndex, int selectedIndex, int selectedAnswerIndex)
    {
        roomModuleBGameManager.ChangePanelsAnsweredServerRpc(selectedIndex, playerIndex);

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            DisableButtons();

            answerButtons[selectedIndex].image.sprite = correctSprite; // Change to green
            audioSource.PlayOneShot(correctSound); // Play correct sound
            answerButtons[selectedIndex].interactable = false;

            foreach (var feedback in feedbackTexts)
            {
                feedback.text = "Correct Answer!";
            }

            isTimerRunning = false; // Stop the stopwatch
            int points = timer > 0 ? 5 : 3; // 5 points if you responded on time, 3 if not
            teamScore += points; // Add points to the team

            foreach (var text in teamScoreTexts)
            {
                text.text = "Team Score: " + teamScore;
            }

            if (timer > 0) // Additional points for the player
            {
                playerScores[selectedIndex] += 2;
                playerScoreTexts[selectedIndex].text = "Player Score: " + playerScores[selectedIndex];
            }

            currentQuestionIndex++;
            Debug.Log("Current Question Index: " + currentQuestionIndex);
            Invoke("LoadQuestion", 2.0f);
        }
        else
        {
            answerButtons[selectedIndex].image.sprite = incorrectSprite; // Change to red
            audioSource.PlayOneShot(incorrectSound); // Play wrong sound
            feedbackTexts[selectedIndex].text = "Incorrect Answer. Please try again.";
            answerButtons[selectedIndex].interactable = false;
            if (teamScore > 0)
            {
                teamScore--; // Subtract points from the team if the answer is wrong
            }
        }
    }

    // Method to Update the Stopwatch
    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false; // Stop the stopwatch
            }

            for (int i = 0; i < totalPlayers; i++)
            {
                timerTexts[i].text = "Tiempo: " + Mathf.Ceil(timer);
            }
        }
    }

    void DisableButtons()
    {
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
    }

    void EnableButtons()
    {
        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }
    }

    public void SetTotalPlayers(int value)
    {
        totalPlayers = value;
    }

}