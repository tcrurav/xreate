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
        public int correctAnswerIndex;  // �ndice de la respuesta correcta
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

    public Button startButtonA;          // Bot�n de inicio
    public Button startButtonB;          // Bot�n de inicio
    public Button startButtonC;          // Bot�n de inicio
    public Button startButtonD;          // Bot�n de inicio
    public Button startButtonE;          // Bot�n de inicio

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

    public Button buttonPanelA;         // Bot�n del panel A
    public Button buttonPanelB;         // Bot�n del panel B
    public Button buttonPanelC;         // Bot�n del panel C
    public Button buttonPanelD;         // Bot�n del panel D
    public Button buttonPanelE;         // Bot�n del panel E

    public TMP_Text feedbackTextA;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextB;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextC;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextD;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextE;      // Texto de retroalimentaci�n: Correcto/Incorrecto

    public TMP_Text timerTextA;          // Texto del cron�metro para el jugador A
    public TMP_Text timerTextB;          // Texto del cron�metro para el jugador B
    public TMP_Text timerTextC;          // Texto del cron�metro para el jugador C
    public TMP_Text timerTextD;          // Texto del cron�metro para el jugador D
    public TMP_Text timerTextE;          // Texto del cron�metro para el jugador E

    private List<Question> questions = new List<Question>();         // Lista de preguntas
    private List<Question> selectedQuestions = new List<Question>(); // Lista de preguntas seleccionadas aleatoriamente
    private Question currentQuestion;                                // Pregunta actual
    private int currentQuestionIndex = 0;

    private int playersReady = 0;       // Contador de jugadores listos
    public int totalPlayers = 5;        // N�mero total de jugadores

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
    private bool isTimerRunning = false; // Indicador de si el cron�metro est� activo

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

    // M�todo llamado cuando un jugador presiona el bot�n start
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
            questionText = "�Qu� es Phishing?",
            answers = new List<string>
        {
            "A: Un m�todo de ataque que busca obtener informaci�n personal mediante enga�os.",
            "B: Un tipo de malware que infecta sistemas operativos.",
            "C: Un ataque para bloquear el acceso a una red.",
            "D: Un m�todo para interceptar comunicaciones cifradas.",
            "E: Un sistema para mejorar la seguridad de contrase�as."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� hace un Ransomware?",
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
            questionText = "�Qu� es un Troyano?",
            answers = new List<string>
        {
            "A: Un tipo de virus dise�ado para replicarse.",
            "B: Una herramienta de protecci�n contra malware.",
            "C: Un programa malicioso disfrazado de software leg�timo.",
            "D: Un ataque dise�ado para saturar una red.",
            "E: Un sistema para detectar actividad sospechosa."
        },
            correctAnswerIndex = 2
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque de fuerza bruta?",
            answers = new List<string>
        {
            "A: Un ataque que prueba todas las combinaciones posibles para descifrar contrase�as.",
            "B: Una estrategia para enga�ar a un usuario mediante ingenier�a social.",
            "C: Un virus que borra datos autom�ticamente.",
            "D: Un m�todo para obtener acceso f�sico a servidores.",
            "E: Una t�cnica para manipular bases de datos."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un Botnet?",
            answers = new List<string>
        {
            "A: Un programa antivirus avanzado.",
            "B: Una red de dispositivos infectados controlados de forma remota.",
            "C: Una t�cnica para realizar phishing.",
            "D: Un m�todo para proteger la privacidad en l�nea.",
            "E: Un sistema para evitar ataques DoS."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque DoS (Denial of Service)?",
            answers = new List<string>
        {
            "A: Un intento de explotar vulnerabilidades de hardware.",
            "B: Un ataque que roba credenciales de usuarios.",
            "C: Un malware dise�ado para cifrar datos personales.",
            "D: Un ataque que bloquea un servicio al saturarlo con solicitudes.",
            "E: Un m�todo de redirecci�n maliciosa en navegadores."
        },
            correctAnswerIndex = 3
        }); 

        questions.Add(new Question
        {
            questionText = "�Qu� es un Rootkit?",
            answers = new List<string>
        {
            "A: Un software que permite acceso oculto a un sistema y evita su detecci�n.",
            "B: Una herramienta de monitoreo de red.",
            "C: Un ataque DoS avanzado.",
            "D: Un programa para cifrar datos personales.",
            "E: Un sistema de control remoto para servidores."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un certificado SSL?",
            answers = new List<string>
        {
            "A: Un ataque para descifrar datos cifrados.",
            "B: Un sistema de autenticaci�n para comunicaciones cifradas.",
            "C: Un sistema para realizar ingenier�a social avanzada.",
            "D: Un est�ndar de seguridad para redes locales.",
            "E: Un tipo de malware dise�ado para servidores."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque SQL Injection?",
            answers = new List<string>
        {
            "A: Un m�todo para prevenir el uso de contrase�as d�biles.",
            "B: Una t�cnica para realizar ataques de phishing.",
            "C: Un ataque que inyecta c�digo malicioso en bases de datos a trav�s de consultas.",
            "D: Una vulnerabilidad que permite acceso remoto no autorizado.",
            "E: Una t�cnica para deshabilitar configuraciones de seguridad."
        },
            correctAnswerIndex = 2
        }); 

        questions.Add(new Question
        {
            questionText = "�Qu� significa el t�rmino Spoofing?",
            answers = new List<string>
        {
            "A: Un ataque dise�ado para robar datos personales mediante redes sociales.",
            "B: Un tipo de malware que infecta dispositivos m�viles.",
            "C: Un m�todo para cifrar datos sensibles.",
            "D: Un software para controlar dispositivos remotamente.",
            "E: Una t�cnica para falsificar la identidad o direcci�n de origen."
        },
            correctAnswerIndex = 4
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque de Ingenier�a Social?",
            answers = new List<string>
        {
            "A: Un ataque t�cnico dirigido a sistemas operativos.",
            "B: Una estrategia para manipular a las personas y obtener informaci�n confidencial.",
            "C: Una t�cnica para proteger informaci�n personal.",
            "D: Un tipo de malware dise�ado para redes sociales.",
            "E: Un sistema de detecci�n de intrusos."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque Man-in-the-Middle (MITM)?",
            answers = new List<string>
        {
            "A: Un ataque donde un tercero intercepta y manipula la comunicaci�n entre dos partes.",
            "B: Un malware que se propaga autom�ticamente en una red.",
            "C: Un software que permite descifrar contrase�as.",
            "D: Un m�todo para evitar conexiones seguras.",
            "E: Un sistema para enga�ar a usuarios mediante ingenier�a social."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es el Criptojacking?",
            answers = new List<string>
        {
            "A: Un ataque que roba criptomonedas directamente de carteras digitales.",
            "B: Un ataque que utiliza recursos de dispositivos infectados para minar criptomonedas.",
            "C: Una t�cnica de protecci�n contra ransomware.",
            "D: Un software para realizar pagos seguros.",
            "E: Un ataque para deshabilitar la red de miner�a."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un Zero-Day?",
            answers = new List<string>
        {
            "A: Una vulnerabilidad desconocida explotada antes de que el desarrollador lance un parche.",
            "B: Un malware dise�ado para desactivar software antivirus.",
            "C: Un sistema de monitoreo de tr�fico de red.",
            "D: Un m�todo para proteger contrase�as d�biles.",
            "E: Un ataque dirigido a hardware obsoleto."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un Keylogger?",
            answers = new List<string>
        {
            "A: Un malware que registra las pulsaciones del teclado.",
            "B: Un ataque para bloquear redes sociales.",
            "C: Una t�cnica de recuperaci�n de datos.",
            "D: Un sistema para encriptar contrase�as autom�ticamente.",
            "E: Un ataque dise�ado para desactivar firewalls."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque de Spear Phishing?",
            answers = new List<string>
        {
            "A: Una versi�n m�s espec�fica y dirigida de un ataque de phishing.",
            "B: Un ataque para deshabilitar contrase�as cifradas.",
            "C: Un software de monitoreo avanzado.",
            "D: Una t�cnica para infectar bases de datos.",
            "E: Un sistema para rastrear correos electr�nicos falsificados."
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
            questionTextPanelA.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore;
            questionTextPanelB.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore;
            questionTextPanelC.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore;
            questionTextPanelD.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore;
            questionTextPanelE.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore;

            feedbackTextA.text = ""; // Limpio el TMP Correspondiente a las preguntas
            feedbackTextB.text = "";
            feedbackTextC.text = "";
            feedbackTextD.text = "";
            feedbackTextE.text = "";

            DisableButtons();
            return;
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];
        feedbackTextA.text = ""; // Limpiar mensajes de retroalimentaci�n
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

        timer = 5.0f; // Reiniciar el cron�metro
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

            feedbackTextA.text = "�Respuesta Correcta!";
            feedbackTextB.text = "�Respuesta Correcta!";
            feedbackTextC.text = "�Respuesta Correcta!";
            feedbackTextD.text = "�Respuesta Correcta!";
            feedbackTextE.text = "�Respuesta Correcta!";

            isTimerRunning = false; // Detener el cron�metro
            int points = timer > 0 ? 5 : 3; // 5 puntos si respondi� a tiempo, 3 si no
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

    // Actualizar el cron�metro
    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false; // Detener el cron�metro
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





