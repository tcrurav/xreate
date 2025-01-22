using UnityEngine;
using TMPro;

namespace TeamWorkSpaces.LeisureModule
{
    public class QuestionTextUpdater : MonoBehaviour
    {
        public TextMeshProUGUI questionText; // Referencia al componente TMP en la escena
        public QuestionManager questionManager; // Referencia al QuestionManager

        void Start()
        {
            // Inicializar el texto en el inicio
            if (questionText != null)
            {
                questionText.text = "No hay pregunta cargada."; // Texto predeterminado
            }
        }

        public void UpdateQuestionText()
        {
            if (questionManager != null && questionManager.CurrentQuestion != null)
            {
                // Actualizar el texto con la pregunta actual
                questionText.text = questionManager.CurrentQuestion.text;
                Debug.Log($"Texto de la pregunta actualizado: {questionManager.CurrentQuestion.text}");
            }
            else
            {
                // Mostrar texto predeterminado si no hay pregunta
                questionText.text = "No hay pregunta cargada.";
                Debug.LogWarning("No hay una pregunta actual para mostrar.");
            }
        }
    }
}
