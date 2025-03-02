//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;

//public class GeneralManager : MonoBehaviour
//{
//    public GameManager gameManager;
//    public GameObject[] playerBoards; // Referencias a los tableros de los jugadores
//    private int numberOfPlayers = 5;

//    private Dictionary<string, string> securityTerms = new Dictionary<string, string>();
//    private ActivityChallengeConfigItemService activityChallengeConfigItemService;

//    public CyberSecurityPanel cyberSecurityPanel;
//    private AchievementItemService achievementItemService; // Service to submit points


//    // Variables to submit points to the API
//    public string challengeName = "match-the-pairs";
//    public string challengeItemItem = "total points of a student in this challenge";
//    private bool isPlayerInRoom = false; // Variable para saber si hay jugadores


//    private void Start()
//    {
//        numberOfPlayers = GetNumberOfPlayersInRoom(); // Método que obtiene el número de jugadores 

//        // Activar los tableros correspondientes según la cantidad de jugadores
//        ActivatePlayerBoards(numberOfPlayers);

//        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

//        StartCoroutine(InitializeQuestionsSelectRandomQuestionsAndLoadQuestion());

//    }
//    private IEnumerator InitializeQuestionsSelectRandomQuestionsAndLoadQuestion()
//    {
//        yield return StartCoroutine(LoadSecurityTerms());
//    }

//    private int GetNumberOfPlayersInRoom()
//    {
//        return numberOfPlayers;
//    }

//    private void ActivatePlayerBoards(int numPlayers)
//    {
//        for (int i = 0; i < playerBoards.Length; i++)
//        {
//            if (i < numPlayers)
//            {
//                playerBoards[i].SetActive(true); // Activar los tableros para los jugadores activos
//            }
//            else
//            {
//                playerBoards[i].SetActive(false); // Desactivar los tableros para los jugadores no activos
//            }
//        }
//    }

//    private List<PlayerScore> playerScores = new List<PlayerScore>();
//    private bool isGameFinished = false;

//    private void UpdateGameEnd()
//    {
//        if (isGameFinished)
//        {
//            DisplayScores();
//            //   UploadPointsToAPI();
//        }
//    }

//    private void DisplayScores()
//    {
//        float totalPoints = 0;
//        foreach (var score in playerScores)
//        {
//            Debug.Log($"{score.playerName}: {score.points} points");
//            totalPoints += score.points;
//        }

//        Debug.Log($"Total points: {totalPoints} points");
//    }

//    // Método para subir los puntos de cada jugador a la API
//    private void UploadPointsToAPIPlayer()
//    {
//        // Subir los puntos de cada jugador a la API
//        foreach (var score in playerScores)
//        {
//            StartCoroutine(OnQuizCompletedUpdatePoints(score.points));
//        }
//    }

//    // Método para agregar los puntajes al finalizar el juego (lo llamamos desde el GameManager cuando cada jugador termine)
//    public void AddPlayerScore(string playerName, int points)
//    {
//        playerScores.Add(new PlayerScore(playerName, points));

//        if (playerScores.Count == numberOfPlayers) // Todos los jugadores han terminado
//        {
//            isGameFinished = true;
//            UpdateGameEnd(); // Procedemos con la visualización y subida de puntos
//        }
//    }

//    // Método para cargar todos los términos de seguridad desde la base de datos
//    private IEnumerator LoadSecurityTerms()
//    {
//        yield return activityChallengeConfigItemService.GetAllById(1);

//        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
//        {

//            foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
//            {
//                securityTerms[item.item]= item.value;
//            }
//            // -------------- ESTOS METODOS PARA REVISAR, FUNCIONAN PERO NO SE MUESTRAN CUANDO LA ESCENA ESTA CARGADA-------
//            // Llamar al método ReceiveSecurityTerms para pasarlos al CyberSecurityPanel
//            cyberSecurityPanel.ReceiveSecurityTerms(securityTerms);  // Pasar los términos completos al panel de ciberseguridad

//            // Ahora puedes obtener 8 parejas aleatorias para el GameManager
//            var elementsGame = GetRandomPairs(8);  // Obtener las 8 parejas aleatorias
//          //  gameManager.SetSecurityTerms(elementsGame);  // Pasar estas 8 parejas al GameManager
//        }
//    }

//    // Método para seleccionar 8 parejas al azar del diccionario de términos
//    private Dictionary<string, string> GetRandomPairs(int pairCount)
//    {
//        // Seleccionamos términos al azar (sin repetición)
//       // var selectedPairs = securityTerms.OrderBy(x => Random.value).Take(pairCount).ToList();
//        var elementsGame = new Dictionary<string, string>();

//       // foreach (var pair in selectedPairs)
//        {
//       //     elementsGame.Add(pair.Key, pair.Value); // Añadir las 8 parejas al diccionario
//        }

//        return elementsGame;
//    }

//    private IEnumerator OnQuizCompletedUpdatePoints(int points)
//    {
//        if (MainManager.GetUser().role != "STUDENT")
//        {
//            throw new System.Exception("Error: Only students get points");
//        }

//        int studentId = MainManager.GetUser().id;
//        int activityId = CurrentActivityManager.GetCurrentActivityId();

//        yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
//             challengeName, challengeItemItem, studentId, activityId, points);

//        if (achievementItemService.responseCode != 200)
//        {
//            yield break;
//        }

//    }

//    public class PlayerScore
//    {
//        public string playerName;
//        public int points;

//        public PlayerScore(string playerName, int points)
//        {
//            this.playerName = playerName;
//            this.points = points;
//        }


//    }

//}
