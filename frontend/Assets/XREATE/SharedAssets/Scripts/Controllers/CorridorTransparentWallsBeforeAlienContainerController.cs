using UnityEngine;

public class CorridorTransparentWallsBeforeAlienContainerController: MonoBehaviour
{
    public GameObject AlienInCorridorManager;

    private void OnTriggerEnter(Collider other)
    {
        AlienInCorridorManager.GetComponent<AlienInCorridorController>().PlayerArrivesToBeforeAlienTransparentWall();
    }
}
