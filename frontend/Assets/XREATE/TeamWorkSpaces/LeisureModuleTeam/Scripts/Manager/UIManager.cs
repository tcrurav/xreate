using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TeamWorkSpaces.LeisureModule
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("🔹 Control de Ronda")]
        public Button startRoundButton;
        public Button restartRoundButton;
        public Button pauseButton;
        public Button endGameButton;

        [Header("🔹 UI - Phase Indicator")]
        public TextMeshProUGUI phaseIndicatorText;

        [Header("🔹 UI - Phase Instructions")]
        public TextMeshProUGUI phaseInstructionsText;


        [Header("🔹 Ajuste de Temporizadores")]
        public Button increasePhase1Button;
        public Button decreasePhase1Button;
        public Button increasePhase2Button;
        public Button decreasePhase2Button;
        public Button increaseTransitionButton;
        public Button decreaseTransitionButton;
        public Button increaseRoundTimeButton;
        public Button decreaseRoundTimeButton;

        [Header("🔹 UI Elements")]
        public TextMeshProUGUI[] phase1ScoreTexts;
        public TextMeshProUGUI[] phase2ScoreTexts;
        public TextMeshProUGUI[] totalScoreTexts;
        public TextMeshProUGUI[] timerTexts;
        public TextMeshProUGUI[] roundTexts;
        public TextMeshProUGUI roundScoreText;
        public TextMeshProUGUI roundTimerText;
        public GameObject resultsCanvas;
        public Button endPhase1Button;
        public Button endPhase2Button;

        [Header("🔹 Panel de Resultados (Text grande)")]
        public TextMeshProUGUI resultsPanelText;

        [Header("🔹 Phase 1 UI - Words")]
        public TextMeshProUGUI totalWordsPlacedText;
        public TextMeshProUGUI correctWordsText;
        public TextMeshProUGUI incorrectWordsText;
        public TextMeshProUGUI phase1ScoreText;
        public TextMeshProUGUI phase1PrecisionText;

        [Header("🔹 Phase 2 UI - Answers")]
        public TextMeshProUGUI totalAnswersPlacedText;
        public TextMeshProUGUI correctAnswersText;
        public TextMeshProUGUI incorrectAnswersText;
        public TextMeshProUGUI phase2ScoreText;
        public TextMeshProUGUI phase2PrecisionText;

        // Para tracking de rondas (si lo usas desde GameSessionManager)
        private int totalScore = 0;
        private List<RoundData> roundScores = new List<RoundData>();

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

            // Ocultar pantalla de resultados al inicio
            if (resultsCanvas != null)
            {
                resultsCanvas.SetActive(false);
            }

            // Vincular botones a métodos
            SetupButtons();
        }

        private void Start()
        {
            CheckRequiredReferences();
        }

        /// <summary>
        /// Asocia los eventos de los botones a métodos.
        /// </summary>
        private void SetupButtons()
        {
            // Botones de fase manual
            if (endPhase1Button != null)
                endPhase1Button.onClick.AddListener(() => FindFirstObjectByType<GameSessionManager>()?.EndPhase1Manually());

            if (endPhase2Button != null)
                endPhase2Button.onClick.AddListener(() => FindFirstObjectByType<GameSessionManager>()?.EndPhase2Manually());

            if (endGameButton != null)
                endGameButton.onClick.AddListener(() => FindFirstObjectByType<GameSessionManager>()?.EndGame());

            // Botones de control de ronda
            if (startRoundButton != null)
                startRoundButton.onClick.AddListener(() => FindObjectOfType<GameSessionManager>().StartNewRound());

            if (restartRoundButton != null)
                restartRoundButton.onClick.AddListener(() => FindObjectOfType<GameSessionManager>().RestartCurrentRound());

            if (pauseButton != null)
                pauseButton.onClick.AddListener(() => FindObjectOfType<GameSessionManager>().TogglePause());

            // Ajuste de temporizadores
            if (increasePhase1Button != null)
                increasePhase1Button.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().phase1TimeLimit, 10));

            if (decreasePhase1Button != null)
                decreasePhase1Button.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().phase1TimeLimit, -10));

            if (increasePhase2Button != null)
                increasePhase2Button.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().phase2TimeLimit, 10));

            if (decreasePhase2Button != null)
                decreasePhase2Button.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().phase2TimeLimit, -10));

            if (increaseTransitionButton != null)
                increaseTransitionButton.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().transitionTimeBetweenPhases, 10));

            if (decreaseTransitionButton != null)
                decreaseTransitionButton.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().transitionTimeBetweenPhases, -10));

            if (increaseRoundTimeButton != null)
                increaseRoundTimeButton.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().totalRoundTimeLimit, 10));

            if (decreaseRoundTimeButton != null)
                decreaseRoundTimeButton.onClick.AddListener(() => AdjustTime(ref FindObjectOfType<GameSessionManager>().totalRoundTimeLimit, -10));
        }


        public void UpdatePhaseInstructions(string phaseTitle, string instructions)
        {
            if (phaseInstructionsText != null)
            {
                phaseInstructionsText.text = $"<b>{phaseTitle}</b>\n\n{instructions}";
                Debug.Log($"[UIManager] 📜 Instrucciones de fase actualizadas: {phaseTitle}");
            }
            else
            {
                Debug.LogError("[UIManager] ❌ El texto de instrucciones de fase no está asignado en el Inspector.");
            }
        }


        /// <summary>
        /// Chequea si los TextMeshPro obligatorios están asignados.
        /// </summary>
        private void CheckRequiredReferences()
        {
            // Fase 1
            if (!totalWordsPlacedText) Debug.LogError("[UIManager] ❌ Falta asignar totalWordsPlacedText.");
            if (!correctWordsText) Debug.LogError("[UIManager] ❌ Falta asignar correctWordsText.");
            if (!incorrectWordsText) Debug.LogError("[UIManager] ❌ Falta asignar incorrectWordsText.");
            if (!phase1ScoreText) Debug.LogError("[UIManager] ❌ Falta asignar phase1ScoreText.");
            if (!phase1PrecisionText) Debug.LogError("[UIManager] ❌ Falta asignar phase1PrecisionText.");

            // Fase 2
            if (!totalAnswersPlacedText) Debug.LogError("[UIManager] ❌ Falta asignar totalAnswersPlacedText.");
            if (!correctAnswersText) Debug.LogError("[UIManager] ❌ Falta asignar correctAnswersText.");
            if (!incorrectAnswersText) Debug.LogError("[UIManager] ❌ Falta asignar incorrectAnswersText.");
            if (!phase2ScoreText) Debug.LogError("[UIManager] ❌ Falta asignar phase2ScoreText.");
            if (!phase2PrecisionText) Debug.LogError("[UIManager] ❌ Falta asignar phase2PrecisionText.");

            // Panel de Resultados Adicional (Opcional)
            if (!resultsPanelText)
                Debug.Log("[UIManager] ℹ️ No has asignado resultsPanelText, no se mostrará el panel grande de resultados.");
        }

        /// <summary>
        /// Ajusta el tiempo de las fases y la ronda en incrementos/decrementos.
        /// </summary>
        private void AdjustTime(ref float timer, int amount)
        {
            timer = Mathf.Max(20, timer + amount); // Mínimo 20 segundos
            Debug.Log($"⏳ Nuevo tiempo ajustado: {timer} segundos.");
        }

        // =============================================================
        // =======================   ACTUALIZACIONES   =================
        // =============================================================

        /// <summary>
        /// Actualiza la UI con el número de ronda.
        /// </summary>
        public void UpdateRoundInfo(int roundNumber)
        {
            foreach (var rt in roundTexts)
            {
                if (rt)
                    rt.text = $"🔹 Round: {roundNumber}";
            }
        }

        /// <summary>
        /// Actualiza el contador de tiempo principal (fase 1 o fase 2).
        /// </summary>
        public void UpdateTimerUI(float timeRemaining)
        {
            string timeFormatted = $"{(int)timeRemaining / 60:D2}:{(int)timeRemaining % 60:D2}";
            foreach (var t in timerTexts)
            {
                if (t)
                    t.text = $"⏳ Time Left: {timeFormatted}";
            }
        }

        /// <summary>
        /// Actualiza el temporizador global de la ronda.
        /// </summary>
        public void UpdateRoundTimer(float roundTimeRemaining)
        {
            if (!roundTimerText) return;

            string formatted = $"{(int)roundTimeRemaining / 60:D2}:{(int)roundTimeRemaining % 60:D2}";
            roundTimerText.text = $"⏳ Round Time: {formatted}";
        }

        /// <summary>
        /// Actualiza y/o Muestra la tabla con las puntuaciones de las rondas pasadas.
        /// </summary>
        public void UpdateScoreUI(List<RoundData> rounds)
        {
            roundScores = rounds;

            if (!roundScoreText) return;

            string scoreboard = "🏆 Scoreboard 🏆\n\n";
            scoreboard += "Round | Phase 1 | Phase 2 | Total\n";
            scoreboard += "----------------------------------\n";

            foreach (var round in rounds)
            {
                scoreboard += $"{round.roundNumber}  |  {round.phase1Score}  |  {round.phase2Score}  |  {round.totalScore}\n";
            }

            roundScoreText.text = scoreboard;

            // Asegurarte de mostrar el Canvas de resultados
            if (resultsCanvas) resultsCanvas.SetActive(true);
        }

        // =============================================================
        // =======================   FASE 1 - WORDS   ==================
        // =============================================================

        /// <summary>
        /// Muestra los datos de la Fase 1 (palabras).
        /// </summary>
        public void UpdatePhase1UI(int totalWordsPlaced, int correctWords, int incorrectWords)
        {
            float precision = (float)correctWords / Mathf.Max(1, totalWordsPlaced) * 100f;
            int score = Mathf.RoundToInt(((float)correctWords / Mathf.Max(1, totalWordsPlaced)) * 10);

/*            if (totalWordsPlacedText) totalWordsPlacedText.text = $"Total: {totalWordsPlaced}";
            if (correctWordsText) correctWordsText.text = $"✅ Correctas: {correctWords}";
            if (incorrectWordsText) incorrectWordsText.text = $"❌ Incorrectas: {incorrectWords}";
            if (phase1ScoreText) phase1ScoreText.text = $"🏆 Puntuación: {score}/10";
            if (phase1PrecisionText) phase1PrecisionText.text = $"🎯 Precisión: {precision:F1}%";*/


            if (totalWordsPlacedText) totalWordsPlacedText.text = $"{totalWordsPlaced}";
            if (correctWordsText) correctWordsText.text = $"{correctWords}";
            if (incorrectWordsText) incorrectWordsText.text = $"{incorrectWords}";
            if (phase1ScoreText) phase1ScoreText.text = $"{score}/10";
            if (phase1PrecisionText) phase1PrecisionText.text = $"{precision:F1}%";

            Debug.Log($"[UIManager] 📊 Fase 1 - totalWordsPlaced={totalWordsPlaced}, correctWords={correctWords}, " +
                      $"incorrectWords={incorrectWords}, score={score}/10, precision={precision:F1}%");
        }

        // =============================================================
        // =======================   FASE 2 - ANSWERS  =================
        // =============================================================

        public void UpdatePhase2UI(int totalAnswersPlaced, int correctAnswers, int incorrectAnswers)
        {
            float precision = (float)correctAnswers / Mathf.Max(1, totalAnswersPlaced) * 100f;
            int score = Mathf.RoundToInt(((float)correctAnswers / Mathf.Max(1, totalAnswersPlaced)) * 10);

/*            if (totalAnswersPlacedText) totalAnswersPlacedText.text = $"Total: {totalAnswersPlaced}";
            if (correctAnswersText) correctAnswersText.text = $"✅ Correctas: {correctAnswers}";
            if (incorrectAnswersText) incorrectAnswersText.text = $"❌ Incorrectas: {incorrectAnswers}";
            if (phase2ScoreText) phase2ScoreText.text = $"🏆 Puntuación: {score}/10";
            if (phase2PrecisionText) phase2PrecisionText.text = $"🎯 Precisión: {precision:F1}%";*/


            if (totalAnswersPlacedText) totalAnswersPlacedText.text = $"{totalAnswersPlaced}";
            if (correctAnswersText) correctAnswersText.text = $"{correctAnswers}";
            if (incorrectAnswersText) incorrectAnswersText.text = $"{incorrectAnswers}";
            if (phase2ScoreText) phase2ScoreText.text = $"{score}/10";
            if (phase2PrecisionText) phase2PrecisionText.text = $"{precision:F1}%";

            Debug.Log($"[UIManager] 📊 Fase 2 - totalAnswersPlaced={totalAnswersPlaced}, correctAnswers={correctAnswers}, " +
                      $"incorrectAnswers={incorrectAnswers}, score={score}/10, precision={precision:F1}%");
        }

        // =============================================================
        // ======================   SCORES DE FASES   ==================
        // =============================================================

        /// <summary>
        /// Muestra la puntuación final de cada Fase (1 y 2) con normalización a 10.
        /// </summary>
        public void UpdateLiveScore(int phase1Score, int phase2Score)
        {
            // Actualiza los textos de Phase1Score /10 y Phase2Score /10
            foreach (var txt in phase1ScoreTexts)
            {
                if (txt)
                    txt.text = $"Phase 1 Score: {phase1Score}/10";
            }

            foreach (var txt in phase2ScoreTexts)
            {
                if (txt)
                    txt.text = $"Phase 2 Score: {phase2Score}/10";
            }

            int total = phase1Score + phase2Score; // Max 20
            foreach (var txt in totalScoreTexts)
            {
                if (txt)
                    txt.text = $"Total Score: {total}/20";
            }

            Debug.Log($"[UIManager] 🎯 Updated Scores => Phase1: {phase1Score}/10, Phase2: {phase2Score}/10, Total: {total}/20");
        }

        /// <summary>
        /// Almacena la puntuación final en la variable totalScore acumulada.
        /// </summary>
        public void ShowFinalScore(int phase1Score, int phase2Score)
        {
            int roundScore = phase1Score + phase2Score;
            totalScore += roundScore;

            UpdateLiveScore(phase1Score, phase2Score);
            Debug.Log($"[UIManager] 🏆 ShowFinalScore => Phase1: {phase1Score}, Phase2: {phase2Score}, RoundScore: {roundScore}, GlobalTotal: {totalScore}");
        }

        // =============================================================
        // ====================   RESULTADOS FINALES   =================
        // =============================================================

        /// <summary>
        /// Muestra un panel final con los resultados de todas las rondas.
        /// </summary>
        public void ShowFinalGameResults(List<RoundData> rounds)
        {
            int finalTotal = 0;
            string finalResults = "🏆 FINAL GAME RESULTS 🏆\n\n";
            finalResults += "Round | Phase 1 | Phase 2 | Total\n";
            finalResults += "----------------------------------\n";

            foreach (var round in rounds)
            {
                finalResults += $"{round.roundNumber}  |   {round.phase1Score}/10    |   {round.phase2Score}/10    |   {round.totalScore}/20\n";
                finalTotal += round.totalScore;
            }

            finalResults += $"\n🎯 GRAND TOTAL: {finalTotal} POINTS!";

            // Actualiza el text principal de scoreboard
            if (roundScoreText != null)
            {
                roundScoreText.text = finalResults;
            }

            // También, si quieres, en un panel adicional
            if (resultsPanelText != null)
            {
                resultsPanelText.text = finalResults;
            }

            // Aseguramos que el Canvas se muestre
            if (resultsCanvas != null)
                resultsCanvas.SetActive(true);

            Debug.Log($"[UIManager] 🏆 ShowFinalGameResults => finalTotal: {finalTotal} points");
        }

        /// <summary>
        /// Oculta la pantalla de resultados.
        /// </summary>
        public void HideResults()
        {
            if (resultsCanvas != null)
            {
                resultsCanvas.SetActive(false);
                Debug.Log("[UIManager] ❌ Ocultando pantalla de resultados.");
            }
        }

        // =============================================================
        // ===================   DISPLAYRESULTS  METODO   ==============
        // =============================================================

        /// <summary>
        /// Método unificado que recibe TODO y lo muestra en la UI (Fase 1 + Fase 2)
        /// Úsalo cuando terminen las dos fases o quieras ver todo junto.
        /// </summary>
        public void DisplayResultsInPanels(int totalWords, int correctWords, int incorrectWords, int phase1Score,
                                           int totalAnswers, int correctAnswers, int incorrectAnswers, int phase2Score)
        {
            Debug.Log($"[UIManager] ⏳ Llamando DisplayResultsInPanels");
            // -- FASE 1 --
            if (totalWordsPlacedText)
                totalWordsPlacedText.text = $"{totalWords}";
            if (correctWordsText)
                correctWordsText.text = $"{correctWords}";
            if (incorrectWordsText)
                incorrectWordsText.text = $"{incorrectWords}";
            if (phase1ScoreText)
                phase1ScoreText.text = $"{phase1Score}/10";

            // -- FASE 2 --
            if (totalAnswersPlacedText)
                totalAnswersPlacedText.text = $"{totalAnswers}";
            if (correctAnswersText)
                correctAnswersText.text = $"{correctAnswers}";
            if (incorrectAnswersText)
                incorrectAnswersText.text = $"{incorrectAnswers}";
            if (phase2ScoreText)
                phase2ScoreText.text = $"{phase2Score}/10";

            // Log detallado para debug
            Debug.Log($"[UIManager] Fase1 => Total {totalWords}, Correctas {correctWords}, Incorrectas {incorrectWords}, Score {phase1Score}/10");
            Debug.Log($"[UIManager] Fase2 => Total {totalAnswers}, Correctas {correctAnswers}, Incorrectas {incorrectAnswers}, Score {phase2Score}/10");

            // Panel "grande" opcional
            if (resultsPanelText)
            {
                string panelText =
                    $"----------------------------------\n" +
                    $"📌 FASE 1 - RESULTADOS\n" +
                    $"----------------------------------\n" +
                    $"Total palabras colocadas: {totalWords}\n" +
                    $"✅ Palabras correctas: {correctWords}\n" +
                    $"❌ Palabras incorrectas: {incorrectWords}\n" +
                    $"🏆 Puntuación: {phase1Score}/10\n\n" +
                    $"----------------------------------\n" +
                    $"📌 FASE 2 - RESULTADOS\n" +
                    $"----------------------------------\n" +
                    $"Total respuestas colocadas: {totalAnswers}\n" +
                    $"✅ Respuestas correctas: {correctAnswers}\n" +
                    $"❌ Respuestas incorrectas: {incorrectAnswers}\n" +
                    $"🏆 Puntuación: {phase2Score}/10\n";

                resultsPanelText.text = panelText;
            }

            // Habilita el canvas si quieres asegurarte de que se vea
            if (resultsCanvas)
                resultsCanvas.SetActive(true);
        }


        public void DisplayResultsInPanels1(int totalWords, int correctWords, int incorrectWords, int phase1Score,
                                   int totalAnswers, int correctAnswers, int incorrectAnswers, int phase2Score)
        {
            Debug.Log($"[UIManager] ⏳ Llamando DisplayResultsInPanels");
            // -- FASE 1 --
            if (totalWordsPlacedText)
                totalWordsPlacedText.text = $"Total: {totalWords}";
            if (correctWordsText)
                correctWordsText.text = $"✅ Correctas: {correctWords}";
            if (incorrectWordsText)
                incorrectWordsText.text = $"❌ Incorrectas: {incorrectWords}";
            if (phase1ScoreText)
                phase1ScoreText.text = $"🏆 Puntuación: {phase1Score}/10";

            // -- FASE 2 --
            if (totalAnswersPlacedText)
                totalAnswersPlacedText.text = $"Total: {totalAnswers}";
            if (correctAnswersText)
                correctAnswersText.text = $"✅ Correctas: {correctAnswers}";
            if (incorrectAnswersText)
                incorrectAnswersText.text = $"❌ Incorrectas: {incorrectAnswers}";
            if (phase2ScoreText)
                phase2ScoreText.text = $"🏆 Puntuación: {phase2Score}/10";

            // Log detallado para debug
            Debug.Log($"[UIManager] Fase1 => Total {totalWords}, Correctas {correctWords}, Incorrectas {incorrectWords}, Score {phase1Score}/10");
            Debug.Log($"[UIManager] Fase2 => Total {totalAnswers}, Correctas {correctAnswers}, Incorrectas {incorrectAnswers}, Score {phase2Score}/10");

            // Panel "grande" opcional
            if (resultsPanelText)
            {
                string panelText =
                    $"----------------------------------\n" +
                    $"📌 FASE 1 - RESULTADOS\n" +
                    $"----------------------------------\n" +
                    $"Total palabras colocadas: {totalWords}\n" +
                    $"✅ Palabras correctas: {correctWords}\n" +
                    $"❌ Palabras incorrectas: {incorrectWords}\n" +
                    $"🏆 Puntuación: {phase1Score}/10\n\n" +
                    $"----------------------------------\n" +
                    $"📌 FASE 2 - RESULTADOS\n" +
                    $"----------------------------------\n" +
                    $"Total respuestas colocadas: {totalAnswers}\n" +
                    $"✅ Respuestas correctas: {correctAnswers}\n" +
                    $"❌ Respuestas incorrectas: {incorrectAnswers}\n" +
                    $"🏆 Puntuación: {phase2Score}/10\n";

                resultsPanelText.text = panelText;
            }

            // Habilita el canvas si quieres asegurarte de que se vea
            if (resultsCanvas)
                resultsCanvas.SetActive(true);
        }
    }
}
