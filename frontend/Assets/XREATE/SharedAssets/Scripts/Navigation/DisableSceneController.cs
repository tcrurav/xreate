using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableSceneController : MonoBehaviour
{
    public string SceneContainer;
    void OnEnable()
    {
        MainNavigationManager.DisableSceneContainer(SceneContainer);
    }
}
