using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableLoginSceneController : MonoBehaviour
{
    void OnEnable()
    {
        GameObject loginSceneContainer = GameObject.FindGameObjectWithTag("LoginSceneContainer");
        loginSceneContainer.SetActive(false);

        // Don't unload because in LoginScene is XR and networking
        //SceneManager.UnloadScene("LoginScene");

        GameObject roomModuleBSceneContainerParent = GameObject.FindGameObjectWithTag("RoomModuleBSceneContainerParent");
        GameObject roomModuleBSceneContainer = MainManager.FindObject(roomModuleBSceneContainerParent, "RoomModuleBSceneContainer");
        roomModuleBSceneContainer.SetActive(true);
    }
}
