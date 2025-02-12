using UnityEngine;

public class CorridorTransparentWallsAtEntryContainerController : MonoBehaviour
{
    public GameObject AlienInCorridorManager;

    private void OnTriggerEnter(Collider other)
    {
        AlienInCorridorManager.GetComponent<AlienInCorridorController>().PlayerArrivesToAtEntryTransparentWall();
    }
}
