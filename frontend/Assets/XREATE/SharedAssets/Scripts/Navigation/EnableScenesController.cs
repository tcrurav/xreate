using UnityEngine;

public class EnableScenesController : MonoBehaviour
{
    public string[] sceneContainers;
    void OnEnable()
    {
        for (var i = 0; i < sceneContainers.Length; i++)
        {
            MainNavigationManager.EnableSceneContainer(sceneContainers[i]);
        }
    }
}
