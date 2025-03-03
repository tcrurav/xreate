using UnityEngine;
using System.Collections;
using System.Linq;

namespace TeamWorkSpaces.LeisureModule
{
    public class ForgeController : MonoBehaviour
    {
        public Animator animator;
        public UIFadeController uiFadeController;
        public GameObject triggerForge;
        public QuestionTextUpdater questionTextUpdater;
        public QuestionManager questionManager;
        public float delayBeforeLoadingWords = 2.5f;
        public bool IsOpening { get; private set; }

        public void StartForgeSequence()
        {
            Debug.Log("[ForgeController] 🔄 Play button pressed. Checking state...");

            if (IsOpening)
            {
                Debug.Log("[ForgeController] 🔄 Resetting before starting a new sequence...");
                StartCoroutine(ResetForgeBeforeNewSequence());

            }
            else
            {
                StartCoroutine(ForgeSequenceRoutine());
            }
        }

        private IEnumerator ResetForgeBeforeNewSequence()
        {
            if (uiFadeController != null)
            {
                Debug.Log("[ForgeController] ❌ Hiding UI elements...");
                uiFadeController.HideUI();
            }

            CloseForge();
            yield return new WaitForSeconds(1f);

            // ✅ Reemplazo del método obsoleto
            Word[] words = FindObjectsByType<Word>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Debug.Log($"[ForgeController] 🗑️ Destroying {words.Length} WordPrefabs...");
            foreach (var word in words)
            {
                Destroy(word.gameObject);
            }

            if (questionTextUpdater != null)
            {
                Debug.Log("[ForgeController] 🛑 Hiding question text...");
                foreach (var textElement in questionTextUpdater.questionTexts)
                {
                    textElement.text = "";
                }
            }

            if (questionManager != null)
            {
                questionManager.ClearCurrentQuestion();
            }

            yield return new WaitForSeconds(1f);

            Debug.Log("[ForgeController] ✅ Reset complete. Starting new sequence...");
            StartCoroutine(ForgeSequenceRoutine());
        }

        private IEnumerator ForgeSequenceRoutine()
        {
            if (uiFadeController != null)
            {
                Debug.Log("[ForgeController] 🎬 Starting UI fade-in...");
                uiFadeController.ShowUI();
            }

            yield return new WaitForSeconds(0.5f);

            OpenForge();
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            if (triggerForge != null)
            {
                Debug.Log("[ForgeController] ✨ Making TriggerForge appear...");
                StartCoroutine(FadeInObject(triggerForge, 1.5f));
            }

            yield return new WaitForSeconds(1.5f);

            Debug.Log("[ForgeController] 📜 Loading new question...");
            if (questionManager != null)
            {
                questionManager.LoadRandomQuestion();
            }

            yield return new WaitForSeconds(1f);

            if (questionTextUpdater != null)
            {
                questionTextUpdater.UpdateQuestionText();
            }

            yield return new WaitForSeconds(1f);

            Debug.Log("[ForgeController] 🔡 Loading words...");
            if (questionManager != null)
            {
                yield return new WaitForSeconds(delayBeforeLoadingWords);
                questionManager.LoadWordsForCurrentQuestion();
            }
        }

        public void OpenForge()
        {
            if (IsOpening) return;

            animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
            IsOpening = true;

            Debug.Log("[ForgeController] 🔥 OpenForge executed.");

            if (uiFadeController != null)
            {
                uiFadeController.ShowUI();
            }
        }

        public void CloseForge()
        {
            if (!IsOpening) return;

            animator.ResetTrigger("Open");
            animator.SetTrigger("Close");
            IsOpening = false;

            Debug.Log("[ForgeController] ❄️ CloseForge executed.");

            if (uiFadeController != null)
            {
                uiFadeController.HideUI();
            }
        }
        public void EvaluateForge()
        {
            Debug.Log("[ForgeController] 🏆 Evaluando la forja...");

            if (questionManager != null)
            {
                questionManager.EvaluateForge(); // Calcular puntuación
            }
            else
            {
                Debug.LogError("[ForgeController] ❌ No hay un QuestionManager asignado.");
            }
        }
        private IEnumerator FadeInObject(GameObject obj, float duration)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                foreach (var renderer in renderers)
                {
                    Color color = renderer.material.color;
                    color.a = Mathf.Lerp(0, 1, time / duration);
                    renderer.material.color = color;
                }
                yield return null;
            }

            foreach (var renderer in renderers)
            {
                Color color = renderer.material.color;
                color.a = 1;
                renderer.material.color = color;
            }
        }
    }
}
