using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Networking;



namespace TeamWorkSpaces.LeisureModule
{
    public class QuestionManager : MonoBehaviour
    {
        private static readonly HashSet<string> stopWords = new HashSet<string>
{
    // 🔹 Articles & Prepositions
    "a", "an", "the", "of", "to", "in", "on", "with", "by", "at", "from", "for", "about",
    "as", "into", "through", "over", "under", "between", "against", "without", "within",
    "onto", "upon", "along", "amid", "among", "beside", "beneath", "beyond", "except",
    
    // 🔹 Pronouns
    "i", "me", "my", "mine", "myself", "we", "us", "our", "ours", "ourselves",
    "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself",
    "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their",
    "theirs", "themselves", "this", "that", "these", "those", "someone", "somebody",
    "something", "anyone", "anybody", "anything", "everyone", "everybody", "everything",
    
    // 🔹 Auxiliary & Modal Verbs
    "is", "am", "are", "was", "were", "be", "been", "being", "do", "does", "did",
    "have", "has", "had", "having", "can", "could", "shall", "should", "will",
    "would", "may", "might", "must", "ought", "need", "dare", "seem", "appear",
    
    // 🔹 Conjunctions & Connectors
    "and", "or", "but", "nor", "so", "yet", "though", "although", "because",
    "since", "unless", "whereas", "while", "however", "therefore", "hence",
    "thus", "nevertheless", "nonetheless", "moreover", "furthermore",
    "otherwise", "whereupon", "whereby", "whenever", "wherever",
    
    // 🔹 Adverbs of Quantity & Frequency
    "very", "too", "quite", "rather", "somewhat", "fairly", "almost", "nearly",
    "barely", "scarcely", "only", "just", "even", "ever", "never", "always",
    "usually", "often", "sometimes", "seldom", "rarely", "again", "once",
    
    // 🔹 Determiners & Quantifiers
    "all", "any", "both", "each", "either", "every", "neither", "some", "several",
    "many", "much", "few", "fewer", "less", "least", "most", "more", "none",
    
    // 🔹 Question Words & Interrogative Particles
    "what", "which", "who", "whom", "whose", "where", "when", "why", "how",
    
    // 🔹 Redundant Filler Words
    "also", "again", "anyway", "certainly", "clearly", "definitely",
    "especially", "exactly", "finally", "fortunately", "hopefully",
    "incidentally", "indeed", "instead", "likely", "meanwhile",
    "namely", "naturally", "necessarily", "nevertheless", "obviously",
    "possibly", "presumably", "probably", "really", "similarly",
    "simply", "specifically", "surely", "thereafter", "thereby",
    "therefore", "unfortunately", "undoubtedly", "unexpectedly",
    "unfortunately", "whereby", "wherein", "wherever", "whenever",
    
    // 🔹 Common Words in Cybersecurity Answers
    "use", "using", "includes", "include", "has", "have", "having", "contains",
    "contain", "avoid", "not", "no", "do", "does", "done", "being", "such", "like",
    "according", "according to", "based on", "related to", "regarding", "concerning",
    
    // 🔹 Additional Stop Words
    "get", "got", "getting", "makes", "made", "make", "takes", "taking", "taken",
    "seems", "seemed", "appears", "appeared", "go", "goes", "went", "gone",
    "said", "says", "saying", "tell", "tells", "told", "come", "comes", "came",
    "put", "puts", "putting", "set", "sets", "setting", "give", "gives", "given"
};


