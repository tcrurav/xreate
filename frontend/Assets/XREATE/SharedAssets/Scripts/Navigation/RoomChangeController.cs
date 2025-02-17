using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class RoomChangeController : MonoBehaviour
{
    public Button ClickToGoButton;
    public TMP_Text RedText;
    public TMP_Text GreenText;

    private readonly Vector3 OffsetRoomModuleA = new(87f, 6.39f, -25.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(87f, 6.75f, -15.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(87f, 6.75f, 19f);
    private readonly Vector3 OffsetRoomModuleB = new(87f, 6.39f, 28f);
    //private readonly Vector3 OffsetCorridorToLeisureModule = new(99f, 6.75f, 11.5f);
    private readonly Vector3 OffsetLeisureModule = new(104f, 5.75f, 16f);
    private readonly Vector3 OffsetBuildingA = new(0, 0, -12);
    private readonly Vector3 OffsetBuildingATopStairs = new(9, 4.02f, 0);

    public void EnableNextRoom()
    {
        ClickToGoButton.gameObject.SetActive(true);
        RedText.gameObject.SetActive(false);
        GreenText.gameObject.SetActive(true);
    }

    public void DisableNextRoom()
    {
        ClickToGoButton.gameObject.SetActive(false);
        RedText.gameObject.SetActive(true);
        GreenText.gameObject.SetActive(false);
    }

    public void ChangeToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleAScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetRoomModuleA);
        SetVisibility("ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER");
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorCScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleA);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleA);
        SetVisibility("ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER");
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorDScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleB);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleB);
        SetVisibility("ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER");
    }

    public void ChangeToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleBScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetRoomModuleB);
        //ChangePlayerPosition(MainManager.GetUser().id, new Vector3(100, 100, 100));
        MainNavigationManager.ChangePosition(OffsetRoomModuleB);
        SetVisibility("ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER");
    }

    //public void ChangeToCorridorToLeisureModule()
    //{
    //    //MainNetworkManager.HideAllStudentsOfOtherTeams();
    //    //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorFScene");
    //    //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToLeisureModule);
    //    MainNavigationManager.ChangePosition(OffsetCorridorToLeisureModule);
    //}

    public void ChangeToLeisureModule()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "LeisureModuleScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetLeisureModule);
        MainNavigationManager.ChangePosition(OffsetLeisureModule);
        SetVisibility("ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER");
    }

    public void ChangeToBuildingA()
    {
        //MainNetworkManager.ShowAllStudents();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "MainScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetBuildingA);
        SetVisibility("EVERYONE_SEE_EVERYONE");
    }

    public void ChangeToBuildingATopStairs()
    {
        //MainNetworkManager.ShowAllStudents();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "MainScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetBuildingATopStairs);
        SetVisibility("EVERYONE_SEE_EVERYONE");
    }

    //private void ChangePlayerPosition(int playerId, Vector3 newPosition)
    //{
    //    MainNetworkManager.ChangePlayerPosition(playerId, newPosition);
    //}

    public void SetVisibility(string typeOfRoom) // EVERYONE_SEE_EVERYONE, NOBODY_SEE_NOBODY, ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER
    {
        Debug.Log($"SetVisibility Principio - typeOfRoom: {typeOfRoom}");

        CurrentActivityManager.ChangeTeamIdInPlayerSync();

        Debug.Log($"SetVisibility - typeOfRoom: {typeOfRoom}");
        if (NetworkManager.Singleton.LocalClient != null)
        {
            Debug.Log($"después - SetVisibility - typeOfRoom: {typeOfRoom}");
            PlayerSync player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerSync>();
            Debug.Log($"después 2 - SetVisibility - typeOfRoom: {typeOfRoom}");
            if (player != null)
            {
                Debug.Log($"después 3 - SetVisibility - typeOfRoom: {typeOfRoom}");
                player.RequestVisibilityUpdateServerRpc(typeOfRoom);
                Debug.Log($"después 4 - SetVisibility - typeOfRoom: {typeOfRoom}");
            }
        }
    }
}
