using Unity.Netcode;
using UnityEngine;

public class SlideShowManager : NetworkBehaviour
{
    public NetworkVariable<int> currentSlide = new(0);
    public NetworkVariable<bool> forceAttention = new(false);
    public NetworkVariable<bool> startReady = new(false);

    private SlideController slideController;
    private ForceAttentionController forceAttentionController;
    private SlideShowStartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        slideController = GetComponent<SlideController>();
        forceAttentionController = GetComponent<ForceAttentionController>();
        startButtonController = GetComponent<SlideShowStartButtonController>();

        if (slideController == null || forceAttentionController == null || startButtonController == null)
        {
            DebugManager.Log("SlideShowManager - Start - Faltan componentes necesarios en el GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        DebugManager.Log($"SlideShowManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (!isSubscribed)
        {

            currentSlide.OnValueChanged += (oldValue, newValue) =>
            {
                DebugManager.Log($"SlideShowManager - currentSlide.OnValueChanged - IsServer: {IsServer} - IsClient: {IsClient}");
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

    [ServerRpc(RequireOwnership = false)]
    public void ChangeCurrentSlideServerRpc(int newCurrentSlide)
    {
        if (currentSlide.Value != newCurrentSlide)
        {
            DebugManager.Log($"ChangeCurrentSlideServerRpc - newCurrentSlide: {newCurrentSlide}");
            currentSlide.Value = newCurrentSlide;
        }
    }

    [ClientRpc]
    public void ChangeCurrentSlideClientRpc(int oldValue, int newValue)
    {
        DebugManager.Log($"ChangeCurrentSlideClientRpc - oldValue: {oldValue}, newValue: {newValue}");
        if (slideController != null)
        {
            DebugManager.Log("ChangeCurrentSlideClientRpc - Llamando a HideOldSlideAndShowNewSlide");
            slideController.HideOldSlideAndShowNewSlide(oldValue, newValue);
        }
        else
        {
            DebugManager.Log("ChangeCurrentSlideClientRpc - SlideController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeForceAttentionServerRpc(bool newForceAttention)
    {
        if (forceAttentionController != null)
        {
            forceAttention.Value = newForceAttention;
        }
        else
        {
            DebugManager.Log("ChangeForceAttentionServerRpc - forceAttentionController es null");

        }
    }

    [ClientRpc]
    public void ChangeForceAttentionClientRpc(bool oldValue, bool newValue)
    {
        if (forceAttentionController != null)
        {
            GetComponent<ForceAttentionController>().SetToggleValue(newValue);
        }
        else
        {
            DebugManager.Log("ChangeForceAttentionClientRpc - forceAttentionController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyServerRpc(bool newStartReady)
    {
        if (startButtonController != null)
        {
            startReady.Value = newStartReady;
        }
        else
        {
            DebugManager.Log("ChangeStartReadyServerRpc - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyClientRpc(bool oldValue, bool newValue)
    {
        if (startButtonController != null)
        {
            GetComponent<SlideShowStartButtonController>().EnableNextRooms();
        }
        else
        {
            DebugManager.Log("ChangeStartReadyClientRpc - startButtonController es null");
        }
    }
}