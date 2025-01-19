using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    //private Vector3 OffsetConnectedUsersSpatialPanelUI = new(0.86f, 0.71f, 0.0f);
    //private Vector3 OffsetUI = new(0.0f, 0.0f, -11.0f);
    private Vector3 OffsetRoomModuleA = new(-7.5f, 6.5f, -20f);
    private Vector3 OffsetCorridorToRoomModuleA = new(-7.5f, 6.75f, -13f);
    private Vector3 OffsetCorridorToRoomModuleB = new(-7.5f, 6.75f, 21f);
    private Vector3 OffsetRoomModuleB = new(-7.5f, 6.5f, 32f);
    private Vector3 OffsetBuildingA = new(-95.15f, 0f, 4.61f);

    public void ChangeToRoomModuleA()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        Vector3 target = OffsetRoomModuleA;
        ChangePosition(target);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        Vector3 target = OffsetCorridorToRoomModuleA;
        ChangePosition(target);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        Vector3 target = OffsetCorridorToRoomModuleB;
        ChangePosition(target);
    }

    public void ChangeToRoomModuleB()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
        Vector3 target = OffsetRoomModuleB;
        ChangePosition(target);
    }

    public void ChangeToBuildingA()
    {
        MainNetworkManager.ShowAllStudents();
        Vector3 target = OffsetBuildingA;
        ChangePosition(target);
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

        GameObject XreatePortTunnelConnectorC = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorC");
        XreatePortTunnelConnectorC.transform.position = offset;

        GameObject XreatePortTunnelConnectorF = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorF");
        XreatePortTunnelConnectorF.transform.position = offset;

        //GameObject XreatePortTunnelConnectorD = GameObject.FindGameObjectWithTag("XreatePortTunnelConnectorD");
        //XreatePortTunnelConnectorD.transform.position = offset;

        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        UI.transform.position = camera.transform.position + new Vector3(.5f,-0.5f,.5f);

        GameObject ConnectedUsersSpatialPanelUI = GameObject.FindGameObjectWithTag("ConnectedUsersSpatialPanelUI");
        ConnectedUsersSpatialPanelUI.transform.position = camera.transform.position + new Vector3(-.5f, -0.5f, .5f);
    }
}
