using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR;

public class MainNetworkManager : MonoBehaviour
{
    public static MainNetworkManager Instance;

    public UnityEngine.UI.Button ConfirmButton;
    public UnityEngine.UI.Button QuickJoinButton;
    public UnityEngine.UI.Button CloseMenuButton;

    private bool disconnected = true;
    private bool headSetError = false;

    private InputDevice headsetDevice;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Instance.ConfirmButton = ConfirmButton;
        Instance.QuickJoinButton = QuickJoinButton;

        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;

        // Get the headset device
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.Head, devices);

        if (devices.Count > 0)
        {
            headsetDevice = devices[0];
            DebugManager.Log("Headset device found: " + headsetDevice.name);
        }
        else
        {
            DebugManager.Log("No headset device found.");
        }
    }

    private void OnDisconnected(ulong clientId)
    {
        DebugManager.Log($"Client {clientId} disconnected.");
        if (NetworkManager.Singleton.IsClient)
        {
            disconnected = true;
            AttemptReconnect();
        }
    }

    private void AttemptReconnect()
    {
        DebugManager.Log("Attempting to reconnect...");

        if (!NetworkManager.Singleton.ShutdownInProgress)
        {
            NetworkManager.Singleton.Shutdown();
        }

        // Restart the connection
        NetworkQuickJoinLoginUsingUnity6TemplateMenus();

        // Optional: Use a retry mechanism for robustness
        StartCoroutine(RetryConnect());
    }

    private IEnumerator RetryConnect(int maxRetries = 5, float retryInterval = 3f)
    {
        int attempts = 0;
        while (attempts < maxRetries && !NetworkManager.Singleton.IsClient)
        {
            DebugManager.Log($"Reconnect attempt {attempts + 1}/{maxRetries}...");
            NetworkQuickJoinLoginUsingUnity6TemplateMenus();
            attempts++;
            yield return new WaitForSeconds(retryInterval);
        }

        if (NetworkManager.Singleton.IsClient)
        {
            disconnected = false;
            DebugManager.Log("Reconnected successfully.");
        }
        else
        {
            disconnected = true;
            DebugManager.Log("Failed to reconnect after maximum attempts.");
        }
    }

    private void Update()
    {
        if (disconnected && MainManager.GetUser() != null)
        {
            if (headsetDevice.isValid)
            {
                // Check for user presence
                if (headsetDevice.TryGetFeatureValue(CommonUsages.userPresence, out bool isWorn))
                {
                    //Debug.Log(isWorn ? "Headset is being worn." : "Headset removed.");
                    if (isWorn)
                    {
                        NetworkQuickJoinLoginUsingUnity6TemplateMenus();
                        disconnected = false;
                    }
                }
                else
                {
                    DebugManager.Log("User presence feature not supported on this device.");
                }
            }
            else
            {
                if (!headSetError)
                {
                    DebugManager.Log("Headset device is not valid.");
                    headSetError = true;
                }
            }
        }
    }

    public static void NetworkQuickJoinLoginUsingUnity6TemplateMenus()
    {
        XRMultiplayer.XRINetworkGameManager.LocalPlayerName.Value = MainManager.GetUser().username;
        Instance.ConfirmButton.onClick.Invoke();
        Instance.QuickJoinButton.onClick.Invoke();
        Instance.CloseMenuButton.onClick.Invoke();
    }

}

