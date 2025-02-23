using UnityEngine;

public class EnableScenesController : MonoBehaviour
{
    public string[] sceneContainers;
    void OnEnable()
    {
        StartCoroutine(MainNavigationManager.WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(Scene.Main));
        MainManager.SetScene(Scene.Main);

        for (var i = 0; i < sceneContainers.Length; i++)
        {
            MainNavigationManager.EnableSceneContainer(sceneContainers[i]);
        }
    }
}
