using Unity.Netcode;
using UnityEngine;

public class TeamMapManager : NetworkBehaviour
{
    public NetworkList<int> currentTeamScene = new(default);

    private TeamMapController teamMapController;

    private bool isSubscribed = false;

    private void Awake()
    {
        Debug.Log("TeamMapManager - Start");

        teamMapController = GetComponent<TeamMapController>();

        if (teamMapController == null)
        {
            Debug.Log("TeamMapManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"TeamMapManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (IsServer)
        {
            Debug.Log($"TeamMapManager - OnNetworkSpawn");
            // TODO - It shouldn't be hardcoded, but now currentTeamScene[0] is the scene where Team 1 is, and currentTeamScene[1] is the scene where Team 2 is
            currentTeamScene.Add((int)Scene.Main);
            currentTeamScene.Add((int)Scene.Main);

            Debug.Log($"TeamMapManager - OnNetworkSpawn - currentTeamScene[0]: {currentTeamScene[0]} - currentTeamScene[1]: {currentTeamScene[1]}");
        }

        if (!isSubscribed)
        {

            currentTeamScene.OnListChanged += (NetworkListEvent<int> changeEvent) =>
            {
                Debug.Log($"TeamMapManager - currentTeamScene.OnListChanged - changeEvent.Index: {changeEvent.Index} - changeEvent.Value: {changeEvent.Value}");

                // Notify clients only on insert, because "RemoveAt" is just to insert afterwards the new value.
                if (changeEvent.Type == NetworkListEvent<int>.EventType.Insert) ChangeCurrentTeamSceneClientRpc(changeEvent.Index, changeEvent.Value);
            };

            isSubscribed = true;
        }

        // TODO - This can be necessary
        //if (IsClient)
        //{
        //    DebugManager.Log($"SlideShowManager - OnNetworkSpawn - if (IsClient) - IsServer: {IsServer} - IsClient: {IsClient}");
        //    ChangeCurrentSlideClientRpc(currentSlide.Value, currentSlide.Value);
        //    ChangeForceAttentionClientRpc(forceAttention.Value, forceAttention.Value);
        //    ChangeStartReadyClientRpc(startReady.Value, startReady.Value);
        //}
    }

    public override void OnDestroy()
    {
        currentTeamScene?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeCurrentTeamSceneServerRpc(int index, int newValue)
    {
        if (currentTeamScene[index] != newValue)
        {
            Debug.Log($"ChangeCurrentTeamSceneServerRpc - index: {index} - newValue: {newValue}");
            currentTeamScene.RemoveAt(index);
            currentTeamScene.Insert(index, newValue);
        }
    }

    [ClientRpc]
    public void ChangeCurrentTeamSceneClientRpc(int index, int newValue)
    {
        Debug.Log($"ChangeCurrentTeamSceneClientRpc - index: {index}, newValue: {newValue}");
        if (teamMapController != null)
        {
            Debug.Log("ChangeCurrentTeamSceneClientRpc - Llamando a changeTeamScene");
            //teamMapController.OnChangeTeamScene(index, newValue);
            teamMapController.RefreshTeamPositionsInAllMaps();
        }
        else
        {
            Debug.Log("ChangeCurrentTeamSceneClientRpc - teamMapController es null");
        }
    }
}