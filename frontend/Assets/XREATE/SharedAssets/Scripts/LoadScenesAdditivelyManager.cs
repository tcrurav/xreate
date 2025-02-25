using System.Collections;
using Unity.Netcode;
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
        StartCoroutine(WaitForNetworkStartAndThenNetworkManagerLoadSceneAsyncAdditively());
    }

    private void networkManagerLoadSceneAsyncAdditively()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("RoomModuleAScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("RoomModuleBScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("LeisureModuleScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorCScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorFScene", LoadSceneMode.Additive);
        NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorDScene", LoadSceneMode.Additive); // This corridor will not be used but has to be visible
        NetworkManager.Singleton.SceneManager.LoadScene("AuditoriumScene", LoadSceneMode.Additive);
    }

    private IEnumerator WaitForNetworkStartAndThenNetworkManagerLoadSceneAsyncAdditively()
    {
        while (NetworkManager.Singleton == null || !NetworkManager.Singleton.IsListening)
        {
            yield return null; // Wait until NetworkManager is initialized
        }

        Debug.Log("NetworkManager is now running!");
        networkManagerLoadSceneAsyncAdditively();
    }

    private void sceneManagerLoadSceneAsyncAdditively()
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
