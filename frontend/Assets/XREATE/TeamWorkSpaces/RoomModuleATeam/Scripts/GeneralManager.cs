using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public List<TMP_Text> playerScoreTexts; // List of player score texts
    public List<TMP_Text> teamScoreTexts;   // List of team score texts

    private int playersReady = 0; // Counter of ready players
    private int totalPlayers = 5; // Total number of players (configurable)

    private int teamScore = 0; // Team score
    private List<int> playerScores = new List<int>(); // Player scores

    // Variables to submit points to the API
    public string challengeName; // Challenge name
    public string challengeItemItem; // Challenge item name

    private AchievementItemService achievementItemService; // Service to submit points
    public ActivityChallengeConfigItemService activityChallengeConfigItemService;

    void Start()
    {

        achievementItemService = gameObject.AddComponent<AchievementItemService>();
        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

        // Initialize scores for all players
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0);
        }

        // Set up the number of players
        if (totalPlayers < 1)
        {
            Debug.LogError("The number of players must be at least 1.");
            return;
        }

        for (int i = 0; i < totalPlayers; i++)
        {
            //startButtons[i].onClick.AddListener(OnPlayerReady);
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
    void OnPlayerReady()
    {
        playersReady++;
        if (playersReady == totalPlayers)
        {
           // Invoke("StartQuiz", 1.5f);
        }
    }
    // Starts the quiz game
    void StartQuiz()
    {
        //// Hide welcome panels and show quiz panels
        //foreach (var panel in welcomePanels)
        //{
        //    panel.SetActive(false);
        //}

        //for (int i = 0; i < totalPlayers; i++)
        //{
        //    quizPanels[i].SetActive(true);
        //}

        //StartCoroutine(InitializeQuestionsSelectRandomQuestionsAndLoadQuestion());
    }

    // Initialize questions and answers
    //private IEnumerator InitializeQuestionsSelectRandomQuestionsAndLoadQuestion()
    //{
    //    //yield return StartCoroutine(GetAllById());
    //    //SelectRandomQuestions(); // Select 10 random questions
    //    //LoadQuestion();
    //}
    private IEnumerator GetAllById()
    {
        yield return activityChallengeConfigItemService.GetAllById(CurrentActivityManager.GetCurrentActivityId());

        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
        {
            //foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
            //{
            //    ActivityChallengeConfigItemValue value = JsonUtility.FromJson<ActivityChallengeConfigItemValue>(item.value);

            //    List<string> answers = new();

            //    for (int i = 0; i < value.answers.Length; i++)
            //    {
            //        answers.Add(value.answers[i]);
            //    }

            //    questions.Add(new Question
            //    {
            //        questionText = item.item,
            //        answers = answers,
            //        correctAnswerIndex = value.correctAnswerIndex
            //    });

            //}
        }
        else
        {
            //Debug.LogError("No se recibieron datos o el array está vacío.");
        }
    }

    // Select 10 random questions
    void SelectRandomQuestions()
    {
        //List<Question> tempQuestions = new List<Question>(questions);
        //for (int i = 0; i < 10 && tempQuestions.Count > 0; i++)
        //{
        //    int randomIndex = Random.Range(0, tempQuestions.Count);
        //    selectedQuestions.Add(tempQuestions[randomIndex]);
        //    tempQuestions.RemoveAt(randomIndex);
        }
    }
//void LoadQuestion()
//{
//    //if (currentQuestionIndex >= selectedQuestions.Count)
//    //{
//    //    foreach (var text in questionTexts)
//    //    {
//    //        text.text = "Game over. Team score: " + teamScore;
//    //    }

//    //    // Find the player with the highest score
//    //    int maxScore = playerScores.Max();
//    //    int bestPlayerIndex = playerScores.ToList().IndexOf(maxScore);

//    //    for (int i = 0; i < feedbackTexts.Count; i++)
//    //    {
//    //        if (i == bestPlayerIndex)
//    //        {
//    //            feedbackTexts[i].text = $"Congratulations! You’ve been the MVP, leading your team with the highest score.";
//    //        }
//    //        else
//    //        {
//    //            feedbackTexts[i].text = ""; // Leave empty so only the mvp can see the message.
//    //        }
//    //    }

//    //    // Call the method to introduce points to the API
//    //    StartCoroutine(OnQuizCompletedUpdatePoints(teamScore));

//    //    DisableButtons();
//    //    return;
//    //}

//    //foreach (var button in answerButtons)
//    //{
//    //    button.image.sprite = defaultSprite; // Set my default sprite.
//    //}

//    //currentQuestion = selectedQuestions[currentQuestionIndex];

//    //foreach (var feedback in feedbackTexts)
//    //{
//    //    feedback.text = ""; // Clear feedback messages
//    //}

//    //EnableButtons();

//    //// Show the question
//    //foreach (var text in questionTexts)
//    //{
//    //    text.text = currentQuestion.questionText;
//    //}

//    //// Mix up the answers and assign them to the panels
//    //List<int> numbers = new List<int> { 0, 1, 2, 3, 4 };
//    //System.Random random = new System.Random(); // Random Number Generator

//    //// Remove the index of the correct answer
//    //numbers.Remove(currentQuestion.correctAnswerIndex);

//    //// Create the mixed response list
//    //List<int> answers = new List<int>();

//    //// Mixing the rates of incorrect answers
//    //numbers = numbers.OrderBy(x => random.Next()).ToList();

//    //// Add incorrect answers
//    //for (int i = 0; i < totalPlayers - 1; i++)
//    //{
//    //    answers.Add(numbers[i]);
//    //}

//    //// Add the correct answer
//    //answers.Add(currentQuestion.correctAnswerIndex);

//    //// Shuffle the final answers again
//    //answers = answers.OrderBy(x => random.Next()).ToList();

//    //// Assign responses to text panels
//    //for (int i = 0; i < totalPlayers; i++)
//    //{
//    //    feedbackTexts[i].text = currentQuestion.answers[answers[i]];
//    //}

//    //timer = 5.0f; // Reset the stopwatch
//    //isTimerRunning = true;

//    //// Clean and add listeners to buttons
//    //for (int i = 0; i < totalPlayers; i++)
//    //{
//    //    int index = i;
//    //    // Clear any old listeners
//    //    answerButtons[i].onClick.RemoveAllListeners();
//    //    // Add the current listener
//    //    answerButtons[i].onClick.AddListener(() => CheckAnswer(index, answers[index]));
//    //}

//    //}


//}
