using UnityEngine;

public class RoomChangeController : MonoBehaviour
{
    public GameObject XreatePortMain;
    //private Vector3 origin = new Vector3(90.39f, 1.36f, 4.8f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //}

    public void ChangeToRoomModuleB()
    {
        XreatePortMain.transform.position = new Vector3(7.5f, -5.0f, -34f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(7.5f, -5.0f, -34f);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        XreatePortMain.transform.position = new Vector3(7.5f, -5.0f, -26f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(7.5f, -5.0f, -26f);
    }

    public void ChangeToBuildingA()
    {
        XreatePortMain.transform.position = new Vector3(95.15f, 1.36f, -4.61f);

        GameObject XreatePortRoomModuleB = GameObject.FindGameObjectWithTag("XreatePortRoomModuleB");
        XreatePortRoomModuleB.transform.position = new Vector3(95.15f, 1.36f, -4.61f);
        //XRRig.transform.position = new Vector3(origin.x - 90.39f, 0.0f, origin.z - 4.8f - 12.0f);
    }
}
