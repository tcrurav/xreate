//using System.Collections.Generic;
//using Unity.Netcode;
//using UnityEngine;

//public class PlayerSync : NetworkBehaviour
//{
//public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
//public Dictionary<int, NetworkVariable<string>> PlayerScenes = new Dictionary<int, NetworkVariable<string>>();

//public override void OnNetworkSpawn()
//{
//    base.OnNetworkSpawn();

//    DebugManager.Log($"New Network Spawn:");

//    // Set the player ID when the object is spawned on the network
//    if (IsOwner)
//    {
//        PlayerId.Value = MainManager.GetUser().id;
//        DebugManager.Log($"PlayerId.Value: {PlayerId.Value}");
//    }

//    if (IsServer)
//    {
//        UpdatePlayerSceneServerRpc(PlayerId.Value, "LoginScene");
//    }
//}

//// Update a player's scene (Server only)
//[ServerRpc(RequireOwnership = false)]
//public void UpdatePlayerSceneServerRpc(int playerId, string scene)
//{
//    Debug.Log($"UpdatePlayerSceneServerRpc, playerId: {playerId}, scene: {scene}");
//    if (IsServer)
//    {
//        if (!PlayerScenes.ContainsKey(playerId))
//        {
//            Debug.Log($"UpdatePlayerSceneServerRpc 2, scene: {scene}");
//            // The playerId is new
//            //PlayerScenes[playerId] = new NetworkVariable<string>(scene, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

//            NetworkVariable<string> aa = new NetworkVariable<string>(scene,
//                NetworkVariableReadPermission.Everyone,
//                NetworkVariableWritePermission.Server
//            );

//            PlayerScenes[playerId] = aa;

//            Debug.Log($"UpdatePlayerSceneServerRpc 2 y medio, scene: {scene},  PlayerScenes[playerId]: {PlayerScenes[playerId].Value}");

//            //PlayerScenes[playerId].OnValueChanged += (oldValue, newValue) =>
//            //{
//            //    Debug.Log($"Server: Player {playerId}'s scene changed from {oldValue} to {newValue}");
//            //    foreach (var client in NetworkManager.Singleton.ConnectedClients)
//            //    {
//            //        Debug.Log("UpdatePlayerSceneServerRpc 3");
//            //        var player = client.Value.PlayerObject.GetComponent<PlayerSync>();

//            //        UpdatePlayerSceneClientRpc(playerId, oldValue, newValue, new ClientRpcParams
//            //        {
//            //            Send = new ClientRpcSendParams
//            //            {
//            //                TargetClientIds = new List<ulong> { client.Key }
//            //            }
//            //        });

//            //    }
//            //};
//            return;
//        }

//        if (PlayerScenes.TryGetValue(playerId, out var networkVariable))
//        {
//            networkVariable.Value = scene;
//        }
//        else
//        {
//            Debug.LogError($"Player ID {playerId} not found in PlayerScenes.");
//        }
//        Debug.Log($"UpdatePlayerSceneServerRpc 4, scene: {scene},  PlayerScenes[playerId]: {PlayerScenes[playerId].Value}, IsSpawned: {IsSpawned}, IsServer: {IsServer}");
//        // the playerId already existed
//        //PlayerScenes[playerId].Value = scene;
//        Debug.Log($"UpdatePlayerSceneServerRpc 5, scene: {scene},  PlayerScenes[playerId]: {PlayerScenes[playerId].Value}, IsSpawned: {IsSpawned}");
//    }
//}

//[ClientRpc]
//private void UpdatePlayerSceneClientRpc(int playerId, string oldScene, string newScene, ClientRpcParams rpcParams = default)
//{
//    Debug.Log("UpdatePlayerSceneClientRpc");
//    if (IsClient)
//    {
//        Debug.Log("UpdatePlayerSceneClientRpc dentro");
//        MainNetworkManager.SetVisibilityOnSceneChange(playerId, oldScene, newScene);
//    }

//}

using Unity.Netcode;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Services.Lobbies.Models;
using XRMultiplayer;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;
using Unity.Services.Matchmaker.Models;

public class PlayerSync : NetworkBehaviour
{
    // NetworkList to store Player IDs
    public NetworkList<int> PlayerIds = new();

    // Server-side only: Dictionary to map PlayerId to Scene
    private readonly Dictionary<int, string> PlayerScenes = new();

    public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        DebugManager.Log("OnNetworkSpawn. New Network Spawn:");
        Debug.Log("OnNetworkSpawn. New Network Spawn:");