        private static readonly HashSet<string> cybersecurityKeywords = new HashSet<string>
{
    // 🔹 Basic Security Concepts 
    "firewall", "malware", "ransomware", "phishing", "authentication", "encryption",
    "password", "data", "network", "security", "multi-factor", "MFA", "EDR", "virus",
    "hacking", "threat", "breach", "intrusion", "spyware", "scam", "hacker", "attack",
    "compromise", "exploit", "payload", "backdoor", "zero-day", "adware", "worm",
    "cyberattack", "defense", "vulnerability", "penetration", "forensics",
    "spoofing", "exfiltration", "keylogger", "brute-force", "social-engineering",
    "patch", "hashing", "whitelist", "blacklist", "certificate", "sensitive",
    "incident", "log", "access-control", "biometrics", "endpoint", "sandboxing",
    
    // 🔹 Types of Attacks and Threats 
    "social engineering", "brute force", "DDoS", "denial of service", "SQL injection",
    "XSS", "cross-site scripting", "CSRF", "session hijacking", "man-in-the-middle",
    "MITM", "credential stuffing", "password spraying", "spoofing", "clickjacking",
    "malvertising", "rootkit", "trojan", "botnet", "keylogger", "skimming",
    "watering hole attack", "supply chain attack", "smishing", "vishing", "whaling",
    "cryptojacking", "sandbox evasion", "side-channel attack", "DNS poisoning",
    "buffer overflow", "race condition", "logic bomb", "session fixation",
    
    // 🔹 Security Protocols and Standards 
    "TLS", "SSL", "HTTPS", "VPN", "SSH", "IPSec", "OAuth", "SAML", "Kerberos",
    "LDAP", "OpenID", "HSTS", "PFS", "E2EE", "AES", "RSA", "SHA", "MD5",
    "bcrypt", "salting", "hashing", "public key", "private key", "PKI",
    "digital certificate", "digital signature", "TLS handshake",
    
    // 🔹 Network Security  
    "packet sniffing", "IP spoofing", "firewall rule", "intrusion detection",
    "intrusion prevention", "proxy", "DMZ", "port scanning", "honeypot",
    "deep packet inspection", "SIEM", "SOC", "IDS", "IPS", "syslog", "zero trust",
    "NAT", "VLAN", "deep web", "dark web", "tor", "proxy server", "onion routing",
    "traffic analysis", "packet capture", "NetFlow", "BGP hijacking",
    
    // 🔹 Cloud and Endpoint Security 
    "cloud security", "AWS security", "Azure security", "Google Cloud security",
    "IAM", "identity access management", "endpoint security", "zero trust model",
    "container security", "Kubernetes security", "API security", "cloud firewall",
    "cloud encryption", "serverless security", "CASB", "BYOD", "MDR", "XDR",
    "SASE", "ZTNA", "cloud forensics", "cloud access", "hybrid cloud",
    
    // 🔹 Ethical Hacking and Pentesting 
    "ethical hacking", "penetration testing", "pentest", "red teaming",
    "black box testing", "white box testing", "gray box testing", "bug bounty",
    "CVE", "exploit development", "reverse engineering", "fuzzing", "buffer overflow",
    "privilege escalation", "lateral movement", "post-exploitation", "web exploitation",
    "Metasploit", "Burp Suite", "Nmap", "Wireshark", "Kali Linux", "reconnaissance",
    "information gathering", "footprinting", "enumeration", "exfiltration",
    
    // 🔹 Compliance and Regulations 
    "GDPR", "CCPA", "HIPAA", "NIST", "ISO 27001", "PCI-DSS", "FISMA", "SOC 2",
    "FedRAMP", "CMMC", "data protection", "compliance", "cyber law",
    
    // 🔹 Advanced Security Concepts 
    "behavioral analysis", "threat intelligence", "malware analysis",
    "cyber forensics", "incident response", "SIEM", "UEBA", "SOC",
    "threat hunting", "dark web monitoring", "deception technology",
    "identity spoofing", "memory forensics", "cyber kill chain",
    "MITRE ATT&CK", "lockpicking", "air gapped", "quarantine",
    
    // 🔹 Mobile and IoT Security
    "mobile security", "IoT security", "BYOD security", "mobile device management",
    "MDM", "MFA", "biometric authentication", "smart lock", "5G security",
    "bluetooth attack", "NFC attack", "mobile phishing", "SIM swap",
    
    // 🔹 Industrial Security and SCADA  
    "SCADA security", "ICS security", "industrial control systems",
    "OT security", "air gapping", "power grid security", "PLC hacking",
    "critical infrastructure", "IIoT security", "cyber-physical security",
    
    // 🔹 Cyber Intelligence and Counterespionage  
    "threat intelligence", "cyber espionage", "nation-state attack",
    "cyber warfare", "cyber sabotage", "dark web intelligence",
    "counterintelligence", "OSINT", "SIGINT", "HUMINT",
    "black market", "ransom negotiations", "cyber extortion"
};
        private HashSet<string> correctWords = new HashSet<string>(); // Stores correct words from answers
        // 🔹 API Endpoint (Set this in Unity Inspector)
        public string apiEndpoint = "https://your-api-endpoint.com/questions";
        public string questionsFileName = "Questions.json"; // JSON file for questions
        public string dictionaryFileName = "Dictionary.json"; // JSON file for dictionary

