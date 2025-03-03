using Unity.Netcode;

public class RoomModuleAGameManager : NetworkBehaviour
{
    //public NetworkList<bool> startedPanels = new();                     // Contain the if the panel has been started
    //public NetworkList<int> panelsAnswered = new();                      // Contains the panel answered for each question
    public NetworkVariable<bool> startReadyToGame = new(false);
    public NetworkVariable<bool> startReadyToNextRoom = new(false);

    // TODO - Get how many students are participating and how many questions
    public int numberOfStudents;
    public int numberOfQuestions;

    private RoomModuleAGameController roomModuleAGameController;
    private StartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        DebugManager.Log("RoomModuleAGameManager - Start");

        roomModuleAGameController = GetComponent<RoomModuleAGameController>();
        startButtonController = GetComponent<StartButtonController>();

        if (roomModuleAGameController == null)
        {
            DebugManager.Log("RoomModuleAGameManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        DebugManager.Log($"RoomModuleAGameManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (IsServer)
        {
            //for (int i = 0; i < numberOfStudents; i++) startedPanels.Add(false);
            //for (int i = 0; i < numberOfQuestions; i++) panelsAnswered.Add(0);
        }

        if (!isSubscribed)
        {

            //startedPanels.OnListChanged += (NetworkListEvent<bool> changeEvent) =>
            //{
            //    ChangeStartedPanelsClientRpc(changeEvent.Index, changeEvent.Value);
            //};

            //panelsAnswered.OnListChanged += (NetworkListEvent<int> changeEvent) =>
            //{
            //    ChangePanelsAnsweredClientRpc(changeEvent.Index, changeEvent.Value);
            //};

            startReadyToGame.OnValueChanged += (oldValue, newValue) =>
            {
                ChangeStartReadyToGameClientRpc(oldValue, newValue);
            };

            startReadyToNextRoom.OnValueChanged += (oldValue, newValue) =>
            {
                DebugManager.Log($"RoomModuleAGameManager - startReadyToNextRoom.OnValueChanged - newValue: {newValue}");
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
        //startedPanels?.Dispose();
        //panelsAnswered?.Dispose();
        startReadyToGame?.Dispose();
        startReadyToNextRoom?.Dispose();
    }

    //[ServerRpc(RequireOwnership = false)]
    //public void ChangeStartedPanelsServerRpc(int index, bool newValue)
    //{
    //    if (startedPanels[index] != newValue)
    //    {
    //        startedPanels.RemoveAt(index);
    //        startedPanels.Insert(index, newValue);
    //    }
    //}

    //[ClientRpc]
    //public void ChangeStartedPanelsClientRpc(int index, bool newValue)
    //{
    //    if (roomModuleAGameController != null)
    //    {
    //        roomModuleAGameController.GotoNextSlide();
    //    }
    //    else
    //    {
    //        DebugManager.Log("RoomModuleAGameManager - roomModuleBGameController es null");
    //    }
    //}

    //[ServerRpc(RequireOwnership = false)]
    //public void ChangePanelsAnsweredServerRpc(int index, int newValue)
    //{
    //    if (panelsAnswered[index] != newValue)
    //    {
    //        panelsAnswered.RemoveAt(index);
    //        panelsAnswered.Insert(index, newValue);
    //    }
    //}

    //[ClientRpc]
    //public void ChangePanelsAnsweredClientRpc(int index, int newValue)
    //{
    //    if (roomModuleAGameController != null)
    //    {
    //        // TODO - IMPORTANT
    //        roomModuleAGameController.GotoNextSlide();
    //    }
    //    else
    //    {
    //        DebugManager.Log("RoomModuleAGameManager - roomModuleBGameController es null");
    //    }
    //}

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToGameServerRpc(bool newStartReadyToGame)
    {
        if (roomModuleAGameController != null && startReadyToGame.Value != newStartReadyToGame)
        {
            startReadyToGame.Value = newStartReadyToGame;
        }
        else
        {
            DebugManager.Log("RoomModuleAGameManager - roomModuleBGameController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToGameClientRpc(bool oldValue, bool newValue)
    {
        if (roomModuleAGameController != null)
        {
            // TODO - IMPORTANT
            roomModuleAGameController.GotoNextSlide();
        }
        else
        {
            DebugManager.Log("RoomModuleAGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToNextRoomServerRpc(bool newValue)
    {
        DebugManager.Log($"RoomModuleAGameManager - ChangeStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null && startReadyToNextRoom.Value != newValue)
        {
            startReadyToNextRoom.Value = newValue;
        }
        else
        {
            DebugManager.Log("RoomModuleAGameManager - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        DebugManager.Log($"RoomModuleAGameManager - ChangeStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            GetComponent<StartButtonController>().EnableNextRooms();
        }
        else
        {
            DebugManager.Log("RoomModuleAGameManager - startButtonController es null");
        }
    }


}