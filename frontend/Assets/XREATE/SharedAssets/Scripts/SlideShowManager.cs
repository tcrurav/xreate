using Unity.Netcode;
using UnityEngine;

public class SlideShowManager : NetworkBehaviour
{
    public NetworkVariable<int> currentSlide = new(0);
    public NetworkVariable<bool> forceAttention = new(false);
    public NetworkVariable<bool> startReady = new(false);

    private void Start()
    {
        if (NetworkManager.Singleton.IsServer) // Ensure only the server spawns it
        {
            // TODO - Remove commented lines below
            //GameObject obj = Instantiate(slideShowPrefab);
            //obj.GetComponent<NetworkObject>().Spawn();
            GetComponent<NetworkObject>().Spawn();
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"SlideShowManager - OnNetworkSpawn - IsServer: {IsServer}");

        currentSlide.OnValueChanged += (oldValue, newValue) =>
        {
            ChangeCurrentSlideClientRpc(oldValue, newValue);
        };

        forceAttention.OnValueChanged += (oldValue, newValue) =>
        {
            ChangeForceAttentionClientRpc(oldValue, newValue);
        };

        startReady.OnValueChanged += (oldValue, newValue) =>
        {
            ChangeStartReadyClientRpc(oldValue, newValue);
        };
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeCurrentSlideServerRpc(int newCurrentSlide)
    {
        currentSlide.Value = newCurrentSlide;
    }

    [ClientRpc]
    public void ChangeCurrentSlideClientRpc(int oldValue, int newValue)
    {
        GetComponent<SlideController>().HideOldSlideAndShowNewSlide(oldValue, newValue);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeForceAttentionServerRpc(bool newForceAttention)
    {
        //Debug.Log($"ChangeForceAttentionServerRpc - newValue: {newForceAttention}");
        forceAttention.Value = newForceAttention;
    }

    [ClientRpc]
    public void ChangeForceAttentionClientRpc(bool oldValue, bool newValue)
    {
        //Debug.Log($"ChangeForceAttentionClientRpc - newValue: {newValue}");
        GetComponent<ForceAttentionController>().SetToggleValue(newValue);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyServerRpc(bool newStartReady)
    {
        //Debug.Log($"ChangeStartReadyServerRpc - newValue: {newStartReady}");
        startReady.Value = newStartReady;
    }

    [ClientRpc]
    public void ChangeStartReadyClientRpc(bool oldValue, bool newValue)
    {
        //Debug.Log($"ChangeStartReadyClientRpc - newValue: {newValue}");
        GetComponent<SlideShowStartButtonController>().EnableNextRooms();
    }
}