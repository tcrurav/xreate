using System.Collections;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class LoadScenesAdditivelyManager : NetworkBehaviour
{
    public string[] m_SceneName;

    //public override void OnNetworkSpawn()
    //{
    //    DebugManager.Log("LoadScenesAdditivelyManager - Se ejecutó fuera");
    //    if (IsServer)
    //    {
    //        DebugManager.Log("LoadScenesAdditivelyManager - Se ejecutó dentro");
    //        for (int i = 0; i < m_SceneName.Length; i++)
    //        {
    //            var status = NetworkManager.SceneManager.LoadScene(m_SceneName[i], LoadSceneMode.Additive);
    //            if (status != SceneEventProgressStatus.Started)
    //            {
    //                DebugManager.Log($"LoadScenesAdditivelyManager - Failed to load {m_SceneName[i]} " +
    //                      $"with a {nameof(SceneEventProgressStatus)}: {status}");
    //            }
    //        }

    //    }
    //}
    //}

    //using UnityEngine;
    //using Unity.Netcode;
    //using UnityEngine.SceneManagement;
    //using UnityEngine.XR.Management;

    //public class MultiplayerManager : NetworkBehaviour
    //{
    //    public static MultiplayerManager Instance;
    //    public string[] sceneNames;

    //    private void Awake()
    //    {
    //        if (Instance == null)
    //            Instance = this;
    //        else
    //            Destroy(gameObject);
    //    }

    //    void Start()
    //    {
    //        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
    //        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
    //        {
    //            XRGeneralSettings.Instance.Manager.StartSubsystems();
    //        }
    //    }

    //    public void StartHost()
    //    {
    //        NetworkManager.Singleton.StartHost();
    //        NetworkManager.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    //        LoadAdditiveScenes();
    //    }

    //    public void StartClient()
    //    {
    //        NetworkManager.Singleton.StartClient();
    //        NetworkManager.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    //    }

    //    public void LoadAdditiveScenes()
    //    {
    //        if (IsServer && sceneNames != null)
    //        {
    //            foreach (var sceneName in sceneNames)
    //            {
    //                var status = NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    //                if (status != SceneEventProgressStatus.Started)
    //                {
    //                    Debug.LogWarning($"Failed to load {sceneName} " +
    //                          $"with a {nameof(SceneEventProgressStatus)}: {status}");
    //                }
    //            }
    //        }
    //    }

    //    private void OnSceneLoaded(string sceneName, LoadSceneMode mode, SceneEventProgressStatus status)
    //    {
    //        if (status == SceneEventProgressStatus.Started)
    //        {
    //            Debug.Log($"Scene {sceneName} loaded successfully.");
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Scene {sceneName} failed to load with status: {status}");
    //        }
    //    }

    //    public override void OnDestroy()
    //    {
    //        base.OnDestroy();
    //        if (NetworkManager.Singleton != null)
    //        {
    //            NetworkManager.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
    //        }
    //    }
    //}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    LoadAllScenesAdditively();
    //}

    //private void LoadAllScenesAdditively()
    //{
    //    SceneManagerLoadSceneAsyncAdditively();

    //    //StartCoroutine(WaitForNetworkStartAndThenNetworkManagerLoadSceneAsyncAdditively());
    //}

    //private void NetworkManagerLoadSceneAsyncAdditively()
    //{
    //    NetworkManager.Singleton.SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("RoomModuleAScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("RoomModuleBScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("LeisureModuleScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorCScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorFScene", LoadSceneMode.Additive);
    //    NetworkManager.Singleton.SceneManager.LoadScene("TunnelConnectorDScene", LoadSceneMode.Additive); // This corridor will not be used but has to be visible
    //    NetworkManager.Singleton.SceneManager.LoadScene("AuditoriumScene", LoadSceneMode.Additive);
    //}

    //private IEnumerator WaitForNetworkStartAndThenNetworkManagerLoadSceneAsyncAdditively()
    //{
    //    while (NetworkManager.Singleton == null || !NetworkManager.Singleton.IsListening)
    //    {
    //        yield return null; // Wait until NetworkManager is initialized
    //    }

    //    DebugManager.Log("NetworkManager is now running!");
    //    NetworkManagerLoadSceneAsyncAdditively();
    //}

    //private void SceneManagerLoadSceneAsyncAdditively()
    //{
    //    SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("RoomModuleAScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("RoomModuleBScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("LeisureModuleScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("TunnelConnectorCScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("TunnelConnectorFScene", LoadSceneMode.Additive);
    //    SceneManager.LoadSceneAsync("TunnelConnectorDScene", LoadSceneMode.Additive); // This corridor will not be used but has to be visible
    //    SceneManager.LoadSceneAsync("AuditoriumScene", LoadSceneMode.Additive);
    //}

}

//using UnityEngine;
//using Unity.Netcode;
//using UnityEngine.SceneManagement;
//using UnityEngine.XR.Management;

//public class MultiplayerManager : NetworkBehaviour
//{
//    public static MultiplayerManager Instance;
//    public string[] sceneNames;

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    void Start()
//    {
//        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
//        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
//        {
//            XRGeneralSettings.Instance.Manager.StartSubsystems();
//        }

//        // Si no hay un host, el primer headset que se conecte será el host
//        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
//        {
//            StartHost();
//        }
//    }

//    public void StartHost()
//    {
//        NetworkManager.Singleton.StartHost();
//        NetworkManager.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
//        LoadAdditiveScenes();
//    }

//    public void StartClient()
//    {
//        NetworkManager.Singleton.StartClient();
//        NetworkManager.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
//    }

//    public void LoadAdditiveScenes()
//    {
//        if (IsServer && sceneNames != null)
//        {
//            foreach (var sceneName in sceneNames)
//            {
//                var status = NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
//                if (status != SceneEventProgressStatus.Started)
//                {
//                    Debug.LogWarning($"Failed to load {sceneName} " +
//                          $"with a {nameof(SceneEventProgressStatus)}: {status}");
//                }
//            }
//        }
//    }

//    private void OnSceneLoaded(string sceneName, LoadSceneMode mode, SceneEventProgressStatus status)
//    {
//        if (status == SceneEventProgressStatus.Started)
//        {
//            Debug.Log($"Scene {sceneName} loaded successfully.");
//        }
//        else
//        {
//            Debug.LogWarning($"Scene {sceneName} failed to load with status: {status}");
//        }
//    }

//    public override void OnDestroy()
//    {
//        base.OnDestroy();
//        if (NetworkManager.Singleton != null)
//        {
//            NetworkManager.SceneManager.OnLoadEventCompleted -= OnSceneLoaded;
//        }
//    }
//}
