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
        base.OnNetworkSpawn();

        PlayerName.OnValueChanged += UpdatePlayerName; // Sync across all clients
        UpdatePlayerName(string.Empty, PlayerName.Value);

        //DebugManager.Log("OnNetworkSpawn. New Network Spawn:");
        //Debug.Log("OnNetworkSpawn. New Network Spawn:");

        // Set the player ID when the object is spawned on the network
        if (IsOwner)
        {
            Debug.Log($"OnNetworkSpawn. IsOwner");

            PlayerId.Value = MainManager.GetUser().id;
            PlayerName.Value = MainManager.GetUser().username;

            //DebugManager.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");
            //Debug.Log($"OnNetworkSpawn. PlayerId.Value: {PlayerId.Value}");

            //Debug.Log($"OnNetworkSpawn. PlayerName.Value: {PlayerName.Value}");
            //DebugManager.Log($"OnNetworkSpawn. PlayerName.Value: {PlayerName.Value}");

            //Debug.Log($"OnNetworkSpawn.  PlayerScene.Value: {PlayerScene.Value}");
            //DebugManager.Log($"OnNetworkSpawn.  PlayerScene.Value: {PlayerScene.Value}");


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

    [ServerRpc]
    public void SetPlayerSceneServerRpc(Scene scene)
    {
        //Debug.Log($"SetPlayerSceneServerRpc ANTES - scene: {scene}");
        //DebugManager.Log($"SetPlayerSceneServerRpc ANTES - scene: {scene}");

        if (IsServer)
        {
            //Debug.Log($"SetPlayerSceneServerRpc DESPUES - scene: {scene}");
            //DebugManager.Log($"SetPlayerSceneServerRpc DESPUES - scene: {scene}");

            PlayerScene.Value = (int)scene;
            SetVisibilityDependingOnScene(scene);
        }
    }

    public void SetPlayerScene(Scene scene)
    {
        //Debug.Log($"SetPlayerScene ANTES - {scene}");
        if (IsOwner)
        {
            //Debug.Log($"SetPlayerScene DESPUES - {scene}");
            SetPlayerSceneServerRpc(scene);
        }
    }

    [ServerRpc]
    public void SetPlayerTeamIdServerRpc(int teamId)
    {
        if (IsServer)
        {
            //Debug.Log($"SetPlayerTeamIdServerRpc - teamId: {teamId}");
            //DebugManager.Log($"SetPlayerTeamIdServerRpc - teamId: {teamId}");

            PlayerTeamId.Value = teamId;
        }
    }

    public void SetPlayerTeamId(int teamId)
    {
        if (IsOwner)
        {
            SetPlayerTeamIdServerRpc(teamId);
        }
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

    public void ShowPlayerForClient(PlayerSync player, ulong clientId, bool isVisible)
    {
        Debug.Log($"ShowPlayerForClient clientId: {clientId}, isVisible: {isVisible}");
        DebugManager.Log($"ShowPlayerForClient clientId: {clientId}, isVisible: {isVisible}");

        return;

        // TODO - This should work some day - But now time is knapp
        //if (isVisible)
        //{
        //    if (player.GetComponent<NetworkObject>().IsSpawned) return;

        //    player.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        //}
        //else
        //{
        //    if (!player.GetComponent<NetworkObject>().IsSpawned) return;

        //    player.GetComponent<NetworkObject>().Despawn(false); // Keeps the object in the server but hides it in the client
        //}
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
                            player.ShowPlayerForClient(player, targetClient.ClientId, false);
                        }
                        break;
                    case RoomVisibility.EveryoneSeeEveryone:
                        foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                        {
                            player.ShowPlayerForClient(player, targetClient.ClientId, true);
                        }
                        break;
                    case RoomVisibility.OnlyMembersOfSameTeamSeeEachOther:
                        foreach (var targetClient in NetworkManager.Singleton.ConnectedClientsList)
                        {
                            PlayerSync targetPlayer = targetClient.PlayerObject.GetComponent<PlayerSync>();
                            bool shouldSee = player.PlayerTeamId.Value == targetPlayer.PlayerTeamId.Value;
                            player.ShowPlayerForClient(player, targetClient.ClientId, shouldSee);
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