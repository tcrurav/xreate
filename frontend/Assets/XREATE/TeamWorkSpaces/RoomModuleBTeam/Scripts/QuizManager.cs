using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private RoomModuleBGameController roomModuleBGameController;

    void Start()
    {
        // Initialize the points service
        achievementItemService = gameObject.AddComponent<AchievementItemService>();
        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

        roomModuleBGameController = roomModuleBGameManager.GetComponent<RoomModuleBGameController>();

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
            int index = i;
            welcomePanels[i].SetActive(true);
            startButtons[i].onClick.AddListener(() =>
            {
                roomModuleBGameManager.ChangeStartedPanelsServerRpc(index, true);
                //OnPlayerReady(index); // TODO - Remove this line - Original from Gabriel
            });
        }
    }

    // Method to submit points to the API
    private IEnumerator OnQuizCompletedUpdatePoints(int points)
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            //throw new System.Exception("Error: Only students get points");
            Debug.Log("Only students get points");
            yield break;
        }

        int studentId = MainManager.GetUser().id;
        int activityId = CurrentActivityManager.GetCurrentActivityId();

        yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
             challengeName, challengeItemItem, studentId, activityId, points);

        if (achievementItemService.responseCode != 200)
        {
            Debug.Log("Error: API Error");
            yield break;
        }

        roomModuleBGameManager.ChangeEnableStartReadyToNextRoomServerRpc(true);
    }

    // Method called when a player presses the start button
    public void OnPlayerReady(int playerIndex)
    {
        Debug.Log("QuizManager - OnPlayerReady");
        playersReady++;
        if (playersReady == totalPlayers)
        {
            Invoke("StartQuiz", 1.5f);
        }
    }

    // Starts the quiz game
    void StartQuiz()
    {
        Debug.Log($"QuizManager - StartQuiz - totalPlayers: {totalPlayers}");
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
            Debug.LogError("Data not received or the array is empty.");
        }
    }


    // Select 10 random questions
    void SelectRandomQuestions()
    {
        List<Question> tempQuestions = new List<Question>(questions);
        for (int i = 0; i < 10 && tempQuestions.Count > 0; i++)
        {
            int randomIndex = 0; // if NOT_RANDOM then always take the first one of the list that is still there.
            if (GameMode == "RANDOM")
            {
                randomIndex = UnityEngine.Random.Range(0, tempQuestions.Count);
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

        // Create the mixed response list
        List<int> answers = new List<int>();

        if (GameMode == "RANDOM")
        {
            // Mix up the answers and assign them to the panels
            List<int> numbers = new List<int> { 0, 1, 2, 3, 4 };

            // Remove the index of the correct answer
            numbers.Remove(currentQuestion.correctAnswerIndex);



            System.Random random = new System.Random(); // Random Number Generator

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
        }
        else
        {
            // The answers will have the same order as read from DB, unless the correct answer is out of range.
            // In that case the correct answer will be the first if question number is even, and the las if odd.
            // This solution is to avoid sending random values to all clients which is a waste of network bandwidth
            for (int i = 0; i < totalPlayers; i++)
            {
                answers.Add(i);
            }
            if (currentQuestion.correctAnswerIndex > (totalPlayers - 1))
            {
                if (currentQuestionIndex % 2 == 0)
                {
                    answers[0] = currentQuestion.correctAnswerIndex;
                }
                else
                {
                    answers[totalPlayers - 1] = currentQuestion.correctAnswerIndex;
                }
            }
        }

        // Assign responses to text panels
        for (int i = 0; i < totalPlayers; i++)
        {
            feedbackTexts[i].text = currentQuestion.answers[answers[i]];
        }

        timer = 5.0f; // Reset the stopwatch
        isTimerRunning = true;

        Debug.Log($"QuizManager - LoadQuestion - Out onClick");
        // Clean and add listeners to buttons
        for (int i = 0; i < totalPlayers; i++)
        {
            int index = i;
            Debug.Log($"QuizManager - LoadQuestion - Before onClick - index: {index}");
            // Clear any old listeners
            answerButtons[i].onClick.RemoveAllListeners();
            // Add the current listener
            answerButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"QuizManager - LoadQuestion - onClick - currentQuestionIndex: {currentQuestionIndex}, index: {index}, answers[index]: {answers[index]}");
                roomModuleBGameManager.ChangePanelsAnsweredServerRpc(currentQuestionIndex, index, answers[index]);
                //CheckAnswer(index, answers[index]); // TODO - Remove this line - Original from Gabriel
            });
        }

    }

    // Method to check if the answer is correct
    public void CheckAnswer(int selectedIndex, int selectedAnswerIndex)
    {
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
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            if (MainManager.GetUser().role == "STUDENT" && roomModuleBGameController.GetStudentIdByPanelIndex(i) == MainManager.GetUser().id)
            {
                answerButtons[i].interactable = true;
                continue;
            }
            answerButtons[i].interactable = false;
        }
    }

    public void SetTotalPlayers(int value)
    {
        totalPlayers = value;
    }

}