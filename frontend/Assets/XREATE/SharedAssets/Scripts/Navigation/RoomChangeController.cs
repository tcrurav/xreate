using Unity.Services.Lobbies.Models;
using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    private readonly Vector3 OffsetRoomModuleA = new(87f, 6.75f, -25.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(87f, 6.75f, -15.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(87f, 6.75f, 19f);
    private readonly Vector3 OffsetRoomModuleB = new(87f, 6.75f, 28f);
    private readonly Vector3 OffsetCorridorToLeisureModule = new(99f, 6.75f, 11.5f);
    private readonly Vector3 OffsetLeisureModule = new(104f, 6.5f, 16f);
    private readonly Vector3 OffsetBuildingA = new(0, 0, -12);

    public void ChangeToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleAScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorCScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleA);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorDScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleB);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleB);
    }

    public void ChangeToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleBScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetRoomModuleB);
        //ChangePlayerPosition(MainManager.GetUser().id, new Vector3(100, 100, 100));
        MainNavigationManager.ChangePosition(OffsetRoomModuleB);
    }

    public void ChangeToCorridorToLeisureModule()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorFScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToLeisureModule);
        MainNavigationManager.ChangePosition(OffsetCorridorToLeisureModule);
    }

    public void ChangeToLeisureModule()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "LeisureModuleScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetLeisureModule);
        MainNavigationManager.ChangePosition(OffsetLeisureModule);
    }

    public void ChangeToBuildingA()
    {
        //MainNetworkManager.ShowAllStudents();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "MainScene");
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetBuildingA);
    }

    private void ChangePlayerPosition(int playerId, Vector3 newPosition)
    {
        MainNetworkManager.ChangePlayerPosition(playerId, newPosition);
    }
}
