using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;      // Question text
        public List<string> answers;     // Answer options
        public int correctAnswerIndex;   // Index of the correct answer
    }

    public List<GameObject> welcomePanels;  // List of welcome panels
    public List<Button> startButtons;       // List of start buttons

    public List<GameObject> quizPanels;     // List of quiz panels
    public List<TMP_Text> questionTexts;    // List of question texts
    public List<TMP_Text> feedbackTexts;    // List of feedback texts
    public List<TMP_Text> timerTexts;       // List of timer texts
    public List<TMP_Text> playerScoreTexts; // List of player score texts
    public List<TMP_Text> teamScoreTexts;   // List of team score texts

    public List<Button> answerButtons;     // List of answer buttons

    public Sprite defaultSprite; // Default sprite

    public Sprite correctSprite;     // Sprite for correct answer
    public Sprite incorrectSprite;   // Sprite for incorrect answer
    public AudioClip correctSound;   // Sound for correct answer
    public AudioClip incorrectSound; // Sound for incorrect answer
    private AudioSource audioSource; // AudioSource component to play sounds

    private List<Question> questions = new List<Question>();         // List of questions
    private List<Question> selectedQuestions = new List<Question>(); // List of randomly selected questions
    private Question currentQuestion;                                // Current question
    private int currentQuestionIndex = 0;

    private int playersReady = 0; // Counter of ready players
    private int totalPlayers = 5; // Total number of players (configurable)

    private int teamScore = 0; // Team score
    private List<int> playerScores = new List<int>(); // Player scores

    private float timer = 5.0f; // Time to answer each question
    private bool isTimerRunning = false; // Indicator if the timer is active

    // Variables to submit points to the API
    public string challengeName; // Challenge name
    public string challengeItemItem; // Challenge item name

    private AchievementItemService achievementItemService; // Service to submit points

    void Start()
    {
        // Initialize the points service
        achievementItemService = gameObject.AddComponent<AchievementItemService>();

        // Initialize my AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Initialize scores for all players
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0);
        }

        // Hide all quiz and welcome panels at the start
        foreach (var panel in quizPanels)
        {
            panel.SetActive(false);
        }

        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        // Set up the number of players
        if (totalPlayers < 2)
        {
            Debug.LogError("The number of players must be at least 2.");
            return;
        }

        // Show panels and assign events to the start buttons
        for (int i = 0; i < totalPlayers; i++)
        {
            welcomePanels[i].SetActive(true);
            startButtons[i].onClick.AddListener(OnPlayerReady);
        }
    }

    // Method to submit points to the API
    private IEnumerator OnQuizCompletedUpdatePoints(int points)
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            throw new System.Exception("Error: Only students get points");
        }

        int studentId = MainManager.GetUser().id;
        int activityId = CurrentActivityManager.GetCurrentActivityId();

        yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
             challengeName, challengeItemItem, studentId, activityId, points);

        if (achievementItemService.responseCode != 200)
        {
            yield break;
        }

    }

    // Method called when a player presses the start button
    void OnPlayerReady()
    {
        playersReady++;
        if (playersReady == totalPlayers)
        {
            Invoke("StartQuiz", 1.5f);
        }
    }

    // Starts the quiz game
    void StartQuiz()
    {
        // Hide welcome panels and show quiz panels
        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        for (int i = 0; i < totalPlayers; i++)
        {
            quizPanels[i].SetActive(true);
        }

        InitializeQuestions();
        SelectRandomQuestions(); // Select 10 random questions
        LoadQuestion();
    }

    // Initialize questions and answers
    void InitializeQuestions()
    {
        questions.Add(new Question
        {
            questionText = "¿Qué es Phishing?",
            answers = new List<string>
        {
            "A: Un método de ataque que busca obtener información personal mediante engaños.",
            "B: Un tipo de malware que infecta sistemas operativos.",
            "C: Un ataque para bloquear el acceso a una red.",
            "D: Un método para interceptar comunicaciones cifradas.",
            "E: Un sistema para mejorar la seguridad de contraseñas."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué hace un Ransomware?",
            answers = new List<string>
        {
            "A: Roba credenciales de acceso.",
            "B: Cifra datos del usuario y pide un rescate para devolverlos.",
            "C: Infecta el hardware del equipo.",
            "D: Monitorea la actividad en el navegador web.",
            "E: Cambia las configuraciones del sistema sin permiso."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Troyano?",
            answers = new List<string>
        {
            "A: Un tipo de virus diseñado para replicarse.",
            "B: Una herramienta de protección contra malware.",
            "C: Un programa malicioso disfrazado de software legítimo.",
            "D: Un ataque diseñado para saturar una red.",
            "E: Un sistema para detectar actividad sospechosa."
        },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque de fuerza bruta?",
            answers = new List<string>
        {
            "A: Un ataque que prueba todas las combinaciones posibles para descifrar contraseñas.",
            "B: Una estrategia para engañar a un usuario mediante ingeniería social.",
            "C: Un virus que borra datos automáticamente.",
            "D: Un método para obtener acceso físico a servidores.",
            "E: Una técnica para manipular bases de datos."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Botnet?",
            answers = new List<string>
        {
            "A: Un programa antivirus avanzado.",
            "B: Una red de dispositivos infectados controlados de forma remota.",
            "C: Una técnica para realizar phishing.",
            "D: Un método para proteger la privacidad en línea.",
            "E: Un sistema para evitar ataques DoS."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque DoS (Denial of Service)?",
            answers = new List<string>
        {
            "A: Un intento de explotar vulnerabilidades de hardware.",
            "B: Un ataque que roba credenciales de usuarios.",
            "C: Un malware diseñado para cifrar datos personales.",
            "D: Un ataque que bloquea un servicio al saturarlo con solicitudes.",
            "E: Un método de redirección maliciosa en navegadores."
        },
            correctAnswerIndex = 3
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Rootkit?",
            answers = new List<string>
        {
            "A: Un software que permite acceso oculto a un sistema y evita su detección.",
            "B: Una herramienta de monitoreo de red.",
            "C: Un ataque DoS avanzado.",
            "D: Un programa para cifrar datos personales.",
            "E: Un sistema de control remoto para servidores."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un certificado SSL?",
            answers = new List<string>
        {
            "A: Un ataque para descifrar datos cifrados.",
            "B: Un sistema de autenticación para comunicaciones cifradas.",
            "C: Un sistema para realizar ingeniería social avanzada.",
            "D: Un estándar de seguridad para redes locales.",
            "E: Un tipo de malware diseñado para servidores."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque SQL Injection?",
            answers = new List<string>
        {
            "A: Un método para prevenir el uso de contraseñas débiles.",
            "B: Una técnica para realizar ataques de phishing.",
            "C: Un ataque que inyecta código malicioso en bases de datos a través de consultas.",
            "D: Una vulnerabilidad que permite acceso remoto no autorizado.",
            "E: Una técnica para deshabilitar configuraciones de seguridad."
        },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "¿Qué significa el término Spoofing?",
            answers = new List<string>
        {
            "A: Un ataque diseñado para robar datos personales mediante redes sociales.",
            "B: Un tipo de malware que infecta dispositivos móviles.",
            "C: Un método para cifrar datos sensibles.",
            "D: Un software para controlar dispositivos remotamente.",
            "E: Una técnica para falsificar la identidad o dirección de origen."
        },
            correctAnswerIndex = 4
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque de Ingeniería Social?",
            answers = new List<string>
        {
            "A: Un ataque técnico dirigido a sistemas operativos.",
            "B: Una estrategia para manipular a las personas y obtener información confidencial.",
            "C: Una técnica para proteger información personal.",
            "D: Un tipo de malware diseñado para redes sociales.",
            "E: Un sistema de detección de intrusos."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque Man-in-the-Middle (MITM)?",
            answers = new List<string>
        {
            "A: Un ataque donde un tercero intercepta y manipula la comunicación entre dos partes.",
            "B: Un malware que se propaga automáticamente en una red.",
            "C: Un software que permite descifrar contraseñas.",
            "D: Un método para evitar conexiones seguras.",
            "E: Un sistema para engañar a usuarios mediante ingeniería social."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es el Criptojacking?",
            answers = new List<string>
        {
            "A: Un ataque que roba criptomonedas directamente de carteras digitales.",
            "B: Un ataque que utiliza recursos de dispositivos infectados para minar criptomonedas.",
            "C: Una técnica de protección contra ransomware.",
            "D: Un software para realizar pagos seguros.",
            "E: Un ataque para deshabilitar la red de minería."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Zero-Day?",
            answers = new List<string>
        {
            "A: Una vulnerabilidad desconocida explotada antes de que el desarrollador lance un parche.",
            "B: Un malware diseñado para desactivar software antivirus.",
            "C: Un sistema de monitoreo de tráfico de red.",
            "D: Un método para proteger contraseñas débiles.",
            "E: Un ataque dirigido a hardware obsoleto."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Keylogger?",
            answers = new List<string>
        {
            "A: Un malware que registra las pulsaciones del teclado.",
            "B: Un ataque para bloquear redes sociales.",
            "C: Una técnica de recuperación de datos.",
            "D: Un sistema para encriptar contraseñas automáticamente.",
            "E: Un ataque diseñado para desactivar firewalls."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque de Spear Phishing?",
            answers = new List<string>
        {
            "A: Una versión más específica y dirigida de un ataque de phishing.",
            "B: Un ataque para deshabilitar contraseñas cifradas.",
            "C: Un software de monitoreo avanzado.",
            "D: Una técnica para infectar bases de datos.",
            "E: Un sistema para rastrear correos electrónicos falsificados."
        },
            correctAnswerIndex = 0
        });

    }


    // Select 10 random questions
    void SelectRandomQuestions()
    {
        List<Question> tempQuestions = new List<Question>(questions);
        for (int i = 0; i < 10 && tempQuestions.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, tempQuestions.Count);
            selectedQuestions.Add(tempQuestions[randomIndex]);
            tempQuestions.RemoveAt(randomIndex);
        }
    }

    // Upload a question and answers to the panels
    void LoadQuestion()
    {
        if (currentQuestionIndex >= selectedQuestions.Count)
        {
            foreach (var text in questionTexts)
            {
                text.text = "Game over. Team score: " + teamScore;
            }

            // Find the player with the highest score
            int maxScore = playerScores.Max();
            int bestPlayerIndex = playerScores.ToList().IndexOf(maxScore);

            for (int i = 0; i < feedbackTexts.Count; i++)
            {
                if (i == bestPlayerIndex)
                {
                    feedbackTexts[i].text = $"Congratulations! You’ve been the MVP, leading your team with the highest score.";
                }
                else
                {
                    feedbackTexts[i].text = ""; // Leave empty so only the mvp can see the message.
                }
            }

            // Call the method to introduce points to the API
            StartCoroutine(OnQuizCompletedUpdatePoints(teamScore));

            DisableButtons();
            return;
        }

        foreach (var button in answerButtons)
        {
            button.image.sprite = defaultSprite; // Set my default sprite.
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];

        foreach (var feedback in feedbackTexts)
        {
            feedback.text = ""; // Clear feedback messages
        }

        EnableButtons();

        // Show the question
        foreach (var text in questionTexts)
        {
            text.text = currentQuestion.questionText;
        }

        // Mix up the answers and assign them to the panels
        List<int> numbers = new List<int> { 0, 1, 2, 3, 4 };
        System.Random random = new System.Random(); // Random Number Generator

        // Remove the index of the correct answer
        numbers.Remove(currentQuestion.correctAnswerIndex);

        // Create the mixed response list
        List<int> answers = new List<int>();

        // Mixing the rates of incorrect answers
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // Add incorrect answers
        for (int i = 0; i < totalPlayers - 1; i++)
        {
            answers.Add(numbers[i]);
        }

        // Add the correct answer
        answers.Add(currentQuestion.correctAnswerIndex);

        // Shuffle the final answers again
        answers = answers.OrderBy(x => random.Next()).ToList();

        // Assign responses to text panels
        for (int i = 0; i < totalPlayers; i++)
        {
            feedbackTexts[i].text = currentQuestion.answers[answers[i]];
        }

        timer = 5.0f; // Reset the stopwatch
        isTimerRunning = true;

        // Clean and add listeners to buttons
        for (int i = 0; i < totalPlayers; i++)
        {
            int index = i;
            // Clear any old listeners
            answerButtons[i].onClick.RemoveAllListeners();
            // Add the current listener
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index, answers[index]));
        }

    }

    // Method to check if the answer is correct
    public void CheckAnswer(int selectedIndex, int selectedAnswerIndex)
    {

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            DisableButtons();

            answerButtons[selectedIndex].image.sprite = correctSprite; // Change to green
            audioSource.PlayOneShot(correctSound); // Play correct sound
            answerButtons[selectedIndex].interactable = false;

            foreach (var feedback in feedbackTexts)
            {
                feedback.text = "Correct Answer!";
            }

            isTimerRunning = false; // Stop the stopwatch
            int points = timer > 0 ? 5 : 3; // 5 points if you responded on time, 3 if not
            teamScore += points; // Add points to the team

            foreach (var text in teamScoreTexts)
            {
                text.text = "Team Score: " + teamScore;
            }

            if (timer > 0) // Additional points for the player
            {
                playerScores[selectedIndex] += 2;
                playerScoreTexts[selectedIndex].text = "Player Score: " + playerScores[selectedIndex];
            }

            currentQuestionIndex++;
            Debug.Log("Current Question Index: " + currentQuestionIndex);
            Invoke("LoadQuestion", 2.0f);
        }
        else
        {
            answerButtons[selectedIndex].image.sprite = incorrectSprite; // Change to red
            audioSource.PlayOneShot(incorrectSound); // Play wrong sound
            feedbackTexts[selectedIndex].text = "Incorrect Answer. Please try again.";
            answerButtons[selectedIndex].interactable = false;
        }
    }

    // Method to Update the Stopwatch
    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false; // Stop the stopwatch
            }

            for (int i = 0; i < totalPlayers; i++)
            {
                timerTexts[i].text = "Tiempo: " + Mathf.Ceil(timer);
            }
        }
    }

    void DisableButtons()
    {
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }
    }

    void EnableButtons()
    {
        foreach (var button in answerButtons)
        {
            button.interactable = true;
        }
    }
}