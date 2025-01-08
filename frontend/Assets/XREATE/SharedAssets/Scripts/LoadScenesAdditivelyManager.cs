using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesAdditivelyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadAllScenesAdditively();
    }

    private void LoadAllScenesAdditively()
    {
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("RoomModuleBScene", LoadSceneMode.Additive);
    }

}
