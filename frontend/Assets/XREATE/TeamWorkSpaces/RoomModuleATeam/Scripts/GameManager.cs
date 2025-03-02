using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
using NUnit.Framework;
using System.Net.NetworkInformation;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform board;
    public int rows = 4;
    public int columns = 4;
    public TextMeshPro timerText;
    public float gameTime = 600f;
    public ParticleSystem victoryParticles; // Reference to the particle system
    private bool isGameOver = false; // Variable so that the timer does not continue
    public TextMeshPro displayedCardTextVR;  // 3D text on the "VR screen"
    private int playerScore = 0; // New variable for points

    public Renderer lockPlaneRenderer;  // Plan where the image will change
    public Texture unlockedTexture;  // Image of open padlock
    public TextMeshPro gameResultText;  // 3D text to display information

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
    public AudioClip wrongPairSound;
    private AudioSource audioSource;
    public AudioClip correctPairSound;
    private float playerTime;
    private bool isAnyCardFlipping = false;
    public static List<float> allPlayersTime = new List<float>();
    private float penaltyTime = 0f;

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

        // Select 8 full terms (key + value)
        var selectedPairs = securityTerms.OrderBy(x => Random.value).Take(8).ToList();

        foreach (var pair in selectedPairs)
        {
            allCards.Add(pair.Key);   
            allCards.Add(pair.Value);
        }

        Shuffle(allCards);

        // Placing cards in a uniform grid
        int index = 0;
        float cardSpacing = 0.5f; // Uniform spacing between cards (horizontal and vertical)
        float fixedX = 0f;        // Keep the X axis constant
        float startZ = -(columns - 1) * cardSpacing / 2; // Center the columns horizontally
        float startY = -(rows - 1) * cardSpacing / 2;    // Center the rows vertically

        for (int row = 0; row < rows; row++)  // Loop through rows
        {
            for (int col = 0; col < columns; col++) // Loop through columns
            {
                string cardText = allCards[index++];  // Assign letter text
                GameObject card = Instantiate(cardPrefab, board);  // Instantiate letter

                // Position cards in a uniform grid
                card.transform.localPosition = new Vector3(
                    fixedX,                         // All cards share the same X plane
                    startY + row * cardSpacing,     // Vertical positioning on the Y axis
                    startZ + col * cardSpacing      // Horizontal positioning on the Z axis
                );

                // Card configuration (associate text and GameManager)
                VRCardFlip cardFlip = card.GetComponent<VRCardFlip>();
                cardFlip.SetCardText(cardText);  // Assign the letter text
                cardFlip.gameManager = this;    // Associate the GameManager
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
            if (isGameOver) yield break;  // If the game is over, stop the timer
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateTimerText();
        }

        if (!isGameOver)  // Prevent GameOver from being activated if it has already been won
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
        canFlip = false;  // Block interactions while verifying
        yield return new WaitForSeconds(0.5f);  // Wait for the player to see the cards

        if (IsMatch(firstCard, secondCard))
        {
            firstCard.LockCard();
            secondCard.LockCard();
            pairsFound++;
            playerScore += 2; // ADD POINTS PER COUPLE

            audioSource.PlayOneShot(correctPairSound);

            if (pairsFound >= 8)
            {
                playerTime += penaltyTime;  // Add penalty only if you win
                GameOver(true);
                yield break;  // Stop the function
            }
        }
        else
        {
            // Ensure both cards are flipped if they don't match
            firstCard?.ResetCard();
            secondCard?.ResetCard();
            remainingTime = Mathf.Max(0, remainingTime - 10f);  // Time penalty
            UpdateTimerText();

            if (wrongPairSound != null) // Play error sound
            {
                audioSource.PlayOneShot(wrongPairSound);
            }
        }

        // Reset variables for next round
        firstCard = null;
        secondCard = null;
        canFlip = true;  // Allow turns again
    }


    private bool IsMatch(VRCardFlip card1, VRCardFlip card2)
    {
        // If the first card is a key, we check that the second is the corresponding value
        bool isCard1Key = securityTerms.ContainsKey(card1.cardText);
        bool isCard2Key = securityTerms.ContainsKey(card2.cardText);

        // If both are keys or both are values, there is no match
        if (isCard1Key == isCard2Key) return false;

        // Check if there is a key-value match
        if (isCard1Key && securityTerms[card1.cardText] == card2.cardText)
        {
            return true;
        }
        if (isCard2Key && securityTerms[card2.cardText] == card1.cardText)
        {
            return true;
        }

        return false; // If none of the above conditions are met, there is no match
    }

    private void GameOver(bool won)
    {
        isGameOver = true;  //Mark that the game is over to avoid restarts

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);  // Completely stop the timer
            timerCoroutine = null;
        }

        // Change the Plane texture to show the open lock
        if (lockPlaneRenderer != null && unlockedTexture != null)
        {
            lockPlaneRenderer.material.mainTexture = unlockedTexture;
        }

        // Get the player's name (you can have him enter it before)
        string playerName = MainManager.GetUser().username;


        if (won)
        {
            playerTime += (gameTime - remainingTime);
            allPlayersTime.Add(playerTime);
            
            timerText.text = $"¡GANASTE! Tiempo: {playerTime:F2} seg - Puntos: {playerScore}";

            // Update 3D text
            gameResultText.text = $"¡Ganaste!\n" +
                                  $"{playerName}\n" +
                                  $"Tiempo: {playerTime:F2} seg\n" +
                                  $"Puntos: {playerScore}";

            if (victoryParticles != null)
            {
                victoryParticles.Play();
            }
        }
        else
        {
            penaltyTime += 10f;
            timerText.text = "YOU HAVE LOST! Time out.";

            // Show Game Over message in the UI
            gameResultText.text = $"¡Perdiste!\n" +
                                  $"{playerName}\n" +
                                  $"Tiempo: {gameTime - remainingTime:F2} seg\n" +
                                  $"Puntos: {playerScore}";

        }
    }

    public void UpdateDisplayedTextVR(string text)
    {
        if (displayedCardTextVR != null)
        {
            displayedCardTextVR.text = text;  // Show the text of the rotated card
            displayedCardTextVR.gameObject.SetActive(!string.IsNullOrEmpty(text)); // Show only if there is text
        }
    }

    public bool CanFlip()
    {
        return !isAnyCardFlipping && canFlip && !isGameOver; // Prevent cards from being turned over after victory/defeat

    }

    public void SetFlippingState(bool state)
    {
        isAnyCardFlipping = state;
    }

}
