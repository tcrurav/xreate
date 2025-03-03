using UnityEngine;
using TMPro;

namespace TeamWorkSpaces.LeisureModule
{
    public class QuestionTextUpdater : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI[] questionTexts; // UI text elements for all sides of the cube
        public QuestionManager questionManager; // Reference to the QuestionManager

        private const string DefaultText = "No question loaded."; // Default message

        void Start()
        {
            // Initialize all question text elements with default text
            ClearQuestionText();
        }

        /// <summary>
        /// Updates all UI elements with the current question from the QuestionManager.
        /// </summary>
        public void UpdateQuestionText()
        {
            if (questionManager == null)
            {
                Debug.LogError("[QuestionTextUpdater] ❌ ERROR: QuestionManager reference is missing.");
                return;
            }

            if (questionManager.CurrentQuestion == null)
            {
                Debug.LogWarning("[QuestionTextUpdater] ⚠ No valid question found. Clearing text...");
                ClearQuestionText();
                return;
            }

            string questionText = questionManager.CurrentQuestion.text;

            // Update all assigned text elements
            for (int i = 0; i < questionTexts.Length; i++)
            {
                if (questionTexts[i] != null)
                {
                    questionTexts[i].text = questionText;
                }
                else
                {
                    Debug.LogWarning($"[QuestionTextUpdater] ⚠ UI element at index {i} is missing.");
                }
            }

            Debug.Log($"[QuestionTextUpdater] ✅ Updated question: {questionText}");
        }

        /// <summary>
        /// Clears all UI text elements and resets them to the default message.
        /// </summary>
        public void ClearQuestionText()
        {
            for (int i = 0; i < questionTexts.Length; i++)
            {
                if (questionTexts[i] != null)
                {
                    questionTexts[i].text = DefaultText;
                }
            }

            Debug.Log("[QuestionTextUpdater] 🔄 Cleared question text.");
        }
    }
}
