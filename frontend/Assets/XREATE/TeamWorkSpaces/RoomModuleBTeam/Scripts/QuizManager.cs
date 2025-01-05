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

    public TMP_Text welcomeTextA;        // Texto de bienvenida
    public TMP_Text welcomeTextB;        // Texto de bienvenida
    public TMP_Text welcomeTextC;        // Texto de bienvenida

    public Button startButtonA;          // Bot�n de inicio
    public Button startButtonB;          // Bot�n de inicio
    public Button startButtonC;          // Bot�n de inicio

    public GameObject quizPanelA;        // Panel del quiz
    public GameObject quizPanelB;        // Panel del quiz
    public GameObject quizPanelC;        // Panel del quiz

    public TMP_Text questionTextPanelA; // Pregunta en el panel A
    public TMP_Text questionTextPanelB; // Pregunta en el panel B
    public TMP_Text questionTextPanelC; // Pregunta en el panel C

    public Button buttonPanelA;         // Bot�n del panel A
    public Button buttonPanelB;         // Bot�n del panel B
    public Button buttonPanelC;         // Bot�n del panel C

    public TMP_Text feedbackTextA;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextB;      // Texto de retroalimentaci�n: Correcto/Incorrecto
    public TMP_Text feedbackTextC;      // Texto de retroalimentaci�n: Correcto/Incorrecto

    public TMP_Text timerTextA;          // Texto del cron�metro para el jugador A
    public TMP_Text timerTextB;          // Texto del cron�metro para el jugador B
    public TMP_Text timerTextC;          // Texto del cron�metro para el jugador C

    private List<Question> questions = new List<Question>();         // Lista de preguntas
    private List<Question> selectedQuestions = new List<Question>(); // Lista de preguntas seleccionadas aleatoriamente
    private Question currentQuestion;                                // Pregunta actual
    private int currentQuestionIndex = 0;

    private int playersReady = 0;       // Contador de jugadores listos
    public int totalPlayers = 3;        // N�mero total de jugadores

    private int teamScore = 0;          // Puntaje del equipo
    private int playerScoreA = 0;       // Puntaje del jugador A
    private int playerScoreB = 0;       // Puntaje del jugador B
    private int playerScoreC = 0;       // Puntaje del jugador C

    public TMP_Text teamScoreA;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreB;         // Puntaje del equipo para mostrar por pantalla
    public TMP_Text teamScoreC;         // Puntaje del equipo para mostrar por pantalla

    public TMP_Text playerTextScoreA;   // Puntaje del jugadoer A para mostrar por pantalla
    public TMP_Text playerTextScoreB;   // Puntaje del jugadoer B para mostrar por pantalla
    public TMP_Text playerTextScoreC;   // Puntaje del jugadoer C para mostrar por pantalla

    private float timer = 5.0f;         // Tiempo para responder cada pregunta
    private bool isTimerRunning = false; // Indicador de si el cron�metro est� activo

    void Start()
    {
        // Inicialmente se muestra la pantalla de bienvenida
        quizPanelA.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelB.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelC.SetActive(false); // Ocultar el panel de preguntas inicialmente

        welcomePanelA.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelB.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelC.SetActive(true); // Mostrar la pantalla de bienvenida

        startButtonA.onClick.AddListener(OnPlayerReady);
        startButtonB.onClick.AddListener(OnPlayerReady);
        startButtonC.onClick.AddListener(OnPlayerReady);
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

        quizPanelA.SetActive(true);    // Mostrar el panel del quiz
        quizPanelB.SetActive(true);    // Mostrar el panel del quiz
        quizPanelC.SetActive(true);    // Mostrar el panel del quiz

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
            "C: Un ataque para bloquear el acceso a una red."
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
            "C: Infecta el hardware del equipo."
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
            "C: Un programa malicioso disfrazado de software leg�timo."
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
            "C: Un virus que borra datos autom�ticamente."
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
            "C: Una t�cnica para realizar phishing."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque DoS (Denial of Service)?",
            answers = new List<string>
        {
            "A: Un ataque que bloquea un servicio al saturarlo con solicitudes.",
            "B: Un ataque que roba credenciales de usuarios.",
            "C: Un malware dise�ado para cifrar datos personales."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un Rootkit?",
            answers = new List<string>
        {
            "A: Un software que permite acceso oculto a un sistema y evita su detecci�n.",
            "B: Una herramienta de monitoreo de red.",
            "C: Un ataque DoS avanzado."
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
            "C: Un sistema para realizar ingenier�a social avanzada."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque SQL Injection?",
            answers = new List<string>
        {
            "A: Un ataque que inyecta c�digo malicioso en bases de datos a trav�s de consultas.",
            "B: Una t�cnica para realizar ataques de phishing.",
            "C: Un m�todo para prevenir el uso de contrase�as d�biles."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� significa el t�rmino Spoofing?",
            answers = new List<string>
        {
            "A: Una t�cnica para falsificar la identidad o direcci�n de origen.",
            "B: Un ataque dise�ado para robar datos personales mediante redes sociales.",
            "C: Un tipo de malware que infecta dispositivos m�viles."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "�Qu� es un ataque de Ingenier�a Social?",
            answers = new List<string>
        {
            "A: Un ataque t�cnico dirigido a sistemas operativos.",
            "B: Una estrategia para manipular a las personas y obtener informaci�n confidencial.",
            "C: Una t�cnica para proteger informaci�n personal."
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
            "C: Un software que permite descifrar contrase�as."
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
            "C: Una t�cnica de protecci�n contra ransomware."
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
            "C: Un sistema de monitoreo de tr�fico de red."
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
            "C: Una t�cnica de recuperaci�n de datos."
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
            "C: Un software de monitoreo avanzado."
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
            questionTextPanelA.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore.ToString();
            questionTextPanelB.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore.ToString();
            questionTextPanelC.text = "Juego terminado. Puntuaci�n del equipo: " + teamScore.ToString();

            feedbackTextA.text = ""; // Limpio el TMP Correspondiente a las preguntas
            feedbackTextB.text = ""; // Limpio el TMP Correspondiente a las preguntas
            feedbackTextC.text = ""; // Limpio el TMP Correspondiente a las preguntas

            DisableButtons(); // Deshabilito los botones
            return;
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];
        feedbackTextA.text = ""; // Limpiar mensajes de retroalimentaci�n
        feedbackTextB.text = ""; // Limpiar mensajes de retroalimentaci�n
        feedbackTextC.text = ""; // Limpiar mensajes de retroalimentaci�n

        // Mostrar la pregunta y respuestas
        questionTextPanelA.text = currentQuestion.questionText;
        questionTextPanelB.text = currentQuestion.questionText;
        questionTextPanelC.text = currentQuestion.questionText;

        feedbackTextA.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[0];
        feedbackTextB.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[1];
        feedbackTextC.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[2];

        timer = 5.0f; // Reiniciar el cron�metro
        isTimerRunning = true;
    }

    // Asignar eventos a los botones
    void AssignButtonListeners()
    {
        buttonPanelA.onClick.AddListener(() => CheckAnswer(0, "A"));
        buttonPanelB.onClick.AddListener(() => CheckAnswer(1, "B"));
        buttonPanelC.onClick.AddListener(() => CheckAnswer(2, "C"));
    }

    // Verificar si la respuesta es correcta
    public void CheckAnswer(int selectedIndex, string player)
    {

        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            feedbackTextA.text = "�Respuesta Correcta!";
            feedbackTextB.text = "�Respuesta Correcta!";
            feedbackTextC.text = "�Respuesta Correcta!";

            isTimerRunning = false; // Detener el cron�metro
            int points = timer > 0 ? 5 : 3; // 5 puntos si respondi� a tiempo, 3 si no
            teamScore += points; // Sumar puntos al equipo

            teamScoreA.text = "Team Score: " + teamScore.ToString();
            teamScoreB.text = "Team Score: " + teamScore.ToString();
            teamScoreC.text = "Team Score: " + teamScore.ToString();

            if (timer > 0) // Puntos adicionales para el jugador
            {
                if (player == "A")
                {
                    playerScoreA += 2;
                    playerTextScoreA.text = "Player Score: " + playerScoreA.ToString();
                } 
                else if (player == "B") 
                {
                    playerScoreB += 2;
                    playerTextScoreB.text = "Player Score: " + playerScoreB.ToString();
                }
                else if (player == "C") 
                {
                    playerScoreC += 2;
                    playerTextScoreC.text = "Player Score: " + playerScoreC.ToString();
                }
            }

            currentQuestionIndex++;
            Invoke("LoadQuestion", 1.5f);
        }
        else
        {
            if (player == "A")
            {
                feedbackTextA.text = "Respuesta Incorrecta. Intenta de nuevo.";
            }
            else if (player == "B")
            {
                feedbackTextB.text = "Respuesta Incorrecta. Intenta de nuevo.";
            }
            else if (player == "C")
            {
                feedbackTextC.text = "Respuesta Incorrecta. Intenta de nuevo.";
            }
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
            timerTextA.text = "Tiempo: " + Mathf.Ceil(timer).ToString();
            timerTextB.text = "Tiempo: " + Mathf.Ceil(timer).ToString();
            timerTextC.text = "Tiempo: " + Mathf.Ceil(timer).ToString();
        }
    }

    void DisableButtons()
    {
        buttonPanelA.interactable = false;
        buttonPanelB.interactable = false;
        buttonPanelC.interactable = false;
    }

}
