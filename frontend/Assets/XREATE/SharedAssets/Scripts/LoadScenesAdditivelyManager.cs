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
        SceneManager.LoadSceneAsync("RoomModuleAScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("RoomModuleBScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("LeisureModuleScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("TunnelConnectorCScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("TunnelConnectorFScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("TunnelConnectorDScene", LoadSceneMode.Additive); // This corridor will not be used but has to be visible
        SceneManager.LoadSceneAsync("AuditoriumScene", LoadSceneMode.Additive);
    }

}
