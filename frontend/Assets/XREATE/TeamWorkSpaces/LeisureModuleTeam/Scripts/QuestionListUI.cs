using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace TeamWorkSpaces.LeisureModule
{
    public class QuestionListUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject questionPrefab; // Prefab for each question item
        public Transform questionContainer; // Parent container inside ScrollView
        public QuestionManager questionManager; // Reference to the QuestionManager
        public TextMeshProUGUI counterText; // UI text to show the number of selected questions

        private List<GameObject> questionObjects = new List<GameObject>(); // Stores instantiated question UI elements
        private int selectedQuestionId = -1; // Stores the currently selected question ID

        void Start()
        {
            Debug.Log("[QuestionListUI] 🔄 Initializing...");

            if (questionManager == null)
            {
                Debug.LogError("❌ ERROR: QuestionManager is not assigned.");
                return;
            }
        }

        /// <summary>
        /// Loads all available questions from the QuestionManager into the UI.
        /// </summary>
        public void LoadQuestions()
        {
            Debug.Log("[QuestionListUI] 📜 Loading questions into UI...");

            var questions = questionManager.GetQuestions();
            if (questions == null || questions.Count == 0)
            {
                Debug.LogError("❌ ERROR: No questions available in QuestionManager.");
                return;
            }

            // Clear previous UI elements
            ClearQuestionList();

            // Populate the UI with new question items
            foreach (var question in questions)
            {
                GameObject newQuestion = Instantiate(questionPrefab, questionContainer);
                newQuestion.transform.localScale = Vector3.one;

                TextMeshProUGUI textComponent = newQuestion.transform.Find("QuestionText")?.GetComponent<TextMeshProUGUI>();
                Toggle toggle = newQuestion.transform.Find("SelectionToggle")?.GetComponent<Toggle>();

                if (textComponent == null || toggle == null)
                {
                    Debug.LogError($"❌ ERROR: Missing UI components in the question prefab for '{question.text}'.");
                    Destroy(newQuestion);
                    continue;
                }

                // Set question text
                textComponent.text = question.text;

                // Add toggle listener
                toggle.onValueChanged.AddListener((isSelected) => OnQuestionSelected(question.id, isSelected));

                questionObjects.Add(newQuestion);
            }
        }

        /// <summary>
        /// Called when a question toggle is selected or deselected.
        /// Ensures only one question is selected at a time.
        /// </summary>
        private void OnQuestionSelected(int id, bool isSelected)
        {
            if (isSelected)
            {
                selectedQuestionId = id;
                Debug.Log($"[QuestionListUI] ✅ Selected question ID: {id}");

                // Deselect other toggles
                foreach (var obj in questionObjects)
                {
                    Toggle toggle = obj.GetComponentInChildren<Toggle>();
                    if (toggle != null && toggle.isOn && obj != questionObjects.First(q => q.name == id.ToString()))
                    {
                        toggle.isOn = false;
                    }
                }
            }
            else if (selectedQuestionId == id)
            {
                selectedQuestionId = -1;
                Debug.Log("[QuestionListUI] ❌ Deselected question.");
            }

            // Update the counter UI
            counterText.text = selectedQuestionId != -1 ? "1 Selected" : "0 Selected";
        }

        /// <summary>
        /// Loads the currently selected question into the QuestionManager.
        /// </summary>
        public void ConfirmSelectedQuestion()
        {
            if (selectedQuestionId == -1)
            {
                Debug.LogWarning("⚠ No question selected.");
                return;
            }

            var selectedQuestion = questionManager.GetQuestions().FirstOrDefault(q => q.id == selectedQuestionId);
            if (selectedQuestion == null)
            {
                Debug.LogError($"❌ ERROR: Question ID {selectedQuestionId} not found.");
                return;
            }

            Debug.Log($"[QuestionListUI] 📌 Loading selected question: {selectedQuestion.text}");
            questionManager.LoadSelectedQuestion(selectedQuestion);
        }

        /// <summary>
        /// Clears all currently displayed questions from the UI.
        /// </summary>
        private void ClearQuestionList()
        {
            foreach (GameObject obj in questionObjects)
            {
                Destroy(obj);
            }
            questionObjects.Clear();
            selectedQuestionId = -1;
            counterText.text = "0 Selected";
        }
    }
}
