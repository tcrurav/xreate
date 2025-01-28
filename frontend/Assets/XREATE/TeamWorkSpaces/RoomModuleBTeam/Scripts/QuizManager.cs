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
        public string questionText;            // Texto de la pregunta
        public List<string> answers;           // Opciones de respuestas
        public int correctAnswerIndex;         // Índice de la respuesta correcta
    }

    public List<GameObject> welcomePanels;  // Lista de paneles de bienvenida
    public List<Button> startButtons;       // Lista de botones de inicio

    public List<GameObject> quizPanels;     // Lista de paneles de quiz
    public List<TMP_Text> questionTexts;    // Lista de textos de preguntas
    public List<TMP_Text> feedbackTexts;    // Lista de textos de retroalimentación
    public List<TMP_Text> timerTexts;       // Lista de textos de cronómetros
    public List<TMP_Text> playerScoreTexts; // Lista de textos de puntajes por jugador
    public List<TMP_Text> teamScoreTexts;   // Lista de textos de puntajes del equipo

    public List<Button> answerButtons;     // Lista de botones de respuestas

    public Sprite defaultSprite; // Sprite por defecto

    public Sprite correctSprite; // Sprite para respuesta correcta
    public Sprite incorrectSprite; // Sprite para respuesta incorrecta
    public AudioClip correctSound; // Sonido para respuesta correcta
    public AudioClip incorrectSound; // Sonido para respuesta incorrecta
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos

    private List<Question> questions = new List<Question>();         // Lista de preguntas
    private List<Question> selectedQuestions = new List<Question>(); // Lista de preguntas seleccionadas aleatoriamente
    private Question currentQuestion;                                // Pregunta actual
    private int currentQuestionIndex = 0;

    private int playersReady = 0;       // Contador de jugadores listos
    private int totalPlayers = 5;       // Número total de jugadores (configurable)

    public TMP_Text feedbackTextA;       // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextB;       // Texto de retroalimentación: Correcto/Incorrecto
    public TMP_Text feedbackTextC;       // Texto de retroalimentación: Correcto/Incorrecto

    private List<Question> questions = new List<Question>(); // Lista de preguntas
    private Question currentQuestion;   // Pregunta actual
    private int currentQuestionIndex = 0;

    void Start()
    {
        // Inicio mi componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Inicializar puntajes para todos los jugadores
        for (int i = 0; i < totalPlayers; i++)
        {
            playerScores.Add(0);
        }

        // Ocultar todos los paneles de quiz y bienvenida al inicio
        foreach (var panel in quizPanels)
        {
            panel.SetActive(false);
        }

        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        // Configurar el número de jugadores
        if (totalPlayers < 2)
        {
            Debug.LogError("El número de jugadores debe ser al menos 2.");
            return;
        }

        // Mostrar los paneles y asignar eventos a los botones de inicio
        for (int i = 0; i < totalPlayers; i++)
        {
            welcomePanels[i].SetActive(true);
            startButtons[i].onClick.AddListener(OnPlayerReady);
        }
    }

    // Método llamado cuando un jugador presiona el botón start
    void OnPlayerReady()
    {
        playersReady++;
        if (playersReady == totalPlayers)
        {
            Invoke("StartQuiz", 1.5f);
        }
    }

    // Inicia el juego de preguntas y respuestas
    void StartQuiz()
    {
        // Ocultar los paneles de bienvenida y mostrar los paneles del quiz
        foreach (var panel in welcomePanels)
        {
            panel.SetActive(false);
        }

        for (int i = 0; i < totalPlayers; i++)
        {
            quizPanels[i].SetActive(true);
        }

        InitializeQuestions();
        LoadQuestion();
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

        foreach (var button in answerButtons)
        {
            button.image.sprite = defaultSprite; // Establecer mi sprite por defecto.
        }

        currentQuestion = selectedQuestions[currentQuestionIndex];

        foreach (var feedback in feedbackTexts)
        {
            feedback.text = ""; // Limpiar mensajes de retroalimentación
        }

        EnableButtons();

        // Mostrar la pregunta
        foreach (var text in questionTexts)
        {
            text.text = currentQuestion.questionText;
        }

        // Mezclar las respuestas y asignarlas a los paneles
        List<int> numbers = new List<int> { 0, 1, 2, 3, 4 };
        System.Random random = new System.Random(); // Generador de números aleatorios

        // Remover el índice de la respuesta correcta
        numbers.Remove(currentQuestion.correctAnswerIndex);

        // Crear la lista de respuestas mezcladas
        List<int> answers = new List<int>();

        // Mezclar los índices de respuestas incorrectas
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // Agregar respuestas incorrectas
        for (int i = 0; i < totalPlayers - 1; i++)
        {
            answers.Add(numbers[i]);
        }

        // Mostrar la pregunta y respuestas
        questionTextPanelA.text = currentQuestion.questionText;
        questionTextPanelB.text = currentQuestion.questionText;
        questionTextPanelC.text = currentQuestion.questionText;

        // Mezclar las respuestas finales
        answers = answers.OrderBy(x => random.Next()).ToList();

        // Asignar las respuestas a los paneles de texto
        for (int i = 0; i < totalPlayers; i++)
        {
            feedbackTexts[i].text = currentQuestion.answers[answers[i]];
        }

        timer = 5.0f; // Reiniciar el cronómetro
        isTimerRunning = true;

        // Limpiar y agregar listeners a los botones
        for (int i = 0; i < totalPlayers; i++)
        {
            int index = i;
            // Limpiar cualquier listener anterior
            answerButtons[i].onClick.RemoveAllListeners();
            // Agregar el listener actual
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index, answers[index]));
        }

    }

    // Verificar si la respuesta es correcta
    public void CheckAnswer(int selectedIndex, int selectedAnswerIndex)
    {

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            DisableButtons();

            answerButtons[selectedIndex].image.sprite = correctSprite; // Cambiar a verde
            audioSource.PlayOneShot(correctSound); // Reproducir sonido correcto
            answerButtons[selectedIndex].interactable = false;

            foreach (var feedback in feedbackTexts)
            {
                feedback.text = "¡Respuesta Correcta!";
            }

            isTimerRunning = false; // Detener el cronómetro
            int points = timer > 0 ? 5 : 3; // 5 puntos si respondió a tiempo, 3 si no
            teamScore += points; // Sumar puntos al equipo

            foreach (var text in teamScoreTexts)
            {
                text.text = "Team Score: " + teamScore;
            }

            if (timer > 0) // Puntos adicionales para el jugador
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
            answerButtons[selectedIndex].image.sprite = incorrectSprite; // Cambiar a rojo
            audioSource.PlayOneShot(incorrectSound); // Reproducir sonido incorrecto
            feedbackTexts[selectedIndex].text = "Respuesta Incorrecta. Intenta de nuevo.";
            answerButtons[selectedIndex].interactable = false;
        }
    }

}
