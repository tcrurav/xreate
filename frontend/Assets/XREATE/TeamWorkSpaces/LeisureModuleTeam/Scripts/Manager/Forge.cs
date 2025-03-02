using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TeamWorkSpaces.LeisureModule
{
    public class Forge : MonoBehaviour
    {
        [Header("Forge Word Storage")]
        public List<string> storedWords = new List<string>(); // Stores words added to the forge

        [Header("Question Manager Reference")]
        public QuestionManager questionManager; // Reference to access correct answers

        /// <summary>
        /// Handles incoming words and stores them if they are not duplicates.
        /// </summary>
        /// <param name="wordText">The word being added to the forge.</param>
        public void HandleWordEntry(string wordText)
        {
            if (string.IsNullOrEmpty(wordText))
            {
                Debug.LogWarning("[Forge] ⚠️ Empty word detected, skipping.");
                return;
            }

            Debug.Log($"[Forge] 🔥 Received word: {wordText}");

            // ✅ Prevent duplicate words
            if (storedWords.Contains(wordText))
            {
                Debug.Log($"[Forge] ⚠️ Word '{wordText}' is already in the forge.");
                return;
            }

            storedWords.Add(wordText);
            Debug.Log($"[Forge] 📜 Updated word list: {string.Join(", ", storedWords)}");
        }

        /// <summary>
        /// Evaluates the stored words and calculates a score based on correct answers.
        /// </summary>
        public void EvaluateForge()
        {
            if (questionManager == null || questionManager.CurrentQuestion == null)
            {
                Debug.LogError("[Forge] ❌ No valid question found for evaluation.");
                return;
            }

            List<string> correctWords = GetCorrectWordsFromQuestion(questionManager.CurrentQuestion);

            int totalWords = storedWords.Count;
            int correctMatches = storedWords.Count(word => correctWords.Contains(word));

            Debug.Log($"[Forge] ✅ Evaluation Complete: {correctMatches}/{totalWords} correct words.");

            // You can trigger UI updates or scoring logic here
        }

        /// <summary>
        /// Extracts all correct words from the current question.
        /// </summary>
        private List<string> GetCorrectWordsFromQuestion(Question question)
        {
            HashSet<string> correctWords = new HashSet<string>();

            foreach (var answer in question.correct_answers)
            {
                correctWords.UnionWith(answer.words);
            }

            return correctWords.ToList();
        }

        /// <summary>
        /// Clears the stored words, resetting the forge.
        /// </summary>
        public void ResetForge()
        {
            storedWords.Clear();
            Debug.Log("[Forge] 🔄 Forge has been reset.");
        }
    }
}