        public QuestionTextUpdater questionTextUpdater; // UI updater reference
        public Transform[] focusPositions; // Positions where words will be placed
        public GameObject wordPrefab; // Prefab for the words

        private List<Question> questions = new List<Question>(); // List of questions
        private List<string> dictionary = new List<string>(); // Dictionary words (distractors)
        private List<GameObject> instantiatedWords = new List<GameObject>(); // List of words in scene


        private const int TOTAL_WORD_SLOTS = 25; // Total words 25
        private int currentQuestionIndex = 0; // Current question index
        public Question CurrentQuestion { get; private set; } // Current question


        // SINGLETON

        public static QuestionManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("❌ QuestionManager duplicado detectado, destruyendo instancia extra.");
                Destroy(gameObject);
            }
        }

        void Start()
        {
            StartCoroutine(LoadQuestionsFromAPI(apiEndpoint)); // Primero intentamos cargar preguntas desde la API
            LoadDictionaryFromJson(); // Luego cargamos el diccionario local

        }

        public List<string> GetRandomDistractors(int count)
        {
            System.Random random = new System.Random();
            return dictionary.OrderBy(_ => random.Next()).Take(count).ToList();
        }

        /// <summary>
        /// Loads questions from an external API.
        /// </summary>
        private IEnumerator LoadQuestionsFromAPI(string apiUrl)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = request.downloadHandler.text;
                    try
                    {
                        QuestionData questionData = JsonUtility.FromJson<QuestionData>(jsonResponse);
                        questions = questionData.questions ?? new List<Question>();
                        Debug.Log($"✅ API Loaded {questions.Count} questions.");
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"❌ Error parsing API response: {e.Message}. Falling back to local JSON.");
                        LoadQuestionsFromJson();
                    }
                }
                else
                {
                    Debug.LogError($"❌ API request failed: {request.error}. Falling back to local JSON.");
                    LoadQuestionsFromJson();
                }
            }
        }

        /// <summary>
        /// Loads questions from a JSON file.
        /// </summary>
        private void LoadQuestionsFromJson()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, questionsFileName);
            Debug.Log($"📂 Loading questions from: {filePath}");

            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                try
                {
                    QuestionData questionData = JsonUtility.FromJson<QuestionData>(jsonContent);
                    questions = questionData.questions ?? new List<Question>();
                    Debug.Log($"✅ Loaded {questions.Count} questions from local JSON.");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"❌ Error parsing question JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"❌ Question file not found at: {filePath}");
            }
        }
        /// <summary>
        /// Loads words related to the current question.
        /// </summary>
        public void LoadWordsForCurrentQuestion()
        {
            if (CurrentQuestion == null)
            {
                Debug.LogError("❌ No question is currently loaded.");
                return;
            }

            // 🧹 1️.- Limpiar palabras previas
            foreach (var word in instantiatedWords)
            {
                if (word != null) Destroy(word);
            }
            instantiatedWords.Clear();

            // 🏷️ 2️.- Obtener palabras correctas de la pregunta
            HashSet<string> correctWords = new HashSet<string>();
            foreach (var answer in CurrentQuestion.correct_answers)
            {
                correctWords.UnionWith(answer.words);
            }

            Debug.Log($"✅ Correct words: {string.Join(", ", correctWords)}");

            // 📌 3️.- Asegurar que haya 25 palabras en total
            List<string> mixedWords = new List<string>(correctWords);

            // 🔄 4️.- Añadir palabras del diccionario hasta completar 25
            if (dictionary == null || dictionary.Count == 0)
            {
                Debug.LogError("❌ Dictionary is empty or not loaded.");
                return;
            }

            System.Random random = new System.Random();
            List<string> fillerWords = dictionary
                .Where(word => !correctWords.Contains(word)) // Evitar duplicados
                .OrderBy(_ => random.Next()) // Mezclar
                .Take(25 - mixedWords.Count) // Tomar las necesarias
                .ToList();

            mixedWords.AddRange(fillerWords);

            // 📌 5️.- Verificar que realmente tenemos 25 palabras
            if (mixedWords.Count < 25)
            {
                Debug.LogError($"❌ STILL insufficient words! Only {mixedWords.Count} generated.");
                return;
            }

            // 🔀 6️.- Mezclar palabras antes de asignarlas a los focos
            mixedWords = mixedWords.OrderBy(_ => random.Next()).ToList();

            Debug.Log($"📝 Mixed words (25 total): {string.Join(", ", mixedWords)}");

            // 🚀 7️.- Asignar palabras a los focos disponibles
            if (focusPositions == null || focusPositions.Length < 25)
            {
                Debug.LogError($"❌ Not enough focus positions! Found {focusPositions.Length}, need 25.");
                return;
            }

            for (int i = 0; i < 25; i++)
            {
                if (i >= focusPositions.Length)
                {
                    Debug.LogError($"❌ No more focus positions available at index {i}.");
                    break;
                }

                GameObject wordObject = Instantiate(wordPrefab, focusPositions[i].position, Quaternion.identity);
                Word wordScript = wordObject.GetComponent<Word>();

                if (wordScript != null)
                {
                    string currentWord = mixedWords[i];

                    // ⚠️ Verificar que la palabra no sea nula o vacía antes de asignarla
                    if (string.IsNullOrWhiteSpace(currentWord))
                    {
                        Debug.LogError($"❌ Empty word detected at index {i}. Skipping.");
                        continue;
                    }

                    wordScript.SetWord(currentWord);

                    // ✅ Verificar si la palabra es correcta
                    bool isCorrect = correctWords.Contains(currentWord);
                    wordScript.SetWordAsCorrect(isCorrect);
                }

                instantiatedWords.Add(wordObject);
            }
        }



        /// <summary>
        /// Extracts relevant words and mixes them with dictionary words.
        /// </summary>
        private List<string> GetMixedWords(Question question)
        {
            HashSet<string> correctWords = new HashSet<string>();

            // 🔹 1️⃣ Extraer palabras clave de las respuestas correctas
            foreach (var answer in question.correct_answers)
            {
                List<string> extractedWords = ExtractRelevantWords(answer.answer);
                correctWords.UnionWith(extractedWords);
            }

            // 🔹 2️⃣ Si no hay suficientes palabras correctas, reducir la restricción
            if (correctWords.Count < 2)
            {
                Debug.LogWarning("⚠ Too few relevant words detected, applying less restrictive filtering.");
                correctWords = question.correct_answers
                    .SelectMany(a => a.answer.Split(' '))
                    .Where(w => w.Length > 2)
                    .Take(2)
                    .ToHashSet();
            }

            // 🔹 3️⃣ Añadir palabras del diccionario hasta completar 25
            List<string> mixedWords = correctWords.ToList();
            List<string> dictionaryWords = dictionary.Except(correctWords).ToList(); // Evitar duplicados

            System.Random random = new System.Random();

            while (mixedWords.Count < 25 && dictionaryWords.Count > 0)
            {
                int index = random.Next(dictionaryWords.Count);
                mixedWords.Add(dictionaryWords[index]);
                dictionaryWords.RemoveAt(index);
            }

            // 🔹 4️⃣ Mezclar aleatoriamente todas las palabras
            return mixedWords.OrderBy(_ => random.Next()).ToList();
        }

        private List<string> ExtractRelevantWords(string sentence)
        {
            return sentence
                .Split(' ')
                .Select(word => word.ToLower().Trim(' ', '.', ',', '!', '?', ':', ';'))
                .Where(word => !stopWords.Contains(word) && (cybersecurityKeywords.Contains(word) || word.Length > 3))
                .ToList();
        }

        /// <summary>
        /// Verifies if a word is correct and logs it.
        /// </summary>
        public void VerifyWordInForge(string word)
        {
            if (correctWords.Contains(word.ToLower()))
            {
                Debug.Log($"✅ CORRECT WORD ADDED TO FORGE: {word}");
            }
            else
            {
                Debug.Log($"❌ INCORRECT WORD: {word}");
            }
        }
        /// <summary>
        /// Loads dictionary words from a JSON file.
        /// </summary>
        private void LoadDictionaryFromJson()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, dictionaryFileName);
            Debug.Log($"📂 Loading dictionary from: {filePath}");

            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                try
                {
                    DictionaryData dictionaryData = JsonUtility.FromJson<DictionaryData>(jsonContent);
                    dictionary = dictionaryData.cybersecurity_words ?? new List<string>();
                    Debug.Log($"✅ Loaded {dictionary.Count} dictionary words.");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"❌ Error parsing dictionary JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"❌ Dictionary file not found at: {filePath}");
            }
        }

        /// <summary>
        /// Loads a question based on its index.
        /// </summary>
        public void LoadQuestion(int questionIndex)
        {
            if (questions == null || questionIndex < 0 || questionIndex >= questions.Count)
            {
                Debug.LogError("❌ Question index out of range.");
                return;
            }

            currentQuestionIndex = questionIndex;
            CurrentQuestion = questions[questionIndex];

            Debug.Log($"📢 Loading question: {CurrentQuestion.text}");

            if (questionTextUpdater != null)
            {
                questionTextUpdater.UpdateQuestionText();
            }
            else
            {
                Debug.LogWarning("⚠ QuestionTextUpdater is not assigned.");
            }
        }

        /// <summary>
        /// Loads a random question from the list.
        /// </summary>
        public void LoadRandomQuestion()
        {
            if (questions == null || questions.Count == 0)
            {
                Debug.LogError("❌ No questions available to load.");
                return;
            }

            int randomIndex = Random.Range(0, questions.Count);
            LoadQuestion(randomIndex);
        }

        /// <summary>
        /// Loads words related to the current question.
        /// </summary>
        public void LoadWordsForCurrentQuestion_borrar()
        {
            if (CurrentQuestion == null)
            {
                Debug.LogError("❌ No question is currently loaded.");
                return;
            }

            // Clear previous words
            foreach (var word in instantiatedWords)
            {
                Destroy(word);
            }
            instantiatedWords.Clear();

            // Generate word list
            List<string> mixedWords = GetMixedWords(CurrentQuestion);
            Debug.Log($"📝 Mixed words: {string.Join(", ", mixedWords)}");

            // Instantiate words
            for (int i = 0; i < focusPositions.Length; i++)
            {
                if (i < mixedWords.Count)
                {
                    GameObject wordObject = Instantiate(wordPrefab, focusPositions[i].position, Quaternion.identity);
                    Word wordScript = wordObject.GetComponent<Word>();

                    if (wordScript != null)
                    {
                        string currentWord = mixedWords[i];
                        wordScript.SetWord(currentWord);

                        // ✅ Check if the word is in the correct answers
                        bool isCorrect = CurrentQuestion.correct_answers.Any(answer => answer.words.Contains(currentWord));
                        wordScript.SetWordAsCorrect(isCorrect);
                    }

                    instantiatedWords.Add(wordObject);
                }
            }
        }

        /// <summary>
        /// Generates a mix of correct words and distractor words.
        /// </summary>
        private List<string> GetMixedWords_borrar(Question question)
        {
            HashSet<string> correctWords = new HashSet<string>();

            // Extract unique words from correct answers
            foreach (var answer in question.correct_answers)
            {
                correctWords.UnionWith(answer.words);
            }

            // Combine with distractors from the dictionary
            List<string> mixedWords = correctWords.ToList();
            foreach (var word in dictionary)
            {
                if (mixedWords.Count >= 25) break;
                if (!mixedWords.Contains(word))
                {
                    mixedWords.Add(word);
                }
            }

            // Shuffle words
            System.Random random = new System.Random();
            return mixedWords.OrderBy(_ => random.Next()).ToList();
        }

        /// <summary>
        /// Returns the list of questions.
        /// </summary>
        public List<Question> GetQuestions()
        {
            return questions;
        }

        /// <summary>
        /// Loads a selected question from the UI.
        /// </summary>
        public void LoadSelectedQuestion(Question selectedQuestion)
        {
            if (selectedQuestion == null)
            {
                Debug.LogWarning("⚠ No selected question to load.");
                return;
            }

            CurrentQuestion = selectedQuestion;

            Debug.Log($"📢 Loading selected question: {CurrentQuestion.text}");

            if (questionTextUpdater != null)
            {
                questionTextUpdater.UpdateQuestionText();
            }
            else
            {
                Debug.LogWarning("⚠ QuestionTextUpdater is not assigned.");
            }
        }

        /// <summary>
        /// 📌 Obtiene un ID de pregunta aleatorio.
        /// </summary>
        public int GetRandomQuestionID()
        {
            if (questions == null || questions.Count == 0)
            {
                Debug.LogError("❌ No hay preguntas disponibles en el QuestionManager.");
                return -1;
            }

            return Random.Range(0, questions.Count); // Devuelve un índice aleatorio de la lista de preguntas
        }

        /// <summary>
        /// 📌 Establece la pregunta actual basada en un ID.
        /// </summary>
        public void SetCurrentQuestion(int questionID)
        {
            if (questionID < 0 || questionID >= questions.Count)
            {
                Debug.LogError($"❌ El ID de pregunta {questionID} está fuera de rango.");
                return;
            }

            CurrentQuestion = questions[questionID]; // Asigna la pregunta seleccionada
            Debug.Log($"✅ Pregunta establecida: {CurrentQuestion.text}");
        }


        /// <summary>
        /// Clears the current question and resets UI.
        /// </summary>
        public void ClearCurrentQuestion()
        {
            Debug.Log("[QuestionManager] 🔄 Clearing current question...");

            CurrentQuestion = null;

            if (questionTextUpdater != null)
            {
                foreach (var textElement in questionTextUpdater.questionTexts)
                {
                    textElement.text = "";
                }
            }

            Debug.Log("[QuestionManager] ✅ Current question cleared.");
        }

        // implementación logica terminar primera fase 1 de cada ronda

        private int lastScore = 0; // 🏆 Almacena la puntuación de la forja

        public int GetUsedWordCount()
        {
            return instantiatedWords.Count; // Devuelve cuántas palabras se han usado
        }

        public void EvaluateForge()
        {
            if (instantiatedWords == null || instantiatedWords.Count == 0)
            {
                Debug.LogWarning("[QuestionManager] ⚠️ No hay palabras en la forja para evaluar.");
                return;
            }

            // 🔹 Filtrar palabras destruidas antes de evaluarlas
            instantiatedWords = instantiatedWords.Where(word => word != null && word.gameObject != null).ToList();

            int totalWords = instantiatedWords.Count;
            int correctWords = instantiatedWords.Count(word => word.GetComponent<Word>() != null && word.GetComponent<Word>().isCorrectWord);

            Debug.Log($"[QuestionManager] ✅ Evaluación: {correctWords}/{totalWords} palabras correctas.");

            lastScore = correctWords; // 🏆 Guardamos la puntuación

            // 🔹 Asegurar que todas las palabras se han eliminado antes de continuar
            foreach (var word in instantiatedWords)
            {
                if (word != null && word.gameObject != null)
                {
                    Destroy(word.gameObject);
                }
            }

            instantiatedWords.Clear(); // Limpiar la lista para evitar referencias a objetos destruidos
        }


        public int GetLastScore()
        {
            return lastScore; // 🔄 Permite a otras clases obtener la puntuación
        }

        //

        public void StartAnswerSelection()
        {
            Debug.Log("📢 Iniciando fase de selección de respuestas...");

            // ✅ Limpiar las palabras en la escena
            foreach (var word in instantiatedWords)
            {
                Destroy(word);
            }
            instantiatedWords.Clear();

            // ✅ Mostrar la pregunta nuevamente
            if (questionTextUpdater != null)
            {
                questionTextUpdater.UpdateQuestionText();
            }

            // ✅ Cargar respuestas para la selección
            LoadAnswerOptions();
        }

        private void LoadAnswerOptions()
        {
            if (CurrentQuestion == null)
            {
                Debug.LogError("❌ No hay pregunta cargada.");
                return;
            }

            Debug.Log($"📜 Mostrando opciones de respuesta para: {CurrentQuestion.text}");

            // 🔹 Generar botones de respuestas en la UI
            foreach (var answer in CurrentQuestion.correct_answers)
            {
                Debug.Log($"✅ Respuesta correcta posible: {answer.answer}");
                // Aquí puedes agregar lógica para generar botones de respuesta en la UI.
            }
        }


        public bool IsCorrectWord(string word)
        {
            if (CurrentQuestion == null)
            {
                Debug.LogWarning("[QuestionManager] ❌ No hay una pregunta activa.");
                return false;
            }

            // Extraer todas las palabras correctas de la pregunta actual
            HashSet<string> correctWords = new HashSet<string>();

            foreach (var answer in CurrentQuestion.correct_answers)
            {
                correctWords.UnionWith(answer.words); // Agregar todas las palabras correctas
            }

            bool isCorrect = correctWords.Contains(word.ToLower());

            Debug.Log($"[QuestionManager] 🔎 Verificando palabra: {word} → {(isCorrect ? "✅ Correcta" : "❌ Incorrecta")}");

            return isCorrect;
        }



    }
}
 