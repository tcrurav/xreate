using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TeamWorkSpaces.LeisureModule
{
    public class ForgeManager : MonoBehaviour
    {
        public static ForgeManager Instance { get; private set; }

        [Header("Referencias")]
        public QuestionManager questionManager;
        public UIManager uiManager;
        public GameObject forgeTrigger; // El objeto que recibe las palabras en la forja

        [Header("Configuración de Tiempo y Límites")]
        public float timeLimit = 120f; // Tiempo límite en segundos (2 minutos)
        public int maxWords = 25; // Máximo de palabras antes de finalizar

        private List<string> collectedWords = new List<string>(); // Palabras introducidas en la forja
        private bool sessionActive = false; // Controla si la sesión está activa
        private float elapsedTime = 0f; // Tiempo transcurrido

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ResetForge();
        }

        private void Update()
        {
            if (!sessionActive) return;

            elapsedTime += Time.deltaTime;

            // Condición de finalización: tiempo límite alcanzado o palabras máximas introducidas
            if (elapsedTime >= timeLimit || collectedWords.Count >= maxWords)
            {
                EndWordSelectionPhase();
            }
        }

        /// <summary>
        /// Se activa cuando una palabra entra en la forja
        /// </summary>
        public void AddWordToForge(string word)
        {
            if (!sessionActive) return;

            if (!collectedWords.Contains(word))
            {
                collectedWords.Add(word);
                Debug.Log($"[ForgeManager] 🔥 Palabra añadida: {word} (Total: {collectedWords.Count}/{maxWords})");

                // Comprobación si ya se han añadido todas las palabras
                if (collectedWords.Count >= maxWords)
                {
                    EndWordSelectionPhase();
                }
            }
        }

        /// <summary>
        /// Finaliza la fase de selección de palabras y calcula la puntuación.
        /// </summary>
        private void EndWordSelectionPhase()
        {
            sessionActive = false;
            Debug.Log("[ForgeManager] ⏳ Fase de selección de palabras finalizada.");

            int correctWords = collectedWords.Count(word => questionManager.IsCorrectWord(word));
            int incorrectWords = collectedWords.Count - correctWords;

            // Fórmula de puntuación basada en correctas/incorrectas
            int finalScore = (correctWords + 1) * 10 - (incorrectWords * 5);
            finalScore = Mathf.Max(0, finalScore); // Asegurar que la puntuación no sea negativa

            Debug.Log($"🏆 Puntuación Final: {finalScore} (✔ {correctWords} | ❌ {incorrectWords})");

            // Mostrar puntuación en UI
            uiManager.ShowFinalScore(finalScore, 0);


            // Iniciar fase de respuestas
            StartCoroutine(StartAnswerSelectionPhase());
        }

        /// <summary>
        /// Inicia la fase de selección de respuestas correctas.
        /// </summary>
        private IEnumerator StartAnswerSelectionPhase()
        {
            Debug.Log("[ForgeManager] ⏳ Preparando fase de respuestas...");
            yield return new WaitForSeconds(2f); // Pequeña pausa para mostrar la puntuación

            questionManager.StartAnswerSelection();
        }

        /// <summary>
        /// Inicia la sesión de juego
        /// </summary>
        public void StartWordSelectionPhase()
        {
            Debug.Log("[ForgeManager] 🚀 Iniciando nueva sesión de selección de palabras.");
            ResetForge();
            sessionActive = true;
            elapsedTime = 0f;
        }

        /// <summary>
        /// Resetea la forja para una nueva sesión.
        /// </summary>
        public void ResetForge()
        {
            collectedWords.Clear();
            elapsedTime = 0f;
            sessionActive = false;
            Debug.Log("[ForgeManager] 🔄 La forja ha sido reiniciada.");
        }
    }
}
