using Unity.Netcode;
using UnityEngine;

public class SlideShowNetworkManager : NetworkBehaviour
{
    public static SlideShowNetworkManager Instance;
    public NetworkVariable<int> currentSlide = new NetworkVariable<int>(0);

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        currentSlide.OnValueChanged += (oldValue, newValue) =>
        {
            GetComponent<SlideController>().HideOldSlideAndShowNewSlide(oldValue, newValue);
        };
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeCurrentSlideServerRpc(int newCurrentSlide)
    {
        currentSlide.Value = newCurrentSlide;
    }
}