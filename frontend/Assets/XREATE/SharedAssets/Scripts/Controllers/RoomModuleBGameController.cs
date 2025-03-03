using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RoomModuleBGameController : MonoBehaviour
{
    public TMP_Text[] names;
    public GameObject[] StartButtons;
    public GameObject[] AnswerButtons;
    public GameObject[] Panels;
    public int teamId;
    public GameObject quizManager;

    private RoomModuleBGameManager roomModuleBGameManager;

    // It should be dynamic - But time is knapp
    private int[] studentIdsInAssignedPanels;
    private string[] studentNamesInAssignedPanels;

    private int numberOfConnectedStudents = 0;

    private void Start()
    {
        Debug.Log($"RoomModuleBGameController - Start");
        roomModuleBGameManager = GetComponent<RoomModuleBGameManager>();

        studentIdsInAssignedPanels = new int[5];
        studentNamesInAssignedPanels = new string[5];

        DisableAllStartButtons();
    }

    public void OnPlayerReady(int index)
    {
        quizManager.GetComponent<QuizManager>().OnPlayerReady(index);
    }

    public void CheckAnswer(int selectedIndex, int selectedAnswerIndex)
    {
        quizManager.GetComponent<QuizManager>().CheckAnswer(selectedIndex, selectedAnswerIndex);
    }

    public void StartGame()
    {
        Debug.Log($"RoomModuleBGameController - StartGame");

        GetListOfConnectedUsers();

        quizManager.GetComponent<QuizManager>().SetTotalPlayers(numberOfConnectedStudents);

        EnableOwnStartButton();
    }

    private void EnableOwnStartButton()
    {
        for(int i = 0; i < numberOfConnectedStudents; i++)
        {
            if (names[i].text == MainManager.GetUser().username) 
            {
                StartButtons[i].SetActive(true);
            }
        }
    }

    private void DisableAllStartButtons()
    {
        foreach (GameObject s in StartButtons)
        {
            s.gameObject.SetActive( false );
        }
    }

    private void GetListOfConnectedUsers()
    {
        Debug.Log($"RoomModuleBGameController - GetListOfConnectedUsers");
        int panelIndex = 0;
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            NetworkObject playerObject = client.PlayerObject;

            int studentId = playerObject.GetComponent<PlayerSync>().PlayerId.Value;
            int clientTeamId = CurrentActivityManager.GetTeamIdByStudentId(studentId);

            if (clientTeamId == teamId)
            {
                studentIdsInAssignedPanels[panelIndex] = studentId;
                studentNamesInAssignedPanels[panelIndex] = playerObject.GetComponent<PlayerSync>().PlayerName.Value.ToString();
                names[panelIndex].text = studentNamesInAssignedPanels[panelIndex];
                panelIndex++;
            }
        }

        Debug.Log($"RoomModuleBGameController - GetListOfConnectedUsers - panelIndex: {panelIndex}");

        numberOfConnectedStudents = panelIndex;
    }
}
