using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    public void GetAllPlayers()
    {
        MainNetworkManager.GetAllPlayers();
    }
}
