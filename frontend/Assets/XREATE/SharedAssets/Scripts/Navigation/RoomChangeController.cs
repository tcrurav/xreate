using Unity.Services.Lobbies.Models;
using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    private readonly Vector3 OffsetRoomModuleA = new(87f, 6.75f, -25.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(87f, 6.75f, -15.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(87f, 6.75f, 19f);
    private readonly Vector3 OffsetRoomModuleB = new(87f, 6.75f, 28f);
    private readonly Vector3 OffsetCorridorToLeisureModule = new(99f, 6.75f, 11.5f);
    private readonly Vector3 OffsetLeisureModule = new(105f, 6.5f, 21.5f);
    private readonly Vector3 OffsetBuildingA = new(0, 0, -12);

    //private readonly Vector3 OffsetRoomModuleA = new(-7.5f, 6.5f, -20f);
    //private readonly Vector3 OffsetCorridorToRoomModuleA = new(-7.5f, 6.75f, -13f);
    //private readonly Vector3 OffsetCorridorToRoomModuleB = new(-7.5f, 6.75f, 21f);
    //private readonly Vector3 OffsetRoomModuleB = new(-7.5f, 6.5f, 32f);
    //private readonly Vector3 OffsetCorridorToLeisureModule = new(4f, 6.75f, 16f);
    //private readonly Vector3 OffsetLeisureModule = new(9.65f, 6.5f, 19.5f);
    //private readonly Vector3 OffsetBuildingA = new(-95.15f, 0f, 4.61f);

    public void ChangeToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleAScene");
        //ChangePosition(OffsetRoomModuleA);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorCScene");
        //ChangePosition(OffsetCorridorToRoomModuleA);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleA);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorDScene");
        //ChangePosition(OffsetCorridorToRoomModuleB);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToRoomModuleB);
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleB);
    }

    public void ChangeToRoomModuleB()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "RoomModuleBScene");
        //ChangePosition(OffsetRoomModuleB);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetRoomModuleB);
        //ChangePlayerPosition(MainManager.GetUser().id, new Vector3(100, 100, 100));
        MainNavigationManager.ChangePosition(OffsetRoomModuleB);
    }

    public void ChangeToCorridorToLeisureModule()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "TunnelConnectorFScene");
        //ChangePosition(OffsetCorridorToLeisureModule);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetCorridorToLeisureModule);
        MainNavigationManager.ChangePosition(OffsetCorridorToLeisureModule);
    }

    public void ChangeToLeisureModule()
    {
        //MainNetworkManager.HideAllStudentsOfOtherTeams();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "LeisureModuleScene");
        //ChangePosition(OffsetLeisureModule);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetLeisureModule);
        MainNavigationManager.ChangePosition(OffsetLeisureModule);
    }

    public void ChangeToBuildingA()
    {
        //MainNetworkManager.ShowAllStudents();
        //MainNetworkManager.ChangeSceneTo(MainManager.GetUser().id, "MainScene");
        //ChangePosition(OffsetBuildingA);
        //ChangePlayerPosition(MainManager.GetUser().id, OffsetBuildingA);
        MainNavigationManager.ChangePosition(OffsetBuildingA);
    }

    private void ChangePosition(Vector3 target)
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 offset = camera.transform.position - target;

        GameObject XreatePortMain = GameObject.FindGameObjectWithTag("XreatePortMain");
        XreatePortMain.transform.position = offset;

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = offset;

        GameObject XreatePortRoomModuleA = GameObject.FindGameObjectWithTag("XreatePortRoomModuleA");
        XreatePortRoomModuleA.transform.position = offset;

        GameObject XreatePortLeisureModule = GameObject.FindGameObjectWithTag("XreatePortLeisureModule");
        XreatePortLeisureModule.transform.position = offset;

        GameObject XreatePortTunnelConnectorC = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorC");
        XreatePortTunnelConnectorC.transform.position = offset;

        GameObject XreatePortTunnelConnectorF = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorF");
        XreatePortTunnelConnectorF.transform.position = offset;

        GameObject XreatePortTunnelConnectorD = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorD");
        XreatePortTunnelConnectorD.transform.position = offset;

        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        UI.transform.position = camera.transform.position + new Vector3(.5f, -0.5f, .5f);

        GameObject ConnectedUsersSpatialPanelUI = GameObject.FindGameObjectWithTag("ConnectedUsersSpatialPanelUI");
        ConnectedUsersSpatialPanelUI.transform.position = camera.transform.position + new Vector3(-.5f, -0.5f, .5f);
    }

    private void ChangePlayerPosition(int playerId, Vector3 newPosition)
    {
        MainNetworkManager.ChangePlayerPosition(playerId, newPosition);
    }
}
