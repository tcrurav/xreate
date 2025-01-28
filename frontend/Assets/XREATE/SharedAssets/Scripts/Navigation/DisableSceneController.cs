using UnityEngine;

public class DisableSceneController : MonoBehaviour
{
    public string SceneContainer;
    void OnEnable()
    {
        MainNavigationManager.DisableSceneContainer(SceneContainer);
    }
}
