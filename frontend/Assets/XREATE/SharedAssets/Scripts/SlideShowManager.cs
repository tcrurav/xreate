using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;
using UnityEngine;

public class SlideShowManager : NetworkBehaviour
{
    //public static SlideShowManager Instance;
    public NetworkVariable<int> currentSlide = new(0);
    public NetworkVariable<bool> forceAttention = new(false);

    //private void Awake()
    //{
    //    Instance = this;
    //}

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //if (!IsServer)
        //{
            Debug.Log($"SlideShowManager - OnNetworkSpawn - IsServer: {IsServer}");

            currentSlide.OnValueChanged += (oldValue, newValue) =>
            {
                ChangeCurrentSlideClientRpc(oldValue, newValue);
            };

            forceAttention.OnValueChanged += (oldValue, newValue) =>
            {
                ChangeForceAttentionClientRpc(oldValue, newValue);
            };
        //}
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
        Debug.Log($"ChangeForceAttentionServerRpc - newValue: {newForceAttention}");
        forceAttention.Value = newForceAttention;
    }

    [ClientRpc]
    public void ChangeForceAttentionClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log($"ChangeForceAttentionClientRpc - newValue: {newValue}");
        GetComponent<ForceAttentionController>().SetToggleValue(newValue);
    }

    // Método para cambiar el índice de la diapositiva desde el cliente
    //public void ChangeSlide(int newIndex)
    //{
    //    if (IsOwner) // Asegúrate de que el propietario puede cambiar el índice
    //    {
    //        ChangeCurrentSlideServerRpc(newIndex);
    //    }
    //}


}