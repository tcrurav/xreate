using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame3D: MonoBehaviour
{
    public GameObject cardPrefab; // Prefab de la carta
    public Transform cardContainer; // Contenedor donde se generarán las cartas (asignado desde la consola)
    public Material[] cardMaterials; // Materiales para las parejas de cartas
    public int totalPairs = 8; // Número de parejas
    public TextMesh timerText; // Texto 3D para el temporizador

    private Dictionary<int, int> cardPairs; // Diccionario de parejas
    private List<GameObject> cards; // Lista de cartas generadas
    private GameObject firstCard; // Primera carta seleccionada
    private GameObject secondCard; // Segunda carta seleccionada
    private float timer = 0f; // Temporizador
    private bool gameRunning = true; // Estado del juego
    private int pairsFound = 0; // Número de parejas encontradas

    void Start()
    {
        GenerateCards();
    }

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            timerText.text = "Tiempo: " + timer.ToString("F2") + "s";
        }
    }

    // Generar las cartas dentro del contenedor
    void GenerateCards()
    {
        cards = new List<GameObject>();
        cardPairs = new Dictionary<int, int>();
        List<int> ids = new List<int>();

        // Generar IDs para las parejas
        for (int i = 0; i < totalPairs; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        // Barajar los IDs
        Shuffle(ids);

        // Calcular posiciones de las cartas dentro del contenedor
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(ids.Count));
        float spacing = 0.15f; // Espaciado entre cartas (ajusta según el tamaño de la pantalla de la consola)
        int index = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                if (index >= ids.Count) break;

                // Crear carta
                GameObject newCard = Instantiate(cardPrefab, cardContainer);
                newCard.transform.localPosition = new Vector3(x * spacing, 0, z * spacing);
                newCard.transform.localScale = Vector3.one * 0.1f; // Escala ajustada a la pantalla de la consola
                int cardId = ids[index];
                cardPairs[newCard.GetInstanceID()] = cardId;

                // Configurar la carta
                newCard.GetComponent<Card3DScript>().Setup(cardId, cardMaterials[cardId], this);
                cards.Add(newCard);
                index++;
            }
        }
    }

    // Barajar lista de IDs
    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Manejar la selección de una carta
    public void OnCardSelected(GameObject selectedCard)
    {
        if (firstCard == null)
        {
            firstCard = selectedCard;
        }
        else if (secondCard == null && selectedCard != firstCard)
        {
            secondCard = selectedCard;
            StartCoroutine(CheckMatch());
        }
    }

    // Comprobar si las dos cartas seleccionadas son pareja
    IEnumerator CheckMatch()
    {
        int firstId = cardPairs[firstCard.GetInstanceID()];
        int secondId = cardPairs[secondCard.GetInstanceID()];

        if (firstId == secondId)
        {
            // Es pareja
            pairsFound++;
            firstCard.GetComponent<Card3DScript>().SetMatched();
            secondCard.GetComponent<Card3DScript>().SetMatched();
            firstCard = null;
            secondCard = null;

            // Verificar si el juego ha terminado
            if (pairsFound == totalPairs)
            {
                gameRunning = false;
                Debug.Log("¡Juego terminado! Tiempo: " + timer.ToString("F2") + "s");
            }
        }
        else
        {
            // No es pareja, girar las cartas de nuevo
            yield return new WaitForSeconds(1f);
            firstCard.GetComponent<Card3DScript>().FlipDown();
            secondCard.GetComponent<Card3DScript>().FlipDown();
            firstCard = null;
            secondCard = null;
        }
    }
}
