using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    private readonly Vector3 OffsetRoomModuleA = new(-7.5f, 6.5f, -20f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(-7.5f, 6.75f, -13f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(-7.5f, 6.75f, 21f);
    private readonly Vector3 OffsetRoomModuleB = new(-7.5f, 6.5f, 32f);
    private readonly Vector3 OffsetCorridorToLeisureModule = new(4f, 6.75f, 16f);
    private readonly Vector3 OffsetLeisureModule = new(9.65f, 6.5f, 19.5f);
    private readonly Vector3 OffsetBuildingA = new(-95.15f, 0f, 4.61f);

    public void ChangeToRoomModuleA()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetCorridorToRoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetCorridorToRoomModuleB);
    }

    public void ChangeToRoomModuleB()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetRoomModuleB);
    }

    public void ChangeToCorridorToLeisureModule()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetCorridorToLeisureModule);
    }

    public void ChangeToLeisureModule()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        ChangePosition(OffsetLeisureModule);
    }

    public void ChangeToBuildingA()
    {
        MainNetworkManager.ShowAllStudents();
        ChangePosition(OffsetBuildingA);
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
}
