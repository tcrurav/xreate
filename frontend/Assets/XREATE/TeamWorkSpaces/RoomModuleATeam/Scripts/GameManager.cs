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
    public ParticleSystem victoryParticles;
    private bool isGameOver = false;
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
    private int playerPoints = 0;
    public static List<int> playerScores = new List<int>(); // Ahora solo almacena los puntos sin subirlos

    private void Start()
    {
        remainingTime = gameTime;
        GenerateBoard();
        UpdateTimerText();
    }

    private void GenerateBoard()
    {
        allCards.Clear();
        var selectedPairs = securityTerms.OrderBy(x => Random.value).Take(8).ToList();
        foreach (var pair in selectedPairs)
        {
            allCards.Add(pair.Key);
            allCards.Add(pair.Value);
        }
        Shuffle(allCards);
        int index = 0;
        float cardSpacing = 0.5f;
        float fixedX = 0f;
        float startZ = -(columns - 1) * cardSpacing / 2;
        float startY = -(rows - 1) * cardSpacing / 2;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                string cardText = allCards[index++];
                GameObject card = Instantiate(cardPrefab, board);
                card.transform.localPosition = new Vector3(fixedX, startY + row * cardSpacing, startZ + col * cardSpacing);
                VRCardFlip cardFlip = card.GetComponent<VRCardFlip>();
                cardFlip.SetCardText(cardText);
                cardFlip.gameManager = this;
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
            if (isGameOver) yield break;
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateTimerText();
        }
        if (!isGameOver) GameOver(false);
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
        canFlip = false;
        yield return new WaitForSeconds(1f);
        if (IsMatch(firstCard, secondCard))
        {
            firstCard.LockCard();
            secondCard.LockCard();
            pairsFound++;
            playerPoints += 5;
            if (pairsFound >= 8)
            {
                GameOver(true);
                yield break;
            }
        }
        else
        {
            firstCard?.ResetCard();
            secondCard?.ResetCard();
        }
        firstCard = null;
        secondCard = null;
        canFlip = true;
    }

    private bool IsMatch(VRCardFlip card1, VRCardFlip card2)
    {
        return securityTerms.ContainsKey(card1.cardText) && securityTerms[card1.cardText] == card2.cardText ||
               securityTerms.ContainsKey(card2.cardText) && securityTerms[card2.cardText] == card1.cardText;
    }

    private void GameOver(bool won)
    {
        isGameOver = true;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        if (won)
        {
            Debug.Log($"Congratulations! You scored {playerPoints} points.");
            timerText.text = $"YOU WIN! Points: {playerPoints}";
            playerScores.Add(playerPoints); // Ahora se almacena sin subir a la base de datos
            if (victoryParticles != null)
            {
                victoryParticles.Play();
            }
        }
        else
        {
            Debug.Log("Time’s up. Game Over.");
            timerText.text = "YOU HAVE LOST! Time out.";
        }
    }
}
