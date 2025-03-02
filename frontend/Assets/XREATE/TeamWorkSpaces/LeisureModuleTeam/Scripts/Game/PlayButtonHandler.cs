using UnityEngine;
using UnityEngine.UI;

namespace TeamWorkSpaces.LeisureModule
{
    public class PlayButtonHandler : MonoBehaviour
    {
        public QuestionListUI questionListUI;
        public QuestionManager questionManager;
        public Button playButton;

        void Start()
        {
            if (playButton != null)
            {
                playButton.onClick.AddListener(OnPlayButtonClicked);
            }
        }

        void OnPlayButtonClicked()
        {
            if (questionListUI == null || questionManager == null)
            {
                Debug.LogError("❌ ERROR: Falta asignar QuestionListUI o QuestionManager en PlayButtonHandler.");
                return;
            }

            // Confirmar la selección de la pregunta y cargarla en QuestionManager
            questionListUI.ConfirmSelectedQuestion();
        }
    }
}
