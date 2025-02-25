using UnityEngine;

public class DisableScenesController : MonoBehaviour
{
    public string[] sceneContainers;
    void OnEnable()
    {
        Debug.Log("DisableScenesController");
        for (var i = 0; i < sceneContainers.Length; i++)
        {
            MainNavigationManager.DisableSceneContainer(sceneContainers[i]);
        }
    }
}
