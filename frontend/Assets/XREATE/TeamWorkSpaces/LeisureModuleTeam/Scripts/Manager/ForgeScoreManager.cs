using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace TeamWorkSpaces.LeisureModule
{
    public class ForgeScoreManager : MonoBehaviour
    {
        [Header("🔹 UI References")]
        public TextMeshProUGUI[] phase1ScoreTexts;
        public TextMeshProUGUI[] phase2ScoreTexts;
        public TextMeshProUGUI[] totalScoreTexts;

        [Header("🔹 Correct Answers")]
        private HashSet<string> correctWordsSet = new HashSet<string>(); // Correct words in Phase 1
        private HashSet<string> correctAnswersSet = new HashSet<string>(); // Correct answers in Phase 2

        private List<string> wordsInForge = new List<string>(); // Words stored in the forge (Phase 1)
        private List<string> answersInForge = new List<string>(); // Answers stored in the forge (Phase 2)

        private int totalPossibleWords = 0;
        private int totalPossibleAnswers = 0;
        private int correctWordsCount = 0;
        private int correctAnswersCount = 0;

        private QuestionManager questionManager;

        void Start()
        {
            questionManager = FindFirstObjectByType<QuestionManager>();

            if (questionManager == null)
            {
                Debug.LogError("[ForgeScoreManager] ❌ ERROR: QuestionManager not found in the scene!");
            }

            ResetScore();
        }

        // 🔹 Get normalized Phase 1 Score (0 - 10)
        public int GetPhase1Score()
        {
            return NormalizeScore(correctWordsCount, totalPossibleWords);
        }

        // 🔹 Get normalized Phase 2 Score (0 - 10)
        public int GetPhase2Score()
        {
            return NormalizeScore(correctAnswersCount, totalPossibleAnswers);
        }

        // 🔹 Get total answers submitted in Phase 2
        public int GetTotalAnswers()
        {
            return answersInForge.Count;
        }


        /// <summary>
        /// 🔹 Normalize score based on max 10-point scale
        /// </summary>
        private int NormalizeScore(int correctCount, int totalPossible)
        {
            if (totalPossible == 0) return 0; // Avoid division by zero
            return Mathf.RoundToInt((correctCount / (float)totalPossible) * 10);
        }

        /// <summary>
        /// 📌 Adds a word when it is placed inside the forge (Phase 1)
        /// </summary>
        public void AddWord(string word)
        {
            word = word.ToLower().Trim();

            if (wordsInForge.Contains(word))
            {
                Debug.Log($"[ForgeScoreManager] ⚠️ Word '{word}' is already counted.");
                return;
            }

            wordsInForge.Add(word);

            if (correctWordsSet.Contains(word))
            {
                correctWordsCount++;
                Debug.Log($"[ForgeScoreManager] ✅ Correct Word Added: {word}");
            }

            CalculateScore();
        }

        /// <summary>
        /// 📌 Adds an answer when it is placed inside the forge (Phase 2)
        /// </summary>
        public void AddAnswer(string answer, bool isCorrect)
        {
            answer = answer.ToLower().Trim();

            if (answersInForge.Contains(answer))
            {
                Debug.Log($"[ForgeScoreManager] ⚠️ Answer '{answer}' is already counted.");
                return;
            }

            answersInForge.Add(answer);

            if (isCorrect)
            {
                correctAnswersCount++;
                Debug.Log($"[ForgeScoreManager] ✅ Correct Answer Added: {answer}");
            }
            else
            {
                Debug.Log($"[ForgeScoreManager] ❌ Incorrect Answer Added: {answer}");
            }

            CalculateScore();
        }

        /// <summary>
        /// 📌 Updates the correct answers from the question manager
        /// </summary>
        public void SetCorrectAnswers(List<string> answers)
        {
            correctWordsSet.Clear();
            correctAnswersSet.Clear();

            foreach (var answer in answers)
            {
                string[] words = answer.Split(' '); // Split sentence into words
                foreach (var word in words)
                {
                    correctWordsSet.Add(word.ToLower());
                }
                correctAnswersSet.Add(answer.ToLower().Trim());
            }

            totalPossibleWords = correctWordsSet.Count;
            totalPossibleAnswers = correctAnswersSet.Count;

            Debug.Log($"[ForgeScoreManager] 🎯 Correct Words: {totalPossibleWords}, Correct Answers: {totalPossibleAnswers}");
        }

        /// <summary>
        /// 📊 Calculates scores for both phases
        /// </summary>
        private void CalculateScore()
        {
            if (questionManager == null || questionManager.CurrentQuestion == null)
            {
                Debug.LogWarning("[ForgeScoreManager] ⚠️ No current question loaded, skipping score calculation.");
                return;
            }

            int phase1Score = GetPhase1Score();
            int phase2Score = GetPhase2Score();

            Debug.Log($"[ForgeScoreManager] 🔄 Score Updated: Phase 1 - {phase1Score}/10, Phase 2 - {phase2Score}/10.");
            UpdateUI(phase1Score, phase2Score);
        }

        /// <summary>
        /// 🖥️ Updates the UI
        /// </summary>
        private void UpdateUI(int phase1Score, int phase2Score)
        {
            foreach (var text in phase1ScoreTexts)
            {
                if (text != null)
                {
                    text.text = $"Phase 1 Score: {phase1Score}/10";
                }
            }

            foreach (var text in phase2ScoreTexts)
            {
                if (text != null)
                {
                    text.text = $"Phase 2 Score: {phase2Score}/10";
                }
            }

            int totalScore = phase1Score + phase2Score;
            foreach (var text in totalScoreTexts)
            {
                if (text != null)
                {
                    text.text = $"Total Score: {totalScore}/20";
                }
            }

            Debug.Log($"[ForgeScoreManager] 🖥️ UI Updated: Phase 1 - {phase1Score}/10, Phase 2 - {phase2Score}/10, Total - {totalScore}/20.");
        }

        /// <summary>
        /// 🛑 Resets the score for a new round
        /// </summary>
        public void ResetScore()
        {
            wordsInForge.Clear();
            answersInForge.Clear();
            correctWordsCount = 0;
            correctAnswersCount = 0;
            totalPossibleWords = 0;
            totalPossibleAnswers = 0;

            UpdateUI(0, 0);
        }
    }
}
