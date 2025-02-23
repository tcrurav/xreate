using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform board;
    public int rows = 4;
    public int columns = 4;
    public TextMeshPro timerText;
    public float gameTime = 600f;
    public ParticleSystem victoryParticles; // Referencia al sistema de partículas
    private bool isGameOver = false; // Variable para que el temporizador no continue

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>
    {
        { "VPN", "Virtual Private Network" },
        { "Phishing", "A type of cyber attack" },
        { "Malware", "Software designed to disrupt" },
        { "Firewall", "A network security system" },
        { "RAT", "Remote Access Trojan" },
        { "DDoS", "Distributed Denial of Service" },
        { "Spoofing", "The act of disguising" },
        { "Encryption", "The process of converting data" },
        { "Brute Force", "A trial-and-error attack" },
        { "Botnet", "A network of infected computers" },
        { "Zero-Day", "A vulnerability unknown to vendors" },
        { "SQL Injection", "A code injection technique" },
        { "Keylogger", "A software that records keystrokes" },
        { "Ransomware", "A malware that locks files for ransom" },
        { "Trojan", "A deceptive malware" },
        { "Spyware", "A malware that spies on user data" },
        { "Adware", "A software that displays unwanted ads" },
        { "Backdoor", "An undocumented way to access a system" },
        { "Social Engineering", "Manipulating people to divulge secrets" },
        { "Man-in-the-Middle", "Intercepting communication between parties" },
        { "Penetration Testing", "Assessing security by simulating attacks" },
        { "Dark Web", "A hidden part of the internet" },
        { "Ethical Hacking", "Hacking for security improvement" },
        { "Rootkit", "A software that enables unauthorized access" },
        { "Session Hijacking", "Taking control of an active session" },
        { "Two-Factor Authentication", "A security measure requiring two forms of verification" },
        { "Cryptojacking", "Unauthorized use of a device to mine cryptocurrency" },
        { "Digital Forensics", "Investigating cybercrimes" },
        { "Patch Management", "Keeping software updated to fix vulnerabilities" },
        { "IDS", "Intrusion Detection System" }
    };

    private List<string> allCards = new List<string>();
    private VRCardFlip firstCard;
    private VRCardFlip secondCard;
    private bool canFlip = true;
    private int pairsFound = 0;
    private float remainingTime;
    private bool isTimerStarted = false;
    private Coroutine timerCoroutine;
    public AudioClip wrongPairSound; // sonido equivocacion
    private AudioSource audioSource;
    public AudioClip correctPairSound; // Sonido cuando se acierta una pareja
    private float playerTime;
    public static List<float> allPlayersTime = new List<float>();
    private float penaltyTime = 0f; // Variable para acumular el tiempo de penalización

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (board == null)
        {
            Debug.LogError("GameBoard not assigned in GameManager");
            return;
        }

        remainingTime = gameTime;
        GenerateBoard();
        UpdateTimerText();
    }

    private void GenerateBoard()
    {
        allCards.Clear();

        // Seleccionar 8 términos completos (clave + valor)
        var selectedPairs = securityTerms.OrderBy(x => Random.value).Take(8).ToList();

        foreach (var pair in selectedPairs)
        {
            allCards.Add(pair.Key);   // Añadir la clave
            allCards.Add(pair.Value); // Añadir el valor
        }

        Shuffle(allCards);

        // Colocación de cartas en una cuadrícula uniforme
        int index = 0;
        float cardSpacing = 0.5f; // Separación uniforme entre cartas (horizontal y vertical)
        float fixedX = 0f;        // Mantener constante el eje X
        float startZ = -(columns - 1) * cardSpacing / 2; // Centrar las columnas horizontalmente
        float startY = -(rows - 1) * cardSpacing / 2;    // Centrar las filas verticalmente

        for (int row = 0; row < rows; row++)  // Recorrer filas
        {
            for (int col = 0; col < columns; col++)  // Recorrer columnas
            {
                string cardText = allCards[index++];  // Asignar texto de carta
                GameObject card = Instantiate(cardPrefab, board);  // Instanciar carta

                // Posicionar las cartas en una cuadrícula uniforme
                card.transform.localPosition = new Vector3(
                    fixedX,                          // Todas las cartas comparten el mismo plano X
                    startY + row * cardSpacing,      // Posicionamiento vertical en el eje Y
                    startZ + col * cardSpacing       // Posicionamiento horizontal en el eje Z
                );

                // Configuración de la carta (asociar texto y GameManager)
                VRCardFlip cardFlip = card.GetComponent<VRCardFlip>();
                cardFlip.SetCardText(cardText);  // Asignar el texto de la carta
                cardFlip.gameManager = this;    // Asociar el GameManager
            }
        }
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            if (isGameOver) yield break;  // Si el juego terminó, detener el temporizador

            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateTimerText();
        }

        if (!isGameOver)  // Evita que se active GameOver si ya se ganó
        {
            GameOver(false);
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = $"Time Remaining: {remainingTime:F0}s";
    }

    public void CardFlipped(VRCardFlip card)
    {
        if (!canFlip || card.IsLocked) return;

        if (!isTimerStarted)
        {
            isTimerStarted = true;
            timerCoroutine = StartCoroutine(GameTimer());
        }

        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        canFlip = false;  // Bloquear interacciones mientras se verifica
        yield return new WaitForSeconds(1f);  // Esperar para que el jugador vea las cartas

        if (IsMatch(firstCard, secondCard))
        {
            firstCard.LockCard();
            secondCard.LockCard();
            pairsFound++;
            audioSource.PlayOneShot(correctPairSound);

            if (pairsFound >= 8)
            {
                playerTime += penaltyTime;  // Sumar penalización solo si gana
                GameOver(true);
                yield break;  // Detener la función
            }
        }
        else
        {
            // Asegurar que ambas cartas se volteen si no coinciden
            firstCard?.ResetCard();
            secondCard?.ResetCard();
            remainingTime = Mathf.Max(0, remainingTime - 10f);  // Penalización de tiempo
            UpdateTimerText();

            if (wrongPairSound != null) // Reproduce sonido error
            {
                audioSource.PlayOneShot(wrongPairSound);
            }
        }

        // Restablecer variables para la siguiente ronda
        firstCard = null;
        secondCard = null;
        canFlip = true;  // Permitir giros nuevamente
    }


    private bool IsMatch(VRCardFlip card1, VRCardFlip card2)
    {
        return securityTerms.ContainsKey(card1.cardText) && securityTerms[card1.cardText] == card2.cardText ||
               securityTerms.ContainsKey(card2.cardText) && securityTerms[card2.cardText] == card1.cardText;
    }

    private void GameOver(bool won)
    {
        isGameOver = true;  //Marcar que el juego ha terminado para evitar reinicios

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);  // Detener completamente el temporizador
            timerCoroutine = null;
        }

        if (won)
        {
            playerTime += (gameTime - remainingTime);
            allPlayersTime.Add(playerTime);

            Debug.Log($" Congratulations! You have won in {playerTime:F2} seconds.");
            timerText.text = $"YOU WIN! Time: {playerTime:F2} seconds";

            if (victoryParticles != null)
            {
                victoryParticles.Play();
            }
        }
        else
        {
            penaltyTime += 10f;
            Debug.Log("Time’s up. Game Over.");
            timerText.text = "YOU HAVE LOST! Time out.";
            StartCoroutine(RestartGameWithPenalty());  //Se ejecuta solo en caso de derrota
        }
    }

    private IEnumerator RestartGameWithPenalty()
    {
        yield return new WaitForSeconds(3f);  //Espera antes de reiniciar

        //  Destruir cartas antiguas
        foreach (Transform card in board)
        {
            Destroy(card.gameObject);
        }

        GenerateBoard();  // Generar nuevo tablero

        // Reiniciar valores del juego
        pairsFound = 0;
        canFlip = true;
        firstCard = null;
        secondCard = null;

        remainingTime = gameTime;  // Reiniciar solo si ha perdido
        isTimerStarted = false;

        UpdateTimerText();  //Mostrar tiempo actualizado
        timerText.gameObject.SetActive(true);

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);  //Detener cualquier temporizador previo
        }
        timerCoroutine = StartCoroutine(GameTimer());  //Iniciar de nuevo el temporizador
    }

    public TextMeshPro displayedCardTextVR;  // Texto 3D en la "pantalla VR"

    public void UpdateDisplayedTextVR(string text)
    {
        if (displayedCardTextVR != null)
        {
            displayedCardTextVR.text = text;  //  Mostrar el texto de la carta girada
            displayedCardTextVR.gameObject.SetActive(!string.IsNullOrEmpty(text)); // Mostrar solo si hay texto
        }
    }

}
