using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RoomModuleAGameController : MonoBehaviour
{
    public TMP_Text[] names;
    public GameObject[] GameManagers;
    public int teamId;

    public Texture[] unlockedTextures;
    public Renderer lockPlaneRenderer;

    public int MaxNumberOfStudents;

    private RoomModuleAGameManager roomModuleAGameManager;

    // It should be dynamic - But time is knapp
    private int[] studentIdsInAssignedPanels;
    private string[] studentNamesInAssignedPanels;

    private int numberOfConnectedStudents = 0;

    private void Start()
    {
        Debug.Log($"RoomModuleAGameController - Start");
        roomModuleAGameManager = GetComponent<RoomModuleAGameManager>();

        studentIdsInAssignedPanels = new int[MaxNumberOfStudents];
        studentNamesInAssignedPanels = new string[MaxNumberOfStudents];
    }

    public void UpdateTextureForNumberOfFinishedPanels(int numberOfFinishedPanels)
    {
        Debug.Log($"RoomModuleAGameController - FinishedPanel");

        if (lockPlaneRenderer != null && unlockedTextures[numberOfFinishedPanels] != null)
        {
            lockPlaneRenderer.material.mainTexture = unlockedTextures[numberOfFinishedPanels];
        }

        if (numberOfFinishedPanels == numberOfConnectedStudents)
        {
            roomModuleAGameManager.ChangeEnableStartReadyToNextRoomServerRpc(true);
        }
    }

    public void StartGame()
    {
        Debug.Log($"RoomModuleAGameController - StartGame");

        GetListOfConnectedUsers();

        EnableOwnStartPanel();
    }

    private void EnableOwnStartPanel()
    {
        for (int i = 0; i < numberOfConnectedStudents; i++)
        {
            if (names[i].text == MainManager.GetUser().username)
            {
                // TODO - Enable other panels - Maybe just the cards at the end with final solution repeated in all maybe.
                GameManagers[i].SetActive(true);
            }
        }
    }

    private void GetListOfConnectedUsers()
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
