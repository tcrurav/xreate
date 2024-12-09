using UnityEngine;

public class XRRigRoomChangeController : MonoBehaviour
{
    public GameObject XRRig;
    //private Vector3 origin = new Vector3(90.39f, 1.36f, 4.8f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //}

    public void ChangeToRoomModuleB()
    {
        XRRig.transform.position = new Vector3(7.5f, -5.0f, -34f);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        XRRig.transform.position = new Vector3(7.5f, -5.0f, -26f);
    }

    public void ChangeToBuildingA()
    {
        XRRig.transform.position = new Vector3(95.15f, 1.36f, -4.61f);
        //XRRig.transform.position = new Vector3(origin.x - 90.39f, 0.0f, origin.z - 4.8f - 12.0f);
    }
}
