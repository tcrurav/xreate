using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TeamWorkSpaces.LeisureModule
{
    public class ForgeTrigger : MonoBehaviour
    {
        public List<string> collectedWords = new List<string>(); // Stores collected words
        public List<string> collectedAnswers = new List<string>(); // Stores collected answers
        private ForgeScoreManager forgeScoreManager; // Reference to Score Manager
        private QuestionManager questionManager; // Reference to QuestionManager

        private void Start()
        {
            // Find the ForgeScoreManager in the scene
            forgeScoreManager = FindFirstObjectByType<ForgeScoreManager>();
            questionManager = FindFirstObjectByType<QuestionManager>();

            if (forgeScoreManager == null)
            {
                Debug.LogError("[ForgeTrigger] ❌ ERROR: ForgeScoreManager not found in the scene!");
            }

            if (questionManager == null)
            {
                Debug.LogError("[ForgeTrigger] ❌ ERROR: QuestionManager not found in the scene!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null)
            {
                Debug.LogError("[ForgeTrigger] ❌ Null collider detected.");
                return;
            }

            // ✅ Detect WordPrefab
            Word wordScript = other.GetComponentInParent<Word>();
            if (wordScript != null)
            {
                ProcessWordEntry(wordScript);
                return;
            }

            // ✅ Detect AnswerPrefab
            Answer answerScript = other.GetComponentInParent<Answer>();
            if (answerScript != null)
            {
                ProcessAnswerEntry(answerScript);
                return;
            }

            Debug.LogWarning($"[ForgeTrigger] ⚠️ Ignored {other.gameObject.name} (No Word or Answer component).");
        }

        private void ProcessWordEntry(Word wordScript)
        {
            string wordText = wordScript.GetWord();
            if (string.IsNullOrEmpty(wordText))
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ Empty word detected, skipping.");
                return;
            }

            if (collectedWords.Contains(wordText))
            {
                Debug.Log($"[ForgeTrigger] ⚠️ Word '{wordText}' is already in the forge.");
                return;
            }

            collectedWords.Add(wordText);
            Debug.Log($"[ForgeTrigger] 📜 Total words in forge: {collectedWords.Count}");

            if (questionManager != null)
            {
                questionManager.VerifyWordInForge(wordText);
            }
            else
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ No QuestionManager detected, cannot verify word correctness.");
            }

            Forge forge = GetComponentInParent<Forge>();
            if (forge != null)
            {
                forge.HandleWordEntry(wordText);
            }
            else
            {
                Debug.LogError("[ForgeTrigger] ❌ ERROR: Forge component not found in parent!");
            }

            if (forgeScoreManager != null)
            {
                forgeScoreManager.AddWord(wordText);
            }
            else
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ No ForgeScoreManager detected, score will not update.");
            }

            StartCoroutine(DestroyObjectWithDelay(wordScript.gameObject, 1.5f));
        }

        private void ProcessAnswerEntry(Answer answerScript)
        {
            string answerText = answerScript.GetAnswer();
            if (string.IsNullOrEmpty(answerText))
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ Empty answer detected, skipping.");
                return;
            }

            if (collectedAnswers.Contains(answerText))
            {
                Debug.Log($"[ForgeTrigger] ⚠️ Answer '{answerText}' is already in the forge.");
                return;
            }

            collectedAnswers.Add(answerText);
            Debug.Log($"[ForgeTrigger] 📜 Total answers in forge: {collectedAnswers.Count}");

            if (forgeScoreManager != null)
            {
                forgeScoreManager.AddAnswer(answerText, answerScript.isCorrectAnswer);
            }
            else
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ No ForgeScoreManager detected, score will not update.");
            }

            StartCoroutine(DestroyObjectWithDelay(answerScript.gameObject, 1.5f));
        }

        private IEnumerator DestroyObjectWithDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (obj == null)
            {
                Debug.LogWarning("[ForgeTrigger] ⚠️ Object already destroyed, skipping.");
                yield break;
            }

            Debug.Log($"[ForgeTrigger] ❌ Destroying object: {obj.name}");
            Destroy(obj);
        }
    }
}
