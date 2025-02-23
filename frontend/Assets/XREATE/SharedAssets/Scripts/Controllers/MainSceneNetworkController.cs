using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CurrentActivityManager.Refresh());
    }
}
