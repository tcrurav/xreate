using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab de la carta
    public Transform board;       // Contenedor del tablero (GameBoard)
    public int rows = 4;          // Número de filas
    public int columns = 4;       // Número de columnas
    public TextMeshPro timerText; // Texto 3D del temporizador
    public float gameTime = 600f;  // Tiempo total del juego

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>
    {
        { "VPN", "Virtual Private Network" },
        { "Phishing", "A type of cyber attack" },
        { "Malware", "Software designed to disrupt" },
        { "Firewall", "A network security system" },
        { "RAT", "Remote Access Trojan" },
        { "DDoS", "Distributed Denial of Service" },
        { "Spoofing", "The act of disguising" },
        { "Encryption", "The process of converting data" }
    };

    private List<string> keys = new List<string>();   // Claves para las cartas
    private List<string> values = new List<string>(); // Valores para las cartas
    private VRCardFlip firstCard;                     // Primera carta seleccionada
    private VRCardFlip secondCard;                    // Segunda carta seleccionada
    private bool canFlip = true;                      // Controla si se pueden voltear cartas
    private int pairsFound = 0;                       // Contador de parejas encontradas
    private float remainingTime;                      // Tiempo restante del juego
    private bool isTimerStarted = false;              // Para controlar si el temporizador ya ha comenzado
    private Coroutine timerCoroutine;                  // Para almacenar la referencia del temporizador
    private float playerTime;                         // Variable para guardar el tiempo que el jugador tarda en el juego

    public static List<float> allPlayersTime = new List<float>(); // Lista para guardar el tiempo de todos los jugadores

    private void Start()
    {
        if (board == null)
        {
            Debug.LogError("GameBoard no asignado en el GameManager");
            return;
        }

        remainingTime = gameTime;
        GenerateBoard();
        UpdateTimerText();
    }

    private void GenerateBoard()
    {
        // Dividir claves y valores en listas separadas
        foreach (var pair in securityTerms)
        {
            keys.Add(pair.Key);   // Agregar la clave
            values.Add(pair.Value); // Agregar el valor
        }

        // Crear una lista de cartas y mezclarlas
        List<string> allCards = new List<string>(keys);
        allCards.AddRange(values);
        Shuffle(allCards); // Mezclar las cartas para la distribución aleatoria

        // Generar cartas en el tablero
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
            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateTimerText();
        }

        GameOver(false);
    }

    private void UpdateTimerText()
    {
        timerText.text = $"Time Remaining: {remainingTime:F0}s";
    }

    public void CardFlipped(VRCardFlip card)
    {
        if (!canFlip || card.IsLocked) return; // Si no se puede voltear la carta o ya está bloqueada, no hace nada

        // Iniciar el temporizador al tocar la primera carta
        if (!isTimerStarted)
        {
            isTimerStarted = true;
            timerCoroutine = StartCoroutine(GameTimer()); // Iniciar el temporizador solo cuando se toque la primera carta
        }

        if (firstCard == null)
        {
            firstCard = card; // Asignar la primera carta
        }
        else if (secondCard == null)
        {
            secondCard = card; // Asignar la segunda carta
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        canFlip = false; // Evitar que se giren más cartas mientras comprobamos la pareja

        yield return new WaitForSeconds(1f); // Esperar un segundo para mostrar las cartas giradas

        if (IsMatch(firstCard, secondCard)) // Comprobamos si es una pareja
        {
            firstCard.LockCard(); // Bloquear las cartas emparejadas
            secondCard.LockCard();
            pairsFound++;

            if (pairsFound >= securityTerms.Count) // Si ya encontramos todas las parejas
            {
                GameOver(true); // Terminar el juego con victoria
                yield break; // Salir del método
            }
        }
        else
        {
            firstCard.ResetCard(); // Volver a girar las cartas si no son iguales
            secondCard.ResetCard();
        }

        // Reiniciar el estado de las cartas
        firstCard = null;
        secondCard = null;
        canFlip = true; // Ahora se pueden voltear nuevas cartas
    }

    private bool IsMatch(VRCardFlip card1, VRCardFlip card2)
    {
        // Verificamos si el texto de las dos cartas coinciden
        return (securityTerms.ContainsKey(card1.cardText) && securityTerms[card1.cardText] == card2.cardText) ||
               (securityTerms.ContainsValue(card1.cardText) && GetKeyByValue(card1.cardText) == card2.cardText);
    }

    private string GetKeyByValue(string value)
    {
        foreach (var pair in securityTerms)
        {
            if (pair.Value == value) return pair.Key;
        }
        return null;
    }

    private void GameOver(bool won)
    {
        canFlip = false; // Ya no se pueden voltear cartas

        // Guardamos el tiempo del jugador al finalizar
        playerTime = gameTime - remainingTime;  // Calculamos el tiempo que el jugador ha tardado
        allPlayersTime.Add(playerTime);         // Guardamos el tiempo en la lista

        // Si ganaste, detener el temporizador
        if (won)
        {
            StopCoroutine(timerCoroutine);
            timerText.text = "You Win!!!";
        }
        else
        {
            // Si perdiste (tiempo agotado), reiniciar el juego
            RestartGame();
        }

        // Mostrar el tiempo del jugador (opcional)
        Debug.Log($"Player's Time: {playerTime:F2}s");
    }

    private void RestartGame()
    {
        // Reiniciar los valores del juego
        pairsFound = 0;
        remainingTime = gameTime;
        isTimerStarted = false;
        firstCard = null;
        secondCard = null;
        canFlip = true;

        // Limpiar el tablero (eliminar las cartas actuales)
        foreach (Transform child in board)
        {
            Destroy(child.gameObject);
        }

        // Generar un nuevo tablero
        GenerateBoard();
        UpdateTimerText();
    }

}