        // Set the player ID when the object is spawned on the network
        if (IsOwner)
        {
            Debug.Log($"OnNetworkSpawn. IsOwner");
            PlayerId.Value = MainManager.GetUser().id;
            DebugManager.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");
            Debug.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");

            MovePlayerServerRpc(new Vector3(20, 20, 20)); // Set target position here

        }

        //if (IsServer)
        //{
        //    Debug.Log($"OnNetworkSpawn. IsServer on Spawining");
        //    PlayerIds.OnListChanged += OnPlayerIdsChanged; // Subscribe to list changes
        //    Debug.Log($"OnNetworkSpawn. IsServer on Spawining 1");
        //    UpdatePlayerSceneServerRpc(PlayerId.Value, "LoginScene");
        //    Debug.Log($"OnNetworkSpawn. IsServer on Spawining 2");
        //}
    }

    [ServerRpc]
    void MovePlayerServerRpc(Vector3 newPosition)
    {
        Debug.Log($"MovePlayerServerRpc");
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = false;
        transform.position = new Vector3(20,20,20);
        controller.enabled = true;
    }

    // Update a player's scene (Server only)
    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerSceneServerRpc(int playerId, string scene)
    {
        Debug.Log($"UpdatePlayerSceneServerRpc called. PlayerId: {playerId}, Scene: {scene}");

        if (!IsServer)
        {
            Debug.LogError("UpdatePlayerSceneServerRpc called on a non-server instance.");
            return;
        }

        // Check if the player already exists in the dictionary
        if (!PlayerScenes.ContainsKey(playerId))
        {
            Debug.Log($"UpdatePlayerSceneServerRpc. Adding new playerId {playerId} with scene {scene}.");
            PlayerScenes[playerId] = scene;
            PlayerIds.Add(playerId); // Add playerId to the NetworkList
        }
        else
        {
            Debug.Log($"UpdatePlayerSceneServerRpc. Updating existing playerId {playerId} to scene {scene}.");
            PlayerScenes[playerId] = scene;
        }

        // Notify clients of the scene update
        UpdatePlayerSceneClientRpc(playerId, scene);
    }

    // Callback for NetworkList changes
    private void OnPlayerIdsChanged(NetworkListEvent<int> changeEvent)
    {
        switch (changeEvent.Type)
        {
            case NetworkListEvent<int>.EventType.Add:
                Debug.Log($"OnPlayerIdsChanged. Player {changeEvent.Value} added.");
                break;

            case NetworkListEvent<int>.EventType.Remove:
                Debug.Log($"OnPlayerIdsChanged. Player {changeEvent.Value} removed.");
                break;

            default:
                Debug.Log("OnPlayerIdsChanged. Unhandled NetworkList event.");
                break;
        }
    }

    // ClientRpc to synchronize scene changes
    [ClientRpc]
    public void UpdatePlayerSceneClientRpc(int playerId, string scene)
    {
        Debug.Log("UpdatePlayerSceneClientRpc - FUERA");
        if (!IsServer)
        {
            Debug.Log($"UpdatePlayerSceneClientRpc. Client: Player {playerId}'s scene updated to {scene}");
            DebugManager.Log($"UpdatePlayerSceneClientRpc. Client: Player {playerId}'s scene updated to {scene}");
            MainNetworkManager.SetVisibilityOnSceneChange(playerId, "", scene);
        }
    }



    //public void UpdatePlayerScene(int playerId, string scene)
    //{
    //    Debug.Log("UpdatePlayerScene");
    //    UpdatePlayerSceneServerRpc(playerId, scene);
    //}

    public void SetVisibility(bool isVisible)
    {
        if (IsClient) // changes are made in clients only.
        {
            Debug.Log($"SetVisibility - Fuera");
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                Debug.Log($"SetVisibility - Dentro");
                renderer.enabled = isVisible;
            }
        }
    }

    [ServerRpc]
    public void ChangePositionServerRpc(Vector3 newPosition)
    {
        Debug.Log($"ChangePositionServerRpc. newPosition: {newPosition}");
        if (IsOwner)
        {
            Debug.Log($"ChangePosition, newPosition: x: {newPosition.x}, y: {newPosition.y}, z: {newPosition.z}, ");
            //transform.position = newPosition;
            //XRINetworkPlayer.LocalPlayer.transform.position = newPosition;
            MovePlayerServerRpc(newPosition); // Set target position here
            Debug.Log($"Después, ChangePosition, transform.position: x: {transform.position.x}, y: {transform.position.y}, z: {transform.position.z}, ");
        }
    }

    //// Use a dictionary-like structure to track player scores by ClientID
    //private Dictionary<ulong, NetworkVariable<int>> XreatePlayerIds = new Dictionary<ulong, NetworkVariable<int>>();
    //private Dictionary<ulong, NetworkVariable<int>> XreatePlayerIds = new Dictionary<ulong, NetworkVariable<int>>();

    //void Start()
    //{
    //    if (IsServer)
    //    {
    //        foreach (var client in NetworkManager.Singleton.ConnectedClients)
    //        {
    //            ulong clientId = client.Key;
    //            PlayerScores[clientId] = new NetworkVariable<int>(0); // Initialize to 0
    //        }
    //    }
    //}

    //// Update a player's score
    //public void UpdatePlayerScore(ulong clientId, int newScore)
    //{
    //    if (IsServer && PlayerScores.ContainsKey(clientId))
    //    {
    //        PlayerScores[clientId].Value = newScore;
    //    }
    //}

    //// Read a player's score
    //public int GetPlayerScore(ulong clientId)
    //{
    //    if (PlayerScores.ContainsKey(clientId))
    //    {
    //        return PlayerScores[clientId].Value;
    //    }
    //    return 0;
    //}

    //public NetworkVariable<int> XreatePlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    //public NetworkVariable<string> XreatePlayerScene = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    ////public NetworkVariable<string> RoomGroupMode = new("HIDE_ALL_STUDENTS", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    ///*
    // * HIDE_ALL_STUDENTS, When student is out of the spaceship, i.e in LoginScene or MenuScene
    // * SHOW_ALL_STUDENTS, When student is in MainScene
    // * HIDE_ALL_STUDENTS_OF_OTHER_TEAMS, When student is in the spaceship in all rooms except the MainScene.
    // */

    //public override void OnNetworkSpawn()
    //{
    //    base.OnNetworkSpawn();

    //    DebugManager.Log($"New Network Spawn:");

    //    // Set the player ID when the object is spawned on the network
    //    if (IsOwner)
    //    {
    //        XreatePlayerId.Value = MainManager.GetUser().id;
    //        XreatePlayerScene.Value = MainManager.GetScene();
    //        DebugManager.Log($"New Network Spawn. XreatePlayerId.Value: {XreatePlayerId.Value}");
    //        DebugManager.Log($"New Network Spawn. XreatePlayerScene.Value: {XreatePlayerScene.Value}");

    //        UpdatePlayerIdServerRpc(MainManager.GetUser().id, NetworkManager.LocalClientId);
    //    }



    //    XreatePlayerId.OnValueChanged += (oldValue, newValue) =>
    //    {
    //        DebugManager.Log($"XreatePlayer ID changed: {newValue}");
    //    };

    //    XreatePlayerScene.OnValueChanged += (oldValue, newValue) =>
    //    {
    //        DebugManager.Log($"XreatePlayer Scene changed: {newValue}");
    //    };
    //}



    //public void SetVisibility(bool isVisible)
    //{
    //    if (IsClient) // changes are made in clients only.
    //    {
    //        foreach (var renderer in GetComponentsInChildren<Renderer>())
    //        {
    //            renderer.enabled = isVisible;
    //        }
    //    }
    //}

    //[ServerRpc(RequireOwnership = false)]
    //public void SendTeamMessageServerRpc(string message, ServerRpcParams rpcParams = default)
    //{
    //    foreach (var client in NetworkManager.Singleton.ConnectedClients)
    //    {
    //        var player = client.Value.PlayerObject.GetComponent<PlayerSync>();

    //        SendTeamMessageClientRpc(message, new ClientRpcParams
    //        {
    //            Send = new ClientRpcSendParams
    //            {
    //                TargetClientIds = new List<ulong> { client.Key }
    //            }
    //        });

    //    }
    //}

    //[ClientRpc]
    //private void SendTeamMessageClientRpc(string message, ClientRpcParams rpcParams = default)
    //{
    //    DebugManager.Log("Message Received: " + message);
    //    switch (message)
    //    {
    //        case "HIDE_ALL_STUDENTS":
    //            MainNetworkManager.HideAllStudents();
    //            break;
    //        case "SHOW_ALL_STUDENTS":
    //            MainNetworkManager.ShowAllStudents();
    //            break;
    //        case "HIDE_ALL_STUDENTS_OF_OTHER_TEAMS":
    //            MainNetworkManager.HideAllStudentsOfOtherTeams();
    //            break;
    //    }
    //}


    //public void SetGroupMode(string groupMode)
    //{
    //    RoomGroupMode.Value = groupMode;

    //    foreach (var player in FindObjectsOfType<PlayerIdSync>())
    //    {
    //        player.GetComponent<NetworkObject>()..ForceCheckObservers(); // update visibility
    //    }
    //}

}
