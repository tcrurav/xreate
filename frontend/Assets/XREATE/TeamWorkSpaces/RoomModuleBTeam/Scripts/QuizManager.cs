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

    public TMP_Text welcomeTextA;        // Texto de bienvenida
    public TMP_Text welcomeTextB;        // Texto de bienvenida
    public TMP_Text welcomeTextC;        // Texto de bienvenida

    public Button startButtonA;          // Botón de inicio
    public Button startButtonB;          // Botón de inicio
    public Button startButtonC;          // Botón de inicio

    public GameObject quizPanelA;        // Panel del quiz
    public GameObject quizPanelB;        // Panel del quiz
    public GameObject quizPanelC;        // Panel del quiz

    public TMP_Text questionTextPanelA; // Pregunta en el panel A
    public TMP_Text questionTextPanelB; // Pregunta en el panel B
    public TMP_Text questionTextPanelC; // Pregunta en el panel C

    public Button buttonPanelA;         // Botón del panel A
    public Button buttonPanelB;         // Botón del panel B
    public Button buttonPanelC;         // Botón del panel C

    public TMP_Text feedbackTextA;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextB;      // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextC;      // Texto de retroalimentación: Correcto/Incorrecto

    private List<Question> questions = new List<Question>(); // Lista de preguntas
    private Question currentQuestion;   // Pregunta actual
    private int currentQuestionIndex = 0;

    private int playersReady = 0;       // Contador de jugadores listos
    public int totalPlayers = 3;        // Número total de jugadores


    void Start()
    {
        // Inicialmente se muestra la pantalla de bienvenida
        quizPanelA.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelB.SetActive(false); // Ocultar el panel de preguntas inicialmente
        quizPanelC.SetActive(false); // Ocultar el panel de preguntas inicialmente

        welcomePanelA.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelB.SetActive(true); // Mostrar la pantalla de bienvenida
        welcomePanelC.SetActive(true); // Mostrar la pantalla de bienvenida

        //welcomeTextA.text = "¡Bienvenidos al desafío de preguntas y respuestas! Prepárense para trabajar en equipo, ser rápidos y demostrar sus conocimientos.\n\nPresiona Start cuando estés listo!";
        //welcomeTextB.text = "¡Bienvenidos al desafío de preguntas y respuestas! Prepárense para trabajar en equipo, ser rápidos y demostrar sus conocimientos.\n\nPresiona Start cuando estés listo!";
        //welcomeTextC.text = "¡Bienvenidos al desafío de preguntas y respuestas! Prepárense para trabajar en equipo, ser rápidos y demostrar sus conocimientos.\n\nPresiona Start cuando estés listo!";
        
        startButtonA.onClick.AddListener(OnPlayerReady);
        startButtonB.onClick.AddListener(OnPlayerReady);
        startButtonC.onClick.AddListener(OnPlayerReady);
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

        quizPanelA.SetActive(true);    // Mostrar el panel del quiz
        quizPanelB.SetActive(true);    // Mostrar el panel del quiz
        quizPanelC.SetActive(true);    // Mostrar el panel del quiz

        InitializeQuestions();
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
            "C: Un ataque para bloquear el acceso a una red."
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
            "C: Infecta el hardware del equipo."
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
            "C: Un programa malicioso disfrazado de software legítimo."
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
            "C: Un virus que borra datos automáticamente."
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
            "C: Una técnica para realizar phishing."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque DoS (Denial of Service)?",
            answers = new List<string>
        {
            "A: Un ataque que bloquea un servicio al saturarlo con solicitudes.",
            "B: Un ataque que roba credenciales de usuarios.",
            "C: Un malware diseñado para cifrar datos personales."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un Rootkit?",
            answers = new List<string>
        {
            "A: Un software que permite acceso oculto a un sistema y evita su detección.",
            "B: Una herramienta de monitoreo de red.",
            "C: Un ataque DoS avanzado."
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
            "C: Un sistema para realizar ingeniería social avanzada."
        },
            correctAnswerIndex = 1
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque SQL Injection?",
            answers = new List<string>
        {
            "A: Un ataque que inyecta código malicioso en bases de datos a través de consultas.",
            "B: Una técnica para realizar ataques de phishing.",
            "C: Un método para prevenir el uso de contraseñas débiles."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué significa el término Spoofing?",
            answers = new List<string>
        {
            "A: Una técnica para falsificar la identidad o dirección de origen.",
            "B: Un ataque diseñado para robar datos personales mediante redes sociales.",
            "C: Un tipo de malware que infecta dispositivos móviles."
        },
            correctAnswerIndex = 0
        });

        questions.Add(new Question
        {
            questionText = "¿Qué es un ataque de Ingeniería Social?",
            answers = new List<string>
        {
            "A: Un ataque técnico dirigido a sistemas operativos.",
            "B: Una estrategia para manipular a las personas y obtener información confidencial.",
            "C: Una técnica para proteger información personal."
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
            "C: Un software que permite descifrar contraseñas."
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
            "C: Una técnica de protección contra ransomware."
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
            "C: Un sistema de monitoreo de tráfico de red."
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
            "C: Una técnica de recuperación de datos."
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
            "C: Un software de monitoreo avanzado."
        },
            correctAnswerIndex = 0
        });
    }

    // Cargar una pregunta y respuestas en los paneles
    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            feedbackTextA.text = "¡Has completado todas las preguntas!";
            feedbackTextB.text = "¡Has completado todas las preguntas!";
            feedbackTextC.text = "¡Has completado todas las preguntas!";
            return;
        }

        currentQuestion = questions[currentQuestionIndex];
        feedbackTextA.text = ""; // Limpiar mensajes de retroalimentación
        feedbackTextB.text = ""; // Limpiar mensajes de retroalimentación
        feedbackTextC.text = ""; // Limpiar mensajes de retroalimentación

        // Mostrar la pregunta y respuestas
        questionTextPanelA.text = currentQuestion.questionText;
        questionTextPanelB.text = currentQuestion.questionText;
        questionTextPanelC.text = currentQuestion.questionText;

        feedbackTextA.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[0];
        feedbackTextB.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[1];
        feedbackTextC.GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[2];
    }

    // Asignar eventos a los botones
    void AssignButtonListeners()
    {
        buttonPanelA.onClick.AddListener(() => CheckAnswer(0));
        buttonPanelB.onClick.AddListener(() => CheckAnswer(1));
        buttonPanelC.onClick.AddListener(() => CheckAnswer(2));
    }

    // Verificar si la respuesta es correcta
    public void CheckAnswer(int selectedIndex)
    {
        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            feedbackTextA.text = "¡Respuesta Correcta!";
            feedbackTextB.text = "¡Respuesta Correcta!";
            feedbackTextC.text = "¡Respuesta Correcta!";
            currentQuestionIndex++;
            Invoke("LoadQuestion", 1.5f); // Cargar la siguiente pregunta después de 1.5 segundos
        }
        else
        {
            feedbackTextA.text = "Respuesta Incorrecta. Intenta de nuevo.";
            feedbackTextB.text = "Respuesta Incorrecta. Intenta de nuevo.";
            feedbackTextC.text = "Respuesta Incorrecta. Intenta de nuevo.";
            Invoke("LoadQuestion", 1.5f); // Cargar la misma pregunta después de 1.5 segundos
        }
    }

}
