using Unity.Netcode;
using UnityEngine;

public class CorridorToLeisureModuleManager : NetworkBehaviour
{
    public NetworkVariable<bool> startReadyToNextRoom = new(false);

    private StartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        startButtonController = GetComponent<StartButtonController>();

        if (startButtonController == null)
        {
            Debug.Log("CorridorToLeisureModuleManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!isSubscribed)
        {
            startReadyToNextRoom.OnValueChanged += (oldValue, newValue) =>
            {
                ChangeStartReadyToNextRoomClientRpc(oldValue, newValue);
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
        startReadyToNextRoom?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToNextRoomServerRpc(bool newValue)
    {
        if (startButtonController != null && startReadyToNextRoom.Value != newValue)
        {
            startReadyToNextRoom.Value = newValue;
        }
        else
        {
            Debug.Log("CorridorToLeisureModuleManager - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        if (startButtonController != null)
        {
            GetComponent<StartButtonController>().EnableNextRooms();
        }
        else
        {
            Debug.Log("CorridorToLeisureModuleManager - startButtonController es null");
        }
    }


}