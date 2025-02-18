using Unity.Netcode;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class PlayerSync : NetworkBehaviour
{
    [SerializeField] private TMP_Text XreateFrontPlayerName;
    [SerializeField] private TMP_Text XreateBackPlayerName;

    public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<FixedString128Bytes> PlayerName = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> PlayerTeamId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> PlayerScene = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

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
            PlayerScene.Value = (int)MainManager.GetScene();

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
            SetVisibilityDependingOnScene((Scene)PlayerScene.Value);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerSceneServerRpc(Scene scene)
    {
        PlayerScene.Value = (int)scene;
        SetVisibilityDependingOnScene(scene);
    }

    private void SetVisibilityDependingOnScene(Scene scene)
    {
        switch (scene)
        {
            case Scene.Login:
                UpdateVisibility(RoomVisibility.NobodySeeNobody);
                break;
            case Scene.Menu:
                UpdateVisibility(RoomVisibility.NobodySeeNobody);
                break;
            case Scene.Main:
                UpdateVisibility(RoomVisibility.EveryoneSeeEveryone);
                break;
            case Scene.RoomModuleB:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
            case Scene.RoomModuleA:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
            case Scene.LeisureModule:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
            case Scene.TunnelConnectorC:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
            case Scene.TunnelConnectorD:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
            case Scene.TunnelConnectorF:
                UpdateVisibility(RoomVisibility.OnlyMembersOfSameTeamSeeEachOther);
                break;
        }

    }

    //private void SetVisibilityToEveryoneSeesToEveryone()
    //{

    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        GetComponent<NetworkObject>().SpawnWithOwnership(client.ClientId);
    //    }
    //}

    //private void SetVisibilityToNobodySeesNobody()
    //{
    //    foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
    //    {
    //        GetComponent<NetworkObject>().Despawn(false);
    //    }
    //}

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

            if (client.Value.ClientId == clientId)
            {
                player.GetComponent<PlayerSync>().PlayerTeamId.Value = teamId;
            }

        }
    }

    //[ServerRpc(RequireOwnership = false)]
    //void RequestVisibilityUpdateServerRpc(RoomVisibility typeOfRoom)
    //{
    //    UpdateVisibility(typeOfRoom);
    //}

    void UpdateVisibility(RoomVisibility typeOfRoom)
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject != null) // TODO - This should be investigated
            {

                PlayerSync player = client.PlayerObject.GetComponent<PlayerSync>();

                switch (typeOfRoom)
                {
                    case RoomVisibility.NobodySeeNobody:
                        foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                        {
                            player.ShowPlayerForClient(targetClient.ClientId, false);
                        }
                        break;
                    case RoomVisibility.EveryoneSeeEveryone:
                        foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                        {
                            player.ShowPlayerForClient(targetClient.ClientId, true);
                        }
                        break;
                    case RoomVisibility.OnlyMembersOfSameTeamSeeEachOther:
                        foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                        {
                            PlayerSync targetPlayer = targetClient.PlayerObject.GetComponent<PlayerSync>();
                            bool shouldSee = player.PlayerTeamId.Value == targetPlayer.PlayerTeamId.Value;
                            player.ShowPlayerForClient(targetClient.ClientId, shouldSee);
                        }
                        break;
                }
            } else
            {
                Debug.Log($"UpdateVisibility - No PlayerObject yet for ClientId: {client.ClientId}");
            }
        }
    }

}