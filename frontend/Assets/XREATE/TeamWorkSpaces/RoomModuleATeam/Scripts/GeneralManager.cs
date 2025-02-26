using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] playerBoards; // Referencias a los tableros de los jugadores
    private int numberOfPlayers = 0;

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>();
    private ActivityChallengeConfigItemService activityChallengeConfigItemService;

    public CyberSecurityPanel cyberSecurityPanel;
    private AchievementItemService achievementItemService; // Service to submit points
    
    // Variables to submit points to the API
    public string challengeName; // Challenge name
    public string challengeItemItem; // Challenge item name
    private bool isPlayerInRoom = false; // Variable para saber si hay jugadores

    private void Start()
    {
        numberOfPlayers = GetNumberOfPlayersInRoom(); // Método que obtiene el número de jugadores 

        // Activar los tableros correspondientes según la cantidad de jugadores
        ActivatePlayerBoards(numberOfPlayers);

        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();
        StartCoroutine(LoadSecurityTerms());
    }

    private int GetNumberOfPlayersInRoom()
    {
        // Este método debería obtener el número de jugadores en la sala desde la base de datos o servicio
        return 4; // Ejemplo: suponiendo que hay 4 jugadores
    }

    private void ActivatePlayerBoards(int numPlayers)
    {
        for (int i = 0; i < playerBoards.Length; i++)
        {
            if (i < numPlayers)
            {
                playerBoards[i].SetActive(true); // Activar los tableros para los jugadores activos
            }
            else
            {
                playerBoards[i].SetActive(false); // Desactivar los tableros para los jugadores no activos
            }
        }
    }

    private List<PlayerScore> playerScores = new List<PlayerScore>();
    private bool isGameFinished = false;

    private void UpdateGameEnd()
    {
        if (isGameFinished)
        {
            DisplayScores();
         //   UploadPointsToAPI();
        }
    }

    private void DisplayScores()
    {
        float totalPoints = 0;
        foreach (var score in playerScores)
        {
            Debug.Log($"{score.playerName}: {score.points} points");
            totalPoints += score.points;
        }

        Debug.Log($"Total points: {totalPoints} points");
    }

    // Método para subir los puntos de cada jugador a la API
    private void UploadPointsToAPIPlayer()
    {
        // Subir los puntos de cada jugador a la API
        foreach (var score in playerScores)
        {
            StartCoroutine(OnQuizCompletedUpdatePoints(score.points, score.playerName));
        }
    }

    // Método para agregar los puntajes al finalizar el juego (lo llamamos desde el GameManager cuando cada jugador termine)
    public void AddPlayerScore(string playerName, float points)
    {
        playerScores.Add(new PlayerScore(playerName, points));

        if (playerScores.Count == numberOfPlayers) // Todos los jugadores han terminado
        {
            isGameFinished = true;
            UpdateGameEnd(); // Procedemos con la visualización y subida de puntos
        }
    }

    // Método para cargar todos los términos de seguridad desde la base de datos
    private IEnumerator LoadSecurityTerms()
    {
        yield return activityChallengeConfigItemService.GetAllById(CurrentActivityManager.GetCurrentActivityId());

        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
        {
            var termsDictionary = new InActivityChallengeConfigItemValueDictionary();

            foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
            {
                if (item.id < 100 || item.id > 150) // Filtrar solo los términos relevantes
                    continue;

                if (!string.IsNullOrEmpty(item.item) && !string.IsNullOrEmpty(item.value))
                {
                    termsDictionary.keys.Add(item.item);
                    termsDictionary.values.Add(item.value);
                }
            }

            // Convertir a un diccionario real
            securityTerms = termsDictionary.ToDictionary();

            // Llamar al método ReceiveSecurityTerms para pasarlos al CyberSecurityPanel
            cyberSecurityPanel.ReceiveSecurityTerms(securityTerms);  // Pasar los términos completos al panel de ciberseguridad

            // Ahora puedes obtener 8 parejas aleatorias para el GameManager
            var elementsGame = GetRandomPairs(8);  // Obtener las 8 parejas aleatorias
            gameManager.SetSecurityTerms(elementsGame);  // Pasar estas 8 parejas al GameManager
        }
    }

    // Método para seleccionar 8 parejas al azar del diccionario de términos
    private Dictionary<string, string> GetRandomPairs(int pairCount)
    {
        // Seleccionamos términos al azar (sin repetición)
        var selectedPairs = securityTerms.OrderBy(x => Random.value).Take(pairCount).ToList();
        var elementsGame = new Dictionary<string, string>();

        foreach (var pair in selectedPairs)
        {
            elementsGame.Add(pair.Key, pair.Value); // Añadir las 8 parejas al diccionario
        }

        return elementsGame;
    }

// Método para subir puntos a la API cuando se completa el quiz
private IEnumerator OnQuizCompletedUpdatePoints(float points, string playerName)
{
    if (MainManager.GetUser().role != "STUDENT")
    {
        throw new System.Exception("Error: Only students get points");
    }

    int studentId = MainManager.GetUser().id;
    int activityId = CurrentActivityManager.GetCurrentActivityId();

    yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
        challengeName, challengeItemItem, studentId, activityId, (int)points); // Convertir puntos a int si es necesario

    if (achievementItemService.responseCode != 200)
    {
       // Debug.LogError($"Error uploading points for {playerName}: {achievementItemService.responseMessage}");
        yield break;
    }

    Debug.Log($"Points for {playerName} uploaded successfully.");
}



    public class PlayerScore
    {
        public string playerName;
        public float points;

        public PlayerScore(string playerName, float points)
        {
            this.playerName = playerName;
            this.points = points;
        }


    }

}
