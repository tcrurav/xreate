using Unity.Netcode;
using UnityEngine;

public class PlayerIdSync : NetworkBehaviour
{
    public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"New Network Spawn:");

        // Set the player ID when the object is spawned on the network
        if (IsOwner)
        {
            PlayerId.Value = MainManager.GetUser().id;
        }

        Debug.Log($"New Network Spawn. PlayerId.Value: {PlayerId.Value}");

        PlayerId.OnValueChanged += (oldValue, newValue) => {
            Debug.Log($"Player ID changed: {newValue}");
        };
    }
}
