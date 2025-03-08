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

    // It should be dynamic - But time is knapp
    private int[] studentIdsInAssignedPanels;
    private string[] studentNamesInAssignedPanels;

    private int numberOfConnectedStudents = 0;

    private void Start()
    {
        studentIdsInAssignedPanels = new int[MaxNumberOfStudents];
        studentNamesInAssignedPanels = new string[MaxNumberOfStudents];

        DisableAllStartButtons();
    }

    public int GetStudentIdByPanelIndex(int panelIndex)
    {
        Debug.Log($"RoomModuleBGameController - GetStudentIdByPanelIndex - panelIndex: {panelIndex}, studentIdsInAssignedPanels[panelIndex]: {studentIdsInAssignedPanels[panelIndex]}");
        return studentIdsInAssignedPanels[panelIndex];
    }

    public void OnPlayerReady(int index)
    {
        quizManager.GetComponent<QuizManager>().OnPlayerReady(index);
    }

    public void CheckAnswer(int questionIndex, int selectedIndex, int selectedAnswerIndex)
    {
        quizManager.GetComponent<QuizManager>().CheckAnswer(selectedIndex, selectedAnswerIndex);
    }

    public void StartGame()
    {
        Debug.Log($"RoomModuleBGameController - StartGame");
        GetListOfConnectedStudentsInCurrentTeam();

        quizManager.GetComponent<QuizManager>().SetTotalPlayers(numberOfConnectedStudents);

        EnableOwnStartButton();
    }

    private void EnableOwnStartButton()
    {
        Debug.Log($"RoomModuleBGameController - EnableOwnStartButton - numberOfConnectedStudents: {numberOfConnectedStudents}");
        for (int i = 0; i < numberOfConnectedStudents; i++)
        {
            StartButtons[i].SetActive(true);
            if (names[i].text == MainManager.GetUser().username)
            {
                StartButtons[i].GetComponent<Button>().interactable = true;
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

    private void GetListOfConnectedStudentsInCurrentTeam()
    {
        // TODO - Really ugly way - Time is Knapp
        // initialize with big values to sort later the array names[]
        int BIG_VALUE = 999999;
        for (int i = 0; i < MaxNumberOfStudents; i++)
        {
            studentIdsInAssignedPanels[i] = BIG_VALUE;
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
                studentNamesInAssignedPanels[panelIndex] = playerObject.GetComponent<PlayerSync>().PlayerName.Value.ToString();
                panelIndex++;
            }
        }

        numberOfConnectedStudents = panelIndex;

        Array.Sort(studentIdsInAssignedPanels, studentNamesInAssignedPanels);

        for (int i = 0; i < numberOfConnectedStudents; i++)
        {
            names[i].text = studentNamesInAssignedPanels[i];
        }
    }
}
