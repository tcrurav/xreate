using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class RoomModuleBGameController : MonoBehaviour
{
    public TMP_Text[] names;
    public GameObject[] StartButtons;
    public GameObject[] AnswerButtons;
    public int teamId;
    public GameObject quizManager;

    public int MaxNumberOfStudents;

    private RoomModuleBGameManager roomModuleBGameManager;

    // It should be dynamic - But time is knapp
    private int[] studentIdsInAssignedPanels;
    private string[] studentNamesInAssignedPanels;

    private int numberOfConnectedStudents = 0;

    private void Start()
    {
        Debug.Log($"RoomModuleBGameController - Start");
        roomModuleBGameManager = GetComponent<RoomModuleBGameManager>();

        studentIdsInAssignedPanels = new int[MaxNumberOfStudents];
        studentNamesInAssignedPanels = new string[MaxNumberOfStudents];

        DisableAllStartButtons();
    }

    public int GetStudentIdByPanelIndex( int panelIndex)
    {
        Debug.Log($"RoomModuleBGameController - GetStudentIdByPanelIndex - panelIndex: {panelIndex}, studentIdsInAssignedPanels[panelIndex]: {studentIdsInAssignedPanels[panelIndex]}");
        return studentIdsInAssignedPanels[panelIndex];
    }

    public void OnPlayerReady(int index)
    {
        Debug.Log($"RoomModuleBGameManager - OnPlayerReady - index: {index}");
        quizManager.GetComponent<QuizManager>().OnPlayerReady(index);
    }

    public void CheckAnswer(int questionIndex, int selectedIndex, int selectedAnswerIndex)
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
        for (int i = 0; i < numberOfConnectedStudents; i++)
        {
            if (names[i].text == MainManager.GetUser().username)
            {
                StartButtons[i].GetComponent<Button>().interactable = true;
                Debug.Log($"EnableOwnStartButton - i: {i}");
                return;
            }
        }
    }

    private void DisableAllStartButtons()
    {
        foreach (GameObject s in StartButtons)
        {
            s.gameObject.SetActive(false);
        }
    }

    private void GetListOfConnectedUsers()
    {
        Debug.Log($"RoomModuleBGameController - GetListOfConnectedUsers");

        // TODO - Really ugly way - Time is Knapp
        // initialize with big values to sort later the array names[]
        int[] aux = new int[MaxNumberOfStudents];
        int BIG_VALUE = 999999;
        for (int i = 0; i < MaxNumberOfStudents; i++)
        {
            studentIdsInAssignedPanels[i] = BIG_VALUE;
            aux[i] = BIG_VALUE;
        }

        int panelIndex = 0;
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            NetworkObject playerObject = client.PlayerObject;

            int studentId = playerObject.GetComponent<PlayerSync>().PlayerId.Value;
            int clientTeamId = CurrentActivityManager.GetTeamIdByStudentId(studentId);

            if (clientTeamId == teamId)
            {
                studentIdsInAssignedPanels[panelIndex] = studentId;
                aux[panelIndex] = studentId;
                studentNamesInAssignedPanels[panelIndex] = playerObject.GetComponent<PlayerSync>().PlayerName.Value.ToString();
                names[panelIndex].text = studentNamesInAssignedPanels[panelIndex];
                panelIndex++;
            }
        }

        numberOfConnectedStudents = panelIndex;

        Array.Sort(studentIdsInAssignedPanels, studentNamesInAssignedPanels);
        Array.Sort(aux, names);

        // TODO - Delete this - It's only for debugging purposes
        for (int i = 0; i < numberOfConnectedStudents; i++)
        {
            DebugManager.Log($"RoomModuleBGameController - studentIdsInAssignedPanels[i]: {studentIdsInAssignedPanels[i]}, studentNamesInAssignedPanels[i]: {studentNamesInAssignedPanels[i]}");
            DebugManager.Log($"RoomModuleBGameController - aux[i]: {aux[i]}, names: {names[i].text}");
        }
    }
}
