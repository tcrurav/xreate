using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{

    [System.Serializable]
    public class Question
    {
        public string questionText;     // Texto de la pregunta
        public List<string> answers;    // Opciones de respuestas
        public int correctAnswerIndex;  // Índice de la respuesta correcta
    }

    public GameObject welcomePanelA;     // Panel de bienvenida
    public GameObject welcomePanelB;     // Panel de bienvenida
    public GameObject welcomePanelC;     // Panel de bienvenida
    public GameObject welcomePanelD;     // Panel de bienvenida
    public GameObject welcomePanelE;     // Panel de bienvenida

    public TMP_Text welcomeTextA;        // Texto de bienvenida
    public TMP_Text welcomeTextB;        // Texto de bienvenida
    public TMP_Text welcomeTextC;        // Texto de bienvenida
    public TMP_Text welcomeTextD;        // Texto de bienvenida
    public TMP_Text welcomeTextE;        // Texto de bienvenida

    public Button startButtonA;          // Botón de inicio
    public Button startButtonB;          // Botón de inicio
    public Button startButtonC;          // Botón de inicio
    public Button startButtonD;          // Botón de inicio
    public Button startButtonE;          // Botón de inicio

    public GameObject quizPanelA;        // Panel del quiz
    public GameObject quizPanelB;        // Panel del quiz
    public GameObject quizPanelC;        // Panel del quiz
    public GameObject quizPanelD;        // Panel del quiz
    public GameObject quizPanelE;        // Panel del quiz

    public TMP_Text questionTextPanelA; // Pregunta en el panel A
    public TMP_Text questionTextPanelB; // Pregunta en el panel B
    public TMP_Text questionTextPanelC; // Pregunta en el panel C
    public TMP_Text questionTextPanelD; // Pregunta en el panel D
    public TMP_Text questionTextPanelE; // Pregunta en el panel E

    public Button buttonPanelA;         // Botón del panel A
    public Button buttonPanelB;         // Botón del panel B
    public Button buttonPanelC;         // Botón del panel C
    public Button buttonPanelD;         // Botón del panel D
    public Button buttonPanelE;         // Botón del panel E

    public TMP_Text feedbackTextA;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextB;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextC;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextD;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextE;      // Texto de retroalimentación: Correcto/Incorrecto

    public TMP_Text timerTextA;          // Texto del cronómetro para el jugador A
    public TMP_Text timerTextB;          // Texto del cronómetro para el jugador B
    public TMP_Text timerTextC;          // Texto del cronómetro para el jugador C
    public TMP_Text timerTextD;          // Texto del cronómetro para el jugador D
    public TMP_Text timerTextE;          // Texto del cronómetro para el jugador E

    private List<Question> questions = new List<Question>();         // Lista de preguntas
    private List<Question> selectedQuestions = new List<Question>(); // Lista de preguntas seleccionadas aleatoriamente
    private Question currentQuestion;                                // Pregunta actual
    private int currentQuestionIndex = 0;

    private int playersReady = 0;       // Contador de jugadores listos
    public int totalPlayers = 5;        // Número total de jugadores

    private int teamScore = 0;          // Puntaje del equipo
    private int playerScoreA = 0;       // Puntaje del jugador A
    private int playerScoreB = 0;       // Puntaje del jugador B
    private int playerScoreC = 0;       // Puntaje del jugador C
    private int playerScoreD = 0;       // Puntaje del jugador D
    private int playerScoreE = 0;       // Puntaje del jugador E

    public TMP_Text teamScoreA;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreB;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreC;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreD;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreE;         // Puntaje del equipo para mostrar por pantalla

    public TMP_Text playerTextScoreA;   // Puntaje del jugador A para mostrar por pantalla
    public TMP_Text playerTextScoreB;   // Puntaje del jugador B para mostrar por pantalla
    public TMP_Text playerTextScoreC;   // Puntaje del jugador C para mostrar por pantalla
    public TMP_Text playerTextScoreD;   // Puntaje del jugador D para mostrar por pantalla
    public TMP_Text playerTextScoreE;   // Puntaje del jugador E para mostrar por pantalla

    private float timer = 5.0f;         // Tiempo para responder cada pregunta
    private bool isTimerRunning = false; // Indicador de si el cronómetro está activo

    void Start()
    {
        // Inicialmente se muestra la pantalla de bienvenida
        quizPanelA.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelB.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelC.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelD.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelE.SetActive(false); // Ocultar el panel de preguntas inicialmente

        welcomePanelA.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelB.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelC.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelD.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelE.SetActive(true); // Mostrar la pantalla de bienvenida

        startButtonA.onClick.AddListener(OnPlayerReady);
        startButtonB.onClick.AddListener(OnPlayerReady);
        startButtonC.onClick.AddListener(OnPlayerReady);
        startButtonD.onClick.AddListener(OnPlayerReady);
        startButtonE.onClick.AddListener(OnPlayerReady);
    }

    // Método llamado cuando un jugador presiona el botón start
    void OnPlayerReady()
    {
        playersReady++;
        if (playersReady >= totalPlayers)
        {
            Invoke("StartQuiz", 1.5f);
        }
    }

    // Inicia el juego de preguntas y respuestas
    void StartQuiz()
    {
        welcomePanelA.SetActive(false); // Ocultar la pantalla de bienvenida
        welcomePanelB.SetActive(false); // Ocultar la pantalla de bienvenida
        welcomePanelC.SetActive(false); // Ocultar la pantalla de bienvenida
        welcomePanelD.SetActive(false); // Ocultar la pantalla de bienvenida
        welcomePanelE.SetActive(false); // Ocultar la pantalla de bienvenida

        quizPanelA.SetActive(true);    // Mostrar el panel del quiz
        quizPanelB.SetActive(true);    // Mostrar el panel del quiz
        quizPanelC.SetActive(true);    // Mostrar el panel del quiz
        quizPanelD.SetActive(true);    // Mostrar el panel del quiz
        quizPanelE.SetActive(true);    // Mostrar el panel del quiz

        InitializeQuestions();
        SelectRandomQuestions(); // Selecciona 10 preguntas aleatorias
        LoadQuestion();
        AssignButtonListeners();
    }

    // Inicializar las preguntas y respuestas
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


    // Selecciona 10 preguntas aleatorias
    void SelectRandomQuestions()
    {
        List<Question> tempQuestions = new List<Question>(questions);
        for (int i = 0; i < 10; i++)
        {
            int randomIndex = Random.Range(0, tempQuestions.Count);
            selectedQuestions.Add(tempQuestions[randomIndex]);
            tempQuestions.RemoveAt(randomIndex);
        }
    }

    // Cargar una pregunta y respuestas en los paneles
    void LoadQuestion()
    {
        if (currentQuestionIndex >= selectedQuestions.Count)
        {
            questionTextPanelA.text = "Juego terminado. Puntuación del equipo: " + teamScore;
            questionTextPanelB.text = "Juego terminado. Puntuación del equipo: " + teamScore;
            questionTextPanelC.text = "Juego terminado. Puntuación del equipo: " + teamScore;
            questionTextPanelD.text = "Juego terminado. Puntuación del equipo: " + teamScore;
            questionTextPanelE.text = "Juego terminado. Puntuación del equipo: " + teamScore;

            feedbackTextA.text = ""; // Limpio el TMP Correspondiente a las preguntas
            feedbackTextB.text = "";
            feedbackTextC.text = "";
            feedbackTextD.text = "";
            feedbackTextE.text = "";

            DisableButtons();
            return;
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];
        feedbackTextA.text = ""; // Limpiar mensajes de retroalimentación
        feedbackTextB.text = "";
        feedbackTextC.text = "";
        feedbackTextD.text = "";
        feedbackTextE.text = "";

        EnableButtons();

        // Mostrar la pregunta
        questionTextPanelA.text = currentQuestion.questionText;
        questionTextPanelB.text = currentQuestion.questionText;
        questionTextPanelC.text = currentQuestion.questionText;
        questionTextPanelD.text = currentQuestion.questionText;
        questionTextPanelE.text = currentQuestion.questionText;

        // Mostrar las opciones de respuesta
        feedbackTextA.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[0];
        feedbackTextB.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[1];
        feedbackTextC.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[2];
        feedbackTextD.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[3];
        feedbackTextE.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[4];

        timer = 5.0f; // Reiniciar el cronómetro
        isTimerRunning = true;
    }

    // Asignar eventos a los botones
    void AssignButtonListeners()
    {
        buttonPanelA.onClick.AddListener(() => CheckAnswer(0, "A"));
        buttonPanelB.onClick.AddListener(() => CheckAnswer(1, "B"));
        buttonPanelC.onClick.AddListener(() => CheckAnswer(2, "C"));
        buttonPanelD.onClick.AddListener(() => CheckAnswer(3, "D"));
        buttonPanelE.onClick.AddListener(() => CheckAnswer(4, "E"));
    }

    // Verificar si la respuesta es correcta
    public void CheckAnswer(int selectedIndex, string player)
    {
        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            DisableButtons();

            feedbackTextA.text = "¡Respuesta Correcta!";
            feedbackTextB.text = "¡Respuesta Correcta!";
            feedbackTextC.text = "¡Respuesta Correcta!";
            feedbackTextD.text = "¡Respuesta Correcta!";
            feedbackTextE.text = "¡Respuesta Correcta!";

            isTimerRunning = false; // Detener el cronómetro
            int points = timer > 0 ? 5 : 3; // 5 puntos si respondió a tiempo, 3 si no
            teamScore += points; // Sumar puntos al equipo

            teamScoreA.text = "Team Score: " + teamScore;
            teamScoreB.text = "Team Score: " + teamScore;
            teamScoreC.text = "Team Score: " + teamScore;
            teamScoreD.text = "Team Score: " + teamScore;
            teamScoreE.text = "Team Score: " + teamScore;

            if (timer > 0) // Puntos adicionales para el jugador
            {
                if (player == "A") playerScoreA += 2;
                if (player == "B") playerScoreB += 2;
                if (player == "C") playerScoreC += 2;
                if (player == "D") playerScoreD += 2;
                if (player == "E") playerScoreE += 2;

                playerTextScoreA.text = "Player Score: " + playerScoreA;
                playerTextScoreB.text = "Player Score: " + playerScoreB;
                playerTextScoreC.text = "Player Score: " + playerScoreC;
                playerTextScoreD.text = "Player Score: " + playerScoreD;
                playerTextScoreE.text = "Player Score: " + playerScoreE;
            }

            currentQuestionIndex++;
            Invoke("LoadQuestion", 1.5f);
        }
        else
        {
            if (player == "A") feedbackTextA.text = "Respuesta Incorrecta. Intenta de nuevo.";
            if (player == "B") feedbackTextB.text = "Respuesta Incorrecta. Intenta de nuevo.";
            if (player == "C") feedbackTextC.text = "Respuesta Incorrecta. Intenta de nuevo.";
            if (player == "D") feedbackTextD.text = "Respuesta Incorrecta. Intenta de nuevo.";
            if (player == "E") feedbackTextE.text = "Respuesta Incorrecta. Intenta de nuevo.";
        }
    }

    // Actualizar el cronómetro
    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false; // Detener el cronómetro
            }

            // Mostrar tiempo en los paneles
            timerTextA.text = "Tiempo: " + Mathf.Ceil(timer);
            timerTextB.text = "Tiempo: " + Mathf.Ceil(timer);
            timerTextC.text = "Tiempo: " + Mathf.Ceil(timer);
            timerTextD.text = "Tiempo: " + Mathf.Ceil(timer);
            timerTextE.text = "Tiempo: " + Mathf.Ceil(timer);
        }
    }

    void DisableButtons()
    {
        buttonPanelA.interactable = false;
        buttonPanelB.interactable = false;
        buttonPanelC.interactable = false;
        buttonPanelD.interactable = false;
        buttonPanelE.interactable = false;
    }

    void EnableButtons()
    {
        buttonPanelA.interactable = true;
        buttonPanelB.interactable = true;
        buttonPanelC.interactable = true;
        buttonPanelD.interactable = true;
        buttonPanelE.interactable = true;
    }

}





