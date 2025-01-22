using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TeamWorkSpaces.LeisureModule;

namespace TeamWorkSpaces.LeisureModule
{
    public class QuestionManager : MonoBehaviour
    {
        public Question CurrentQuestion { get; private set; } // La pregunta actual
        public Transform[] focusPositions; // Posiciones para las palabras
        public GameObject wordPrefab; // Prefab para las palabras

        public string questionsFileName = "Questions.json"; // Archivo JSON de preguntas
        public string dictionaryFileName = "Dictionary.json"; // Archivo JSON del diccionario

        private List<Question> questions = new List<Question>(); // Lista de preguntas
        private List<string> dictionary = new List<string>(); // Lista de palabras del diccionario
        private List<GameObject> instantiatedWords = new List<GameObject>();

        private int currentQuestionIndex = 0; // Índice de la pregunta actual

        public QuestionTextUpdater questionTextUpdater; // Referencia al QuestionTextUpdater

        void Start()
        {
            Debug.Log("Iniciando QuestionManager...");

            // Cargar preguntas y diccionario al inicio, pero sin cargar preguntas automáticamente
            LoadQuestionsFromJson();
            LoadDictionaryFromJson();

            Debug.Log("Datos cargados. Usa los botones para cargar preguntas y palabras.");
        }

        private void LoadQuestionsFromJson()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, questionsFileName);
            Debug.Log($"Intentando cargar preguntas desde: {filePath}");

            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                Debug.Log($"Contenido del JSON de preguntas: {jsonContent}");

                try
                {
                    QuestionData questionData = JsonUtility.FromJson<QuestionData>(jsonContent);
                    questions = questionData.questions;

                    if (questions != null && questions.Count > 0)
                    {
                        Debug.Log($"Preguntas cargadas: {questions.Count}");
                    }
                    else
                    {
                        Debug.LogError("El archivo de preguntas está vacío o no contiene datos válidos.");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error al parsear el JSON de preguntas: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"Archivo de preguntas no encontrado en: {filePath}");
            }
        }

        private void LoadDictionaryFromJson()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, dictionaryFileName);
            Debug.Log($"Intentando cargar diccionario desde: {filePath}");

            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                Debug.Log($"Contenido del JSON de diccionario: {jsonContent}");

                try
                {
                    DictionaryData dictionaryData = JsonUtility.FromJson<DictionaryData>(jsonContent);
                    dictionary = dictionaryData.cybersecurity_words;

                    if (dictionary != null && dictionary.Count > 0)
                    {
                        Debug.Log($"Palabras del diccionario cargadas: {dictionary.Count}");
                    }
                    else
                    {
                        Debug.LogError("El diccionario está vacío o no contiene palabras válidas.");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error al parsear el JSON del diccionario: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"Archivo de diccionario no encontrado en: {filePath}");
            }
        }

        public void LoadQuestion(int questionIndex)
        {
            if (questions == null || questionIndex < 0 || questionIndex >= questions.Count)
            {
                Debug.LogError("Índice de pregunta fuera de rango.");
                return;
            }

            currentQuestionIndex = questionIndex;
            CurrentQuestion = questions[questionIndex];

            Debug.Log($"Cargando pregunta: {CurrentQuestion.text}");

            // Actualizar el texto en el UI
            if (questionTextUpdater != null)
            {
                questionTextUpdater.UpdateQuestionText();
            }
            else
            {
                Debug.LogWarning("QuestionTextUpdater no está asignado en el QuestionManager.");
            }
        }

        public void LoadRandomQuestion()
        {
            if (questions == null || questions.Count == 0)
            {
                Debug.LogError("No hay preguntas cargadas para seleccionar.");
                return;
            }

            // Seleccionar una pregunta aleatoria
            int randomIndex = Random.Range(0, questions.Count);

            // Cargar la pregunta seleccionada
            LoadQuestion(randomIndex);
        }

        public void LoadWordsForCurrentQuestion()
        {
            if (CurrentQuestion == null)
            {
                Debug.LogError("No hay una pregunta cargada actualmente. Selecciona una pregunta primero.");
                return;
            }

            // Limpiar palabras existentes
            foreach (var word in instantiatedWords)
            {
                Destroy(word);
            }
            instantiatedWords.Clear();

            // Mezclar palabras
            List<string> mixedWords = GetMixedWords(CurrentQuestion);
            Debug.Log($"Palabras mezcladas: {string.Join(", ", mixedWords)}");

            // Distribuir palabras en los focos
            for (int i = 0; i < focusPositions.Length; i++)
            {
                if (i < mixedWords.Count)
                {
                    Debug.Log($"Instanciando palabra '{mixedWords[i]}' en foco {i}");
                    GameObject wordObject = Instantiate(wordPrefab, focusPositions[i].position, Quaternion.identity);
                    Word wordScript = wordObject.GetComponent<Word>();
                    if (wordScript != null)
                    {
                        wordScript.SetWord(mixedWords[i]);
                    }
                    instantiatedWords.Add(wordObject);
                }
            }
        }

        private List<string> GetMixedWords(Question question)
        {
            // Obtener palabras únicas desde las respuestas correctas
            HashSet<string> uniqueWords = new HashSet<string>();
            foreach (var correctAnswer in question.correct_answers)
            {
                uniqueWords.UnionWith(correctAnswer.words);
            }

            // Añadir palabras del diccionario sin duplicar
            List<string> mixedWords = uniqueWords.ToList();
            foreach (var word in dictionary)
            {
                if (mixedWords.Count >= 25) break;
                if (!mixedWords.Contains(word))
                {
                    mixedWords.Add(word);
                }
            }

            // Mezclar las palabras
            System.Random random = new System.Random();
            mixedWords = mixedWords.OrderBy(_ => random.Next()).ToList();

            return mixedWords;
        }
    }
}
