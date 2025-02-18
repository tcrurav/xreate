using Unity.Netcode;

public class StartTrainingNetworkManager : NetworkBehaviour
{
    public static StartTrainingNetworkManager Instance;
    public NetworkVariable<bool> start = new NetworkVariable<bool>(false);

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        start.OnValueChanged += (oldValue, newValue) =>
        {
            GetComponent<RoomChangeController>().EnableNextRoom();
        };
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartServerRpc(bool newValue)
    {
        start.Value = newValue;
    }
}