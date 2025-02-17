using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UIElements;
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

    public static void GetAllPlayers()
    {
        DebugManager.Log("GetAllPlayers");
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            // Access each player's client and identity information
            var playerObject = client.PlayerObject;
            // You can then check the playerObject or get components from it

            DebugManager.Log(playerObject.name);
        }
    }

    public static void NetworkQuickJoinLoginUsingUnity6TemplateMenus()
    {
        XRMultiplayer.XRINetworkGameManager.LocalPlayerName.Value = MainManager.GetUser().username;
        Instance.ConfirmButton.onClick.Invoke();
        Instance.QuickJoinButton.onClick.Invoke();
        Instance.CloseMenuButton.onClick.Invoke();
    }

    //public static void ChangeSceneTo(int playerId, string sceneName)
    //{
    //    MainManager.SetScene(sceneName);
    //    DebugManager.Log($"ChangeSceneTo, playerId: {playerId}, sceneName: {sceneName}");
    //    Debug.Log($"ChangeSceneTo. playerId: {playerId}, sceneName: {sceneName}");

    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        Debug.Log($"ChangeSceneTo. playerId: {playerId}");
    //        Debug.Log($"ChangeSceneTo. client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value: {client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value}");
    //        DebugManager.Log($"ChangeSceneTo. client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value: {client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value}");
    //        if (client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value == playerId)
    //        {
    //            Debug.Log($"ChangeSceneTo. Going to serverRpc: playerId: {playerId}, sceneName: {sceneName}");
    //            //client.PlayerObject.GetComponent<PlayerSync>().UpdatePlayerScene(playerId, sceneName);
    //            client.PlayerObject.GetComponent<PlayerSync>().UpdatePlayerSceneServerRpc(playerId, sceneName);
    //            return;
    //        }
    //    }
    //}

    //public static void SetVisibilityOnSceneChange(int playerId, string oldScene, string newScene)
    //{
    //    Debug.Log("SetVisibilityOnSceneChange");
    //    Debug.Log($"playerId: {playerId}, oldScene: {oldScene}, newScene: {newScene}");
    //    DebugManager.Log($"playerId: {playerId}, oldScene: {oldScene}, newScene: {newScene}");
    //    if (MainManager.GetScene() == "LoginScene" ||  MainManager.GetScene() == "MenuScene")
    //    {
    //        SetVisibility(playerId, false);
    //        return;
    //    }

    //    if(newScene != MainManager.GetScene())
    //    {
    //        SetVisibility(playerId, false);
    //        return;
    //    }

    //    if (newScene == "MainScene")
    //    {
    //        SetVisibility(playerId, true);
    //        return;
    //    }

    //    int thisUserTeamId = CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id);
    //    int playerWhoChangeSceneTeamId = CurrentActivityManager.GetTeamIdByStudentId(playerId);

    //    if(thisUserTeamId != playerWhoChangeSceneTeamId)
    //    {
    //        SetVisibility(playerId, false);
    //        return;
    //    }

    //    SetVisibility(playerId, true);
    //    return;
    //}

    //private static void SetVisibility(int playerId, bool visibility)
    //{
    //    Debug.Log("SetVisibility - MainNetworkManager 1");
    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        Debug.Log("SetVisibility - MainNetworkManager 2");
    //        if (client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value == playerId)
    //        {
    //            Debug.Log("SetVisibility - MainNetworkManager 3");
    //            client.PlayerObject.GetComponent<PlayerSync>().SetVisibility(visibility);
    //            return;
    //        }
    //    }
    //}

    //public static void ChangePlayerPosition(int playerId, Vector3 newPosition)
    //{
    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        Debug.Log($"ChangePlayerPosition - client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value: {client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value}");
    //        Debug.Log($"ChangePlayerPosition - playerId: {playerId}");
    //        if (client.PlayerObject.GetComponent<PlayerSync>().PlayerId.Value == playerId)
    //        {
    //            Debug.Log($"ChangePlayerPosition - MainNetworkManager 3 - PlayerId: {playerId}, newPosition: {newPosition}");
    //            client.PlayerObject.GetComponent<PlayerSync>().ChangePositionServerRpc(newPosition);
    //            return;
    //        }
    //    }
    //}

    //public static void HideAllStudentsOfOtherTeams()
    //{
    //    DebugManager.Log("HideAllStudentsOfOtherTeams");
    //    if (MainManager.GetUser().role != "STUDENT")
    //    {
    //        DebugManager.Log("Only students are in a team. All other roles see all players");
    //        return;
    //    }

    //    int thisUserTeamId = CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id);
    //    DebugManager.Log("thisUserTeamId:" + thisUserTeamId.ToString());

    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        // playerObject will have the NetworkObject of the client
    //        NetworkObject playerObject = client.PlayerObject;
    //        int studentId = playerObject.GetComponent<PlayerSync>().XreatePlayerId.Value;
    //        DebugManager.Log("studentId:" + studentId.ToString());
    //        int clientTeamId = CurrentActivityManager.GetTeamIdByStudentId(studentId);
    //        DebugManager.Log("clientTeamId:" + clientTeamId.ToString());

    //        if (clientTeamId != thisUserTeamId)
    //        {
    //            //client.PlayerObject.GetComponent<Renderer>().enabled = false;
    //            client.PlayerObject.GetComponent<PlayerSync>().SetVisibility(false);
    //            DebugManager.Log($"Se deshabilitó el cliente {studentId} del equipo {clientTeamId}");
    //        }
    //    }
    //}

    //public static void ShowAllStudents()
    //{
    //    DebugManager.Log("ShowAllStudents");
    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        DebugManager.Log($"Antes de Enabling client {client.PlayerObject.GetComponent<PlayerSync>().XreatePlayerId.Value}");
    //        //client.PlayerObject.GetComponent<Renderer>().enabled = true;
    //        //if(client.PlayerObject.GetComponent<PlayerIdSync>().PlayerId.Value != MainManager.GetUser().id) client.PlayerObject.gameObject.SetActive(true);
    //        client.PlayerObject.GetComponent<PlayerSync>().SetVisibility(true);
    //        DebugManager.Log($"Después de Enabling client {client.PlayerObject.GetComponent<PlayerSync>().XreatePlayerId.Value}");
    //    }
    //}

    //public static void HideAllStudents()
    //{
    //    DebugManager.Log("HideAllStudentsExceptLocalPlayer");
    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        DebugManager.Log($"Antes de Enabling client {client.PlayerObject.GetComponent<PlayerSync>().XreatePlayerId.Value}");
    //        //client.PlayerObject.GetComponent<Renderer>().enabled = true;
    //        //if(client.PlayerObject.GetComponent<PlayerIdSync>().PlayerId.Value != MainManager.GetUser().id) client.PlayerObject.gameObject.SetActive(true);
    //        client.PlayerObject.GetComponent<PlayerSync>().SetVisibility(false);
    //        DebugManager.Log($"Después de Enabling client {client.PlayerObject.GetComponent<PlayerSync>().XreatePlayerId.Value}");
    //    }
    //}

}

