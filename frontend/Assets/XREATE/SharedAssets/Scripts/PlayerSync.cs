using Unity.Netcode;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

public class PlayerSync : NetworkBehaviour
{
    //// NetworkList to store Player IDs
    //public NetworkList<int> PlayerIds = new();

    //// Server-side only: Dictionary to map PlayerId to Scene
    //private readonly Dictionary<int, string> PlayerScenes = new();

    [SerializeField] private TMP_Text XreateFrontPlayerName;
    [SerializeField] private TMP_Text XreateBackPlayerName;

    public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<FixedString128Bytes> PlayerName = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> PlayerTeamId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        //base.OnNetworkSpawn();

        PlayerName.OnValueChanged += UpdatePlayerName; // Sync across all clients
        UpdatePlayerName(string.Empty, PlayerName.Value);

        DebugManager.Log("OnNetworkSpawn. New Network Spawn:");
        Debug.Log("OnNetworkSpawn. New Network Spawn:");

        // Set the player ID when the object is spawned on the network
        if (IsOwner)
        {
            Debug.Log($"OnNetworkSpawn. IsOwner");

            PlayerId.Value = MainManager.GetUser().id;
            PlayerName.Value = MainManager.GetUser().username;

            DebugManager.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");
            DebugManager.Log($"OnNetworkSpawn. PlayerName.Value: {PlayerName.Value}");
            Debug.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");
            Debug.Log($"OnNetworkSpawn. PlayerName.Value: {PlayerName.Value}");

            if (XreateBackPlayerName == null)
            {
                DebugManager.Log("El valor del TMP es nulo");
            }

            XreateFrontPlayerName.text = MainManager.GetUser().username;
            XreateBackPlayerName.text = MainManager.GetUser().username;
        }

