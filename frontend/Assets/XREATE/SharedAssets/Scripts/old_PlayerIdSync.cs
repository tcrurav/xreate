using Unity.Netcode;
using UnityEngine;

public class old_PlayerIdSync : NetworkBehaviour
{
    public NetworkVariable<int> PlayerId = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        DebugManager.Log($"New Network Spawn:");

        // Set the player ID when the object is spawned on the network
        if (IsOwner)
        {
            PlayerId.Value = MainManager.GetUser().id;
            DebugManager.Log($"New Network Spawn. PlayerId.Value: {PlayerId.Value}");
        }



        PlayerId.OnValueChanged += (oldValue, newValue) =>
        {
            DebugManager.Log($"Player ID changed: {newValue}");
        };
    }

    public void SetVisibility(bool isVisible)
    {
        if (IsClient) // changes are made in clients only.
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = isVisible;
            }
        }
    }
}
