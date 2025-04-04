using Unity.Netcode;
using UnityEngine;

public class SlideShowManager : NetworkBehaviour
{
    public NetworkVariable<int> currentSlide = new(0);
    public NetworkVariable<bool> forceAttention = new(false);
    public NetworkVariable<bool> startReady = new(false);

    private SlideController slideController;
    private ForceAttentionController forceAttentionController;
    private StartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        slideController = GetComponent<SlideController>();
        forceAttentionController = GetComponent<ForceAttentionController>();
        startButtonController = GetComponent<StartButtonController>();

        if (slideController == null || forceAttentionController == null || startButtonController == null)
        {
            Debug.Log("SlideShowManager - Start - Faltan componentes necesarios en el GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"SlideShowManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (!isSubscribed)
        {

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
        currentSlide?.Dispose();
        forceAttention?.Dispose();
        startReady?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeCurrentSlideServerRpc(int newCurrentSlide)
    {
        if (currentSlide.Value != newCurrentSlide)
        {
            Debug.Log($"ChangeCurrentSlideServerRpc - newCurrentSlide: {newCurrentSlide}");
            currentSlide.Value = newCurrentSlide;
        }
    }

    [ClientRpc]
    public void ChangeCurrentSlideClientRpc(int oldValue, int newValue)
    {
        Debug.Log($"ChangeCurrentSlideClientRpc - oldValue: {oldValue}, newValue: {newValue}");
        if (slideController != null)
        {
            Debug.Log("ChangeCurrentSlideClientRpc - Llamando a HideOldSlideAndShowNewSlide");
            slideController.HideOldSlideAndShowNewSlide(oldValue, newValue);
        }
        else
        {
            Debug.Log("ChangeCurrentSlideClientRpc - SlideController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeForceAttentionServerRpc(bool newForceAttention)
    {
        if (forceAttentionController != null && forceAttention.Value != newForceAttention)
        {
            forceAttention.Value = newForceAttention;
        }
        else
        {
            Debug.Log("ChangeForceAttentionServerRpc - forceAttentionController es null");

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
            Debug.Log("ChangeForceAttentionClientRpc - forceAttentionController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyServerRpc(bool newStartReady)
    {
        if (startButtonController != null && startReady.Value != newStartReady)
        {
            startReady.Value = newStartReady;
        }
        else
        {
            Debug.Log("ChangeStartReadyServerRpc - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyClientRpc(bool oldValue, bool newValue)
    {
        if (startButtonController != null)
        {
            GetComponent<StartButtonController>().EnableNextRooms();
        }
        else
        {
            Debug.Log("ChangeStartReadyClientRpc - startButtonController es null");
        }
    }
}