        if (IsServer) // Only the server controls the visitiblity
        {
            SetVisibilityToNobodySeesNobody();
        }

    }

    private void SetVisibilityToEveryoneSeesToEveryone()
    {

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            GetComponent<NetworkObject>().SpawnWithOwnership(client.ClientId);
        }
    }

    private void SetVisibilityToNobodySeesNobody()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            GetComponent<NetworkObject>().Despawn(false);
        }
    }

    public void ShowPlayerForClient(ulong clientId, bool isVisible)
    {
        if (isVisible)
        {
            GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        }
        else
        {
            GetComponent<NetworkObject>().Despawn(false); // Keeps the object in the server but hides it in the client
        }
    }

    private void UpdatePlayerName(FixedString128Bytes oldValue, FixedString128Bytes newValue)
    {
        if (XreateFrontPlayerName != null)
        {
            XreateFrontPlayerName.text = newValue.ToString();
        }

        if (XreateBackPlayerName != null)
        {
            XreateBackPlayerName.text = newValue.ToString();
        }
    }

    [ServerRpc]
    public void SetPlayerTeamIdServerRpc(ulong clientId, int teamId)
    {
        Debug.Log($"SetPlayerTeamIdServerRpc 1 {teamId} - {clientId}");
        DebugManager.Log($"SetPlayerTeamIdServerRpc 1 {teamId} - {clientId}");

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            Debug.Log($"SetNameToPlayerServerRpc 2 - {client.Value.ClientId}");
            DebugManager.Log($"SetNameToPlayerServerRpc 2 - {client.Value.ClientId}");
            var player = client.Value.PlayerObject;

            if(client.Value.ClientId == clientId)
            {
                player.GetComponent<PlayerSync>().PlayerTeamId.Value = teamId;
            }

        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestVisibilityUpdateServerRpc(string typeOfRoom) // EVERYONE_SEE_EVERYONE, NOBODY_SEE_NOBODY, ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER
    {
        UpdateVisibility(typeOfRoom);
    }

    void UpdateVisibility(string typeOfRoom)
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            PlayerSync player = client.PlayerObject.GetComponent<PlayerSync>();

            switch (typeOfRoom)
            {
                case "NOBODY_SEE_NOBODY":
                    foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                    {
                        player.ShowPlayerForClient(targetClient.ClientId, false);
                    }
                    break;
                case "EVERYONE_SEE_EVERYONE":
                    foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                    {
                        player.ShowPlayerForClient(targetClient.ClientId, true);
                    }
                    break;
                case "ONLY_MEMBERS_OF_SAME_TEAM_SEE_EACH_OTHER":
                    foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                    {
                        PlayerSync targetPlayer = targetClient.PlayerObject.GetComponent<PlayerSync>();
                        bool shouldSee = player.PlayerTeamId.Value == targetPlayer.PlayerTeamId.Value;
                        player.ShowPlayerForClient(targetClient.ClientId, shouldSee);
                    }
                    break;
            }
        }
    }

    //[ServerRpc]
    //public void SetNameToPlayerServerRpc(string name)
    //{
    //    //Debug.Log($"SetNameToPlayerServerRpc");
    //    //CharacterController controller = GetComponent<CharacterController>();
    //    //controller.enabled = false;
    //    //transform.position = new Vector3(20, 20, 20);
    //    //controller.enabled = true;

    //    Debug.Log("SetNameToPlayerServerRpc 1");
    //    DebugManager.Log($"SetNameToPlayerServerRpc 1 {name}");

    //    foreach (var client in NetworkManager.Singleton.ConnectedClients)
    //    {
    //        Debug.Log("SetNameToPlayerServerRpc 2");
    //        DebugManager.Log("SetNameToPlayerServerRpc 2");
    //        var player = client.Value.PlayerObject;



    //        GameObject textObject = FindObject.FindInsideNetworkObjectParentByName(player, "XreatePlayerName");
    //        textObject.GetComponent<TMP_Text>().SetText(name);
    //    }
    //}

    // Update a player's scene (Server only)
    //[ServerRpc(RequireOwnership = false)]
    //public void UpdatePlayerSceneServerRpc(int playerId, string scene)
    //{
    //    Debug.Log($"UpdatePlayerSceneServerRpc called. PlayerId: {playerId}, Scene: {scene}");

    //    if (!IsServer)
    //    {
    //        Debug.LogError("UpdatePlayerSceneServerRpc called on a non-server instance.");
    //        return;
    //    }

    //    // Check if the player already exists in the dictionary
    //    if (!PlayerScenes.ContainsKey(playerId))
    //    {
    //        Debug.Log($"UpdatePlayerSceneServerRpc. Adding new playerId {playerId} with scene {scene}.");
    //        PlayerScenes[playerId] = scene;
    //        PlayerIds.Add(playerId); // Add playerId to the NetworkList
    //    }
    //    else
    //    {
    //        Debug.Log($"UpdatePlayerSceneServerRpc. Updating existing playerId {playerId} to scene {scene}.");
    //        PlayerScenes[playerId] = scene;
    //    }

    //    // Notify clients of the scene update
    //    UpdatePlayerSceneClientRpc(playerId, scene);
    //}

    // Callback for NetworkList changes
    //private void OnPlayerIdsChanged(NetworkListEvent<int> changeEvent)
    //{
    //    switch (changeEvent.Type)
    //    {
    //        case NetworkListEvent<int>.EventType.Add:
    //            Debug.Log($"OnPlayerIdsChanged. Player {changeEvent.Value} added.");
    //            break;

    //        case NetworkListEvent<int>.EventType.Remove:
    //            Debug.Log($"OnPlayerIdsChanged. Player {changeEvent.Value} removed.");
    //            break;

    //        default:
    //            Debug.Log("OnPlayerIdsChanged. Unhandled NetworkList event.");
    //            break;
    //    }
    //}

    // ClientRpc to synchronize scene changes
    //[ClientRpc]
    //public void UpdatePlayerSceneClientRpc(int playerId, string scene)
    //{
    //    Debug.Log("UpdatePlayerSceneClientRpc - FUERA");
    //    if (!IsServer)
    //    {
    //        Debug.Log($"UpdatePlayerSceneClientRpc. Client: Player {playerId}'s scene updated to {scene}");
    //        DebugManager.Log($"UpdatePlayerSceneClientRpc. Client: Player {playerId}'s scene updated to {scene}");
    //        MainNetworkManager.SetVisibilityOnSceneChange(playerId, "", scene);
    //    }
    //}



    //public void UpdatePlayerScene(int playerId, string scene)
    //{
    //    Debug.Log("UpdatePlayerScene");
    //    UpdatePlayerSceneServerRpc(playerId, scene);
    //}

    //public void SetVisibility(bool isVisible)
    //{
    //    if (IsClient) // changes are made in clients only.
    //    {
    //        Debug.Log($"SetVisibility - Fuera");
    //        foreach (var renderer in GetComponentsInChildren<Renderer>())
    //        {
    //            Debug.Log($"SetVisibility - Dentro");
    //            renderer.enabled = isVisible;
    //        }
    //    }
    //}
}
