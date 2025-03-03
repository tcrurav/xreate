using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TeamWorkSpaces.LeisureModule
{
    public class GameSessionManager : MonoBehaviour
    {

        [Header("🔹 Posiciones para las respuestas en la Fase 2")]
        public Transform[] answerPositions; // Array de posiciones para las respuestas en la Fase 2
        public GameObject answerPrefab; // Prefab de las respuestas
        private ForgeScoreManager forgeScoreManager; // 🔹 Referencia al sistema de puntuación

        [Header("🔹 Referencias")]
        public QuestionManager questionManager;
        public ForgeController forgeController;
        public UIManager uiManager;
        public HologramLightManager hologramLightManager;


        [Header("🔹 Configuración de la Ronda")]
        public float totalRoundTimeLimit = 180f; // ⏳ Tiempo límite de la ronda completa
        public float endRoundDelay = 5f; // ⏳ Tiempo de espera tras Phase 2 antes de la nueva ronda
        public bool autoStartNextRound = true; // ✅ Ejecutar automáticamente la siguiente ronda

        [Header("🔹 Configuración de la Phase 1")]
        public float phase1TimeLimit = 60f; // ⏳ Tiempo límite en segundos (personalizable)
        public int maxWords = 25; // 🔢 Máximo de palabras permitidas

        [Header("🔹 Configuración de la Phase 2")]
        public float phase2TimeLimit = 60f; // ⏳ Tiempo límite en Phase 2
        public int maxAnswers = 10; // 🔢 Máximo de respuestas en Phase 2

        [Header("🔹 Configuración entre phases")]
        public float transitionTimeBetweenPhases = 5f; // ⏳ Espera entre Phase 1 y Phase 2
        public bool autoStartPhase2 = true; // ✅ Ejecutar automáticamente la Phase 2 tras Phase 1


        private bool isPaused = false; // ⏸ Control de pausa


        private bool phase1Active = false;
        private bool phase2Active = false;
        private float roundTimer;
        private float phase1Timer;
        private float phase2Timer;
        private int currentWordCount = 0; // 📝 Palabras usadas en la forja
        private int roundNumber = 1; // 🔄 Contador de rondas
        private List<RoundData> roundScores = new List<RoundData>(); // 📊 Historial de rondas


        private float timer;


        private void Start()
        {


            // Verificar UIManager
            if (uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
                if (uiManager == null)
                {
                    Debug.LogError("❌ UIManager no encontrado en la escena.");
                    return;
                }
            }

            // Verificar QuestionManager
            if (questionManager == null)
            {
                questionManager = FindObjectOfType<QuestionManager>();
                if (questionManager == null)
                {
                    Debug.LogError("❌ QuestionManager no encontrado en la escena.");
                    return;
                }
            }

            // Verificar ForgeController
            if (forgeController == null)
            {
                forgeController = FindObjectOfType<ForgeController>();
                if (forgeController == null)
                {
                    Debug.LogError("❌ ForgeController no encontrado en la escena.");
                    return;
                }
            }

            // Verificar HologramLightManager
            if (hologramLightManager == null)
            {
                hologramLightManager = FindObjectOfType<HologramLightManager>();
                if (hologramLightManager == null)
                {
                    Debug.LogError("❌ HologramLightManager no encontrado en la escena.");
                    return;
                }
            }

            // Verificar ForgeScoreManager
            if (forgeScoreManager == null)
            {
                forgeScoreManager = FindObjectOfType<ForgeScoreManager>();
                if (forgeScoreManager == null)
                {
                    Debug.LogError("❌ ForgeScoreManager no encontrado en la escena.");
                    return;
                }
            }

            // ✅ Asegurar que los temporizadores tomen los valores configurados en el inspector
            roundTimer = totalRoundTimeLimit;
            phase1Timer = phase1TimeLimit;
            phase2Timer = phase2TimeLimit;

            Debug.Log($"⏳ Tiempo límite de la ronda completa: {totalRoundTimeLimit} segundos.");
            Debug.Log($"⏳ Tiempo límite de Phase 1: {phase1TimeLimit} segundos.");
            Debug.Log($"🔢 Máximo de palabras permitidas: {maxWords}.");
            Debug.Log($"⏳ Espera entre Phase 1 y Phase 2: {transitionTimeBetweenPhases} segundos.");
            Debug.Log($"⏳ Tiempo límite de Phase 2: {phase2TimeLimit} segundos.");
            Debug.Log($"🔢 Máximo de respuestas en Phase 2: {maxAnswers}.");
            Debug.Log($"⏳ Tiempo de espera tras Phase 2 antes de la nueva ronda: {endRoundDelay} segundos.");

            // Asegurar que las posiciones de respuestas estén asignadas
            if (answerPositions != null && answerPositions.Length > 0)
            {
                Answer.SetAnswerPositions(answerPositions);
            }
            else
            {
                Debug.LogError("❌ No hay posiciones definidas para las respuestas en la Fase 2.");
            }

            hologramLightManager = FindObjectOfType<HologramLightManager>();
            if (hologramLightManager == null)
            {
                Debug.LogError("❌ ERROR: HologramLightManager no encontrado en la escena.");
            }
        }



        /// <summary>
        /// 🔄 Esperar un tiempo antes de iniciar una nueva ronda
        /// </summary>
        private IEnumerator WaitBeforeNewRound()
        {
            Debug.Log($"⏳ Esperando {endRoundDelay} segundos antes de la siguiente ronda...");
            yield return new WaitForSeconds(endRoundDelay); // ✅ Usar el tiempo configurado en el inspector

            StartNewRound(); // 🔄 Iniciar la nueva ronda
        }

        private IEnumerator WaitBeforeRestart()
        {
            Debug.Log("🔄 Esperando antes de reiniciar la misma ronda...");
            yield return new WaitForSeconds(3f); // Espera personalizada
            RestartCurrentRound();
        }



        private List<int> usedQuestionIDs = new List<int>(); // 📜 Guardar preguntas ya usadas

        /// <summary>
        /// 🔄 Inicia una nueva ronda con una nueva pregunta no repetida.
        /// </summary>
        public void StartNewRound()
        {
            Debug.Log($"📢 Iniciando Ronda {roundNumber}...");

            if (uiManager == null)
            {
                Debug.LogError("❌ Error: UIManager no asignado. No se puede actualizar la UI.");
                return;
            }

            if (questionManager == null)
            {
                Debug.LogError("❌ Error: QuestionManager no asignado. No se puede obtener una nueva pregunta.");
                return;
            }

            phase1Active = true;
            currentWordCount = 0;

            // ✅ Usar el tiempo configurado en el inspector
            roundTimer = totalRoundTimeLimit;
            phase1Timer = phase1TimeLimit;
            phase2Timer = phase2TimeLimit;

            // Buscar una nueva pregunta que no haya sido usada
            int newQuestionID;
            do
            {
                newQuestionID = questionManager.GetRandomQuestionID();
            }
            while (usedQuestionIDs.Contains(newQuestionID));

            usedQuestionIDs.Add(newQuestionID); // Guardar pregunta usada
            questionManager.SetCurrentQuestion(newQuestionID);

            // 🔄 Actualizar UI con la nueva ronda
            uiManager.UpdateRoundInfo(roundNumber);
            uiManager.UpdateScoreUI(roundScores);

            // 💡 Activar efecto de luces antes de cargar palabras
            if (hologramLightManager != null)
            {
                Debug.Log("💡 Activando luces del holograma antes de cargar las palabras...");
                hologramLightManager.StartCoroutine("DelayedRunDemoSequence");
            }

            // 🔄 Actualizar la UI con las instrucciones de Phase 1
            uiManager.UpdatePhaseInstructions(
                "Phase 1: Forge the Key Words!",
                "Drag the correct words into the Forge.\n\n" +
                "Only words related to the question will score points.\n\n" +
                "Be precise! Incorrect words will reduce your score.\n\n" +
                "You have a limited time or until a certain number of words are used.\n\n" +
                "Once done, the next phase will begin."
            );

            // 🔥 Iniciar la Fase 1 (Forja)
            forgeController.StartForgeSequence();
            StartCoroutine(Phase1Timer());
        }



        /// <summary>
        /// 📌 Inicia una nueva ronda y arranca la Fase 1.
        /// </summary>
        public void StartNewRound1()
        {
            Debug.Log($"📢 Iniciando Ronda {roundNumber}...");

            phase1Active = true;
            timer = phase1TimeLimit;
            currentWordCount = 0;

            // 🔄 Actualizar UI con la nueva ronda
            uiManager.UpdateRoundInfo(roundNumber);
            uiManager.UpdateScoreUI(roundScores);

            // 💡 Activar efecto de luces antes de cargar palabras
            if (hologramLightManager != null)
            {
                Debug.Log("💡 Activando luces del holograma antes de cargar las palabras...");
                hologramLightManager.StartCoroutine("DelayedRunDemoSequence");
            }

            // 🔥 Iniciar la Fase 1 (Forja)
            forgeController.StartForgeSequence();
            StartCoroutine(Phase1Timer());
        }

        public void RestartCurrentRound()
        {
            Debug.Log("🔄 Reiniciando ronda actual...");

            // Reiniciar temporizadores
            roundTimer = totalRoundTimeLimit;
            phase1Timer = phase1TimeLimit;
            phase2Timer = phase2TimeLimit;

            // Resetear estados
            phase1Active = false;
            phase2Active = false;
            currentWordCount = 0;

            // Borrar palabras y respuestas en la escena
            DestroyAllWords();
            DestroyAllAnswers();

            // Reiniciar puntuaciones
            forgeScoreManager.ResetScore();

            // Volver a empezar con la misma pregunta
            StartNewRound();
        }



        /// <summary>
        /// ⏳ Controla el tiempo de la Fase 1
        /// </summary>
        private IEnumerator Phase1Timer()
        {
            float timer = phase1Timer; // ✅ Usar el valor configurado en el inspector

            while (phase1Active && timer > 0)
            {
                timer -= Time.deltaTime;
                uiManager.UpdateTimerUI(timer); // ⏳ Actualizar la UI en cada frame
                yield return null;

                // 🚨 Si se han usado todas las palabras, terminamos la fase
                if (currentWordCount >= maxWords)
                {
                    Debug.Log("📝 Límite de palabras alcanzado. Pasando a la Fase 2...");
                    EndPhase1();
                    yield break;
                }
            }

            // ⏳ Si el tiempo se acaba, terminamos la fase
            if (timer <= 0)
            {
                Debug.Log("⏳ Tiempo agotado. Pasando a la Fase 2...");
                EndPhase1();
            }
        }

        /// <summary>
        /// 📝 Agregar palabra a la forja y verificar condiciones de finalización
        /// </summary>
        public void AddWordToForge(string word)
        {
            if (!phase1Active) return;

            currentWordCount++;

            // ✅ Verificar si se llegó al límite de palabras
            if (currentWordCount >= maxWords)
            {
                Debug.Log("📝 Se alcanzó el límite de palabras, terminando Fase 1...");
                EndPhase1();
            }
        }


        /// <summary>
        /// 🚨 Terminar la Fase 1 y calcular puntuación
        /// </summary>
        /// 
        public void EndPhase1()
        {
            if (!phase1Active) return;
            phase1Active = false;

            Debug.Log($"📢 Fase 1 finalizada - Ronda {roundNumber}.");
            forgeController.EvaluateForge(); // 🏆 Evaluar la puntuación final

            int totalWordsPlaced = currentWordCount;
            int correctWords = forgeScoreManager.GetPhase1Score();
            int incorrectWords = totalWordsPlaced - correctWords;
            int phase1Score = correctWords; // Puntuación basada en palabras correctas

            // ✅ Asegurar que la UI de resultados está activa
            if (uiManager.resultsCanvas != null)
            {
                uiManager.resultsCanvas.SetActive(true);
            }

            // 🔄 Actualizar UI con los resultados de la Fase 1
            uiManager.DisplayResultsInPanels(totalWordsPlaced, correctWords, incorrectWords, phase1Score, 0, 0, 0, 0);

            Debug.Log($"📢 Datos enviados a UIManager: {totalWordsPlaced} palabras, {correctWords} correctas, {incorrectWords} incorrectas, Puntuación {phase1Score}/10");

            StartCoroutine(WaitBeforePhase2());
        }



        /// <summary>
        /// 🚨 Terminar la Fase 2 y calcular la puntuación final de la ronda
        /// </summary>
        public void EndPhase2()
        {
            Debug.Log("📢 Finalizando Fase 2...");
            phase2Active = false;

            // 🔥 Eliminar todas las respuestas de la escena
            DestroyAllAnswers();

            int totalAnswersPlaced = forgeScoreManager.GetTotalAnswers();
            int correctAnswers = forgeScoreManager.GetPhase2Score();
            int incorrectAnswers = totalAnswersPlaced - correctAnswers;
            int phase2Score = correctAnswers; // Puntuación basada en respuestas correctas

            // ✅ Asegurar que la UI de resultados está activa
            if (uiManager.resultsCanvas != null)
            {
                uiManager.resultsCanvas.SetActive(true);
            }

            // 🔄 Actualizar UI con los resultados de la Fase 2
            uiManager.DisplayResultsInPanels(currentWordCount, forgeScoreManager.GetPhase1Score(),
                                             currentWordCount - forgeScoreManager.GetPhase1Score(),
                                             forgeScoreManager.GetPhase1Score(),
                                             totalAnswersPlaced, correctAnswers, incorrectAnswers, phase2Score);

            // 📊 Actualizar UI con precisión y puntuación
            uiManager.UpdatePhase2UI(totalAnswersPlaced, correctAnswers, incorrectAnswers);

            // 🔄 Pasar a la siguiente ronda con tiempo de espera
            StartCoroutine(WaitBeforeNewRound());
        }


        /// <summary>
        /// ⏳ Esperar un tiempo antes de iniciar la Fase 2
        /// </summary>
        private IEnumerator WaitBeforePhase2()
        {
            Debug.Log($"⏳ Esperando {transitionTimeBetweenPhases} segundos antes de iniciar la Fase 2...");
            yield return new WaitForSeconds(transitionTimeBetweenPhases);

            StartPhase2();
        }



        /// <summary>
        /// 📌 Inicia la Fase 2 (Selección de Respuestas)
        /// </summary>
        public void StartPhase2()
        {
            Debug.Log("📢 Iniciando Fase 2...");
            phase2Active = true;

            uiManager.UpdatePhaseInstructions(
    "Phase 2: Forge the Key Answers!",
    "Drag the correct answers into the Forge.\n\n" +
    "Only answers related to the question will score points.\n\n" +
    "Be precise! Incorrect answers will reduce your score.\n\n" +
    "You have a limited time or until a certain number of answers are used.\n\n" +
    "Once done, the next end round."
);

            // ✅ Usar el tiempo configurado en el inspector
            phase2Timer = phase2TimeLimit;

            SpawnAnswersForPhase2();
            StartCoroutine(Phase2Timer());
        }

        /// <summary>
        /// ⏳ Controla el tiempo de la Fase 2
        /// </summary>
        private IEnumerator Phase2Timer()
        {
            float timer = phase2Timer; // ✅ Usar el valor configurado en el inspector

            while (phase2Active && timer > 0)
            {
                timer -= Time.deltaTime;
                uiManager.UpdateTimerUI(timer);
                yield return null;
            }

            if (timer <= 0)
            {
                Debug.Log("⏳ Tiempo de la Fase 2 agotado.");
                EndPhase2();
            }
        }


        private void DestroyAllWords()
        {
            Word[] words = FindObjectsOfType<Word>(); // Encuentra todos los objetos Word en la escena

            foreach (Word word in words)
            {
                Destroy(word.gameObject); // Destruye cada palabra encontrada
            }

            Debug.Log($"🗑️ Eliminados {words.Length} WordPrefabs antes de iniciar la Fase 2.");
        }

        private void SpawnAnswersForPhase2()
        {
            if (answerPrefab == null)
            {
                Debug.LogError("❌ No se ha asignado el prefab de respuestas en GameSessionManager.");
                return;
            }

            QuestionManager questionManager = FindObjectOfType<QuestionManager>();
            if (questionManager == null || questionManager.CurrentQuestion == null)
            {
                Debug.LogError("❌ No hay una pregunta activa en QuestionManager.");
                return;
            }

            List<string> correctAnswers = questionManager.CurrentQuestion.correct_answers
                .Select(answer => answer.answer)
                .ToList();

            List<string> allAnswers = new List<string>(correctAnswers);

            // Obtener distractores desde el diccionario
            List<string> distractors = questionManager.GetRandomDistractors(10 - correctAnswers.Count);
            allAnswers.AddRange(distractors);
            allAnswers = allAnswers.OrderBy(a => Random.value).ToList(); // Mezclar respuestas

            for (int i = 0; i < Mathf.Min(answerPositions.Length, allAnswers.Count); i++)
            {
                GameObject answerObj = Instantiate(answerPrefab, answerPositions[i].position, Quaternion.identity);
                Answer answerScript = answerObj.GetComponent<Answer>();

                bool isCorrect = correctAnswers.Contains(allAnswers[i]);
                answerScript.SetAnswer(allAnswers[i], isCorrect);
                Debug.Log($"📢 Instanciando respuesta: '{allAnswers[i]}' | Correcta: {isCorrect}");
            }
        }

        /// <summary>
        /// 🚨 Terminar la Fase 2 y calcular la puntuación final de la ronda
        /// </summary>
        public void EndPhase2_()
        {
            Debug.Log("📢 Finalizando Fase 2...");
            phase2Active = false;

            // 🔥 Eliminar todas las respuestas de la escena
            DestroyAllAnswers();

            // 🔢 Obtener la puntuación específica de la Fase 2
            int phase2Score = forgeScoreManager.GetPhase2Score();
            Debug.Log($"🏆 Puntuación Fase 2: {phase2Score}/{forgeScoreManager.GetTotalAnswers()}");

            // 🖥️ Actualizar la UI
            uiManager.UpdateLiveScore(forgeScoreManager.GetPhase1Score(), phase2Score);

            // 🔄 Pasar a la siguiente ronda con tiempo de espera
            StartCoroutine(WaitBeforeNewRound());
        }





        private void DestroyAllAnswers()
        {
            Answer[] answers = FindObjectsOfType<Answer>(); // Encuentra todos los AnswerPrefab en la escena

            foreach (Answer answer in answers)
            {
                Destroy(answer.gameObject); // Destruye cada respuesta encontrada
            }

            Debug.Log($"🗑️ Eliminados {answers.Length} AnswerPrefabs antes de iniciar una nueva ronda.");
        }


        /// <summary>
        /// 🎓 El profesor finaliza manualmente la Fase 1
        /// </summary>
        public void EndPhase1Manually()
        {
            Debug.Log("🎓 El profesor ha terminado la Fase 1 manualmente.");
            EndPhase1();
        }

        /// <summary>
        /// 🎓 El profesor finaliza manualmente la Fase 2
        /// </summary>
        public void EndPhase2Manually()
        {
            Debug.Log("🎓 El profesor ha terminado la Fase 2 manualmente.");
            EndPhase2();
        }

        /// <summary>
        /// 🛑 Finaliza el juego y muestra resultados finales
        /// </summary>
        public void EndGame()
        {
            Debug.Log("🛑 El profesor ha terminado el juego.");
            uiManager.ShowFinalGameResults(roundScores);
        }




        private void Update()
        {
            if (isPaused || uiManager == null) return; // ⏸ Evita que se ejecute si está pausado o uiManager es null

            if (phase1Active || phase2Active)
            {
                roundTimer -= Time.deltaTime;

                if (uiManager != null)
                {
                    uiManager.UpdateRoundTimer(roundTimer);
                }
            }
        }


        /// <summary>
        /// ⏸ Pausa o reanuda la sesión.
        /// </summary>
        public void TogglePause()
        {
            isPaused = !isPaused; // Alternar estado de pausa
            Debug.Log(isPaused ? "⏸ Juego pausado." : "▶ Juego reanudado.");
        }

        /// <summary>
        /// 🔄 Reinicia la ronda actual con la misma pregunta.
        /// </summary>
        /// 



    }




    /// <summary>
    /// 📊 Estructura para almacenar los datos de cada ronda
    /// </summary>
    public class RoundData
    {
        public int roundNumber;
        public int phase1Score;
        public int phase2Score;
        public int totalScore;

        public RoundData(int round, int phase1, int phase2, int total)
        {
            roundNumber = round;
            phase1Score = phase1;
            phase2Score = phase2;
            totalScore = total;
        }
    }
}
