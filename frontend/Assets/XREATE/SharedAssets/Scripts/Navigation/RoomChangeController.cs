using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    public void ChangeToRoomModuleA()
    {
        Vector3 target = new Vector3(-7.5f, 6.5f, -20f);
        ChangePosition(target);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        Vector3 target = new Vector3(-7.5f, 6.75f, -13f);
        ChangePosition(target);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        Vector3 target = new Vector3(-7.5f, 6.75f, 21f);
        ChangePosition(target);
    }

    public void ChangeToRoomModuleB()
    {
        Vector3 target = new Vector3(-7.5f, 6.5f, 32f);
        ChangePosition(target);
    }

    public void ChangeToBuildingA()
    {
        Vector3 target = new Vector3(-95.15f, 0f, 4.61f);
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
    }
}
