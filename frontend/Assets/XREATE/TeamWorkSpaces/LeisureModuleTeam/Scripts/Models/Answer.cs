using UnityEngine;
using TMPro;

namespace TeamWorkSpaces.LeisureModule
{
    public class Answer : MonoBehaviour
    {
        public TextMeshPro textMeshPro;
        public bool isCorrectAnswer = false;
        private string answerText;

        public Material defaultMaterial;
        public Material grabbedMaterial;
        public Material correctMaterial;

        // Array de posiciones para las respuestas en la Fase 2
        public static Transform[] answerPositions;

        public static void SetAnswerPositions(Transform[] positions)
        {
            answerPositions = positions;
        }

        private void Awake()
        {
            if (textMeshPro == null)
                textMeshPro = GetComponentInChildren<TextMeshPro>();
        }

        public void SetAnswer(string text, bool isCorrect)
        {
            answerText = text;
            isCorrectAnswer = isCorrect;
            textMeshPro.text = text;
            Debug.Log($"[Answer] 📌 Respuesta asignada: '{answerText}' | Correcta: {isCorrectAnswer}");
            // Aplicar material si la respuesta es correcta
            if (isCorrectAnswer)
            {
                ApplyCorrectMaterial();
            }
        }

        public string GetAnswer()
        {
            return answerText;
        }

        public void OnGrabbed()
        {
            UpdateVisuals(grabbedMaterial);
        }

        public void OnReleased()
        {
            UpdateVisuals(defaultMaterial);
        }

        private void ApplyCorrectMaterial()
        {
            UpdateVisuals(correctMaterial);
        }

        private void UpdateVisuals(Material material)
        {
            var renderer = GetComponentInChildren<MeshRenderer>();
            if (renderer != null && material != null)
            {
                renderer.material = material;
            }
        }
    }
}
