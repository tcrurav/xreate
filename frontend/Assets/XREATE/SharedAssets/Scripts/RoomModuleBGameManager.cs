using Unity.Netcode;

public class RoomModuleBGameManager : NetworkBehaviour
{
    public NetworkList<bool> startedPanels = new();                     // Contain the if the panel has been started
    public NetworkList<int> panelsAnswered = new();                      // Contains the panel answered for each question
    public NetworkVariable<bool> startReadyToGame = new(false);
    public NetworkVariable<bool> startReadyToNextRoom = new(false);

    // TODO - Get how many students are participating and how many questions
    public int numberOfStudents;
    public int numberOfQuestions;

    private RoomModuleBGameController roomModuleBGameController;
    private StartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        DebugManager.Log("RoomModuleBGameManager - Start");

        roomModuleBGameController = GetComponent<RoomModuleBGameController>();
        startButtonController = GetComponent<StartButtonController>();

        if (roomModuleBGameController == null)
        {
            DebugManager.Log("RoomModuleBGameManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        DebugManager.Log($"RoomModuleBGameManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (IsServer)
        {
            for (int i = 0; i < numberOfStudents; i++) startedPanels.Add(false);
            for (int i = 0; i < numberOfQuestions; i++) panelsAnswered.Add(0);
        }

        if (!isSubscribed)
        {

            startedPanels.OnListChanged += (NetworkListEvent<bool> changeEvent) =>
            {
                ChangeStartedPanelsClientRpc(changeEvent.Index, changeEvent.Value);
            };

            panelsAnswered.OnListChanged += (NetworkListEvent<int> changeEvent) =>
            {
                ChangePanelsAnsweredClientRpc(changeEvent.Index, changeEvent.Value);
            };

            startReadyToGame.OnValueChanged += (oldValue, newValue) =>
            {
                ChangeStartReadyToGameClientRpc(oldValue, newValue);
            };

            startReadyToNextRoom.OnValueChanged += (oldValue, newValue) =>
            {
                DebugManager.Log($"RoomModuleBGameManager - startReadyToNextRoom.OnValueChanged - newValue: {newValue}");
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
        startedPanels?.Dispose();
        panelsAnswered?.Dispose();
        startReadyToGame?.Dispose();
        startReadyToNextRoom?.Dispose();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartedPanelsServerRpc(int index, bool newValue)
    {
        if (startedPanels[index] != newValue)
        {
            startedPanels.RemoveAt(index);
            startedPanels.Insert(index, newValue);
        }
    }

    [ClientRpc]
    public void ChangeStartedPanelsClientRpc(int index, bool newValue)
    {
        if (roomModuleBGameController != null)
        {
            roomModuleBGameController.GotoNextSlide();
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangePanelsAnsweredServerRpc(int index, int newValue)
    {
        if (panelsAnswered[index] != newValue)
        {
            panelsAnswered.RemoveAt(index);
            panelsAnswered.Insert(index, newValue);
        }
    }

    [ClientRpc]
    public void ChangePanelsAnsweredClientRpc(int index, int newValue)
    {
        if (roomModuleBGameController != null)
        {
            // TODO - IMPORTANT
            roomModuleBGameController.GotoNextSlide();
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToGameServerRpc(bool newStartReadyToGame)
    {
        if (roomModuleBGameController != null)
        {
            startReadyToGame.Value = newStartReadyToGame;
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - roomModuleBGameController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToGameClientRpc(bool oldValue, bool newValue)
    {
        if (roomModuleBGameController != null)
        {
            // TODO - IMPORTANT
            roomModuleBGameController.GotoNextSlide();
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToNextRoomServerRpc(bool newValue)
    {
        DebugManager.Log($"RoomModuleBGameManager - ChangeStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            startReadyToNextRoom.Value = newValue;
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        DebugManager.Log($"RoomModuleBGameManager - ChangeStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            GetComponent<StartButtonController>().EnableNextRooms();
        }
        else
        {
            DebugManager.Log("RoomModuleBGameManager - startButtonController es null");
        }
    }


}