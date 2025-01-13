using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    public void ChangeToRoomModuleA()
    {
        GameObject XreatePortMain = GameObject.FindGameObjectWithTag("XreatePortMain");
        XreatePortMain.transform.position = new Vector3(7f, -5.0f, 17f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(7f, -5.0f, 17f);

        GameObject XreatePortRoomModuleA = GameObject.FindGameObjectWithTag("XreatePortRoomModuleA");
        XreatePortRoomModuleA.transform.position = new Vector3(7f, -5.0f, 17f);
    }

    public void ChangeToRoomModuleB()
    {
        GameObject XreatePortMain = GameObject.FindGameObjectWithTag("XreatePortMain");
        XreatePortMain.transform.position = new Vector3(7.5f, -5.0f, -34f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(7.5f, -5.0f, -34f);

        GameObject XreatePortRoomModuleA = GameObject.FindGameObjectWithTag("XreatePortRoomModuleA");
        XreatePortRoomModuleA.transform.position = new Vector3(7.5f, -5.0f, -34f);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        GameObject XreatePortMain = GameObject.FindGameObjectWithTag("XreatePortMain");
        XreatePortMain.transform.position = new Vector3(7.5f, -5.0f, -26f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(7.5f, -5.0f, -26f);

        GameObject XreatePortRoomModuleA = GameObject.FindGameObjectWithTag("XreatePortRoomModuleA");
        XreatePortRoomModuleA.transform.position = new Vector3(7.5f, -5.0f, -26f);
    }

    public void ChangeToBuildingA()
    {
        GameObject XreatePortMain = GameObject.FindGameObjectWithTag("XreatePortMain");
        XreatePortMain.transform.position = new Vector3(95.15f, 1.36f, -4.61f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(95.15f, 1.36f, -4.61f);

        GameObject XreatePortRoomModuleA = GameObject.FindGameObjectWithTag("XreatePortRoomModuleA");
        XreatePortRoomModuleA.transform.position = new Vector3(95.15f, 1.36f, -4.61f);
    }
}
