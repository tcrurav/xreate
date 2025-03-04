using Unity.Netcode;
using UnityEngine;

public class RoomModuleBGameManager : NetworkBehaviour
{
    public NetworkList<bool> startedPanels = new();                     // Contain the if the panel has been started
    public NetworkList<int> panelsAnswered = new();                      // Contains the panel answered for each question
    public NetworkVariable<bool> startReadyToGame = new(false);
    public NetworkVariable<bool> startReadyToNextRoom = new(false);
    public NetworkVariable<bool> enableStartReadyToNextRoom = new(false);

    public int MaxNumberOfStudents;
    public int MaxNumberOfQuestions;

    private RoomModuleBGameController roomModuleBGameController;
    private StartButtonController startButtonController;

    private bool isSubscribed = false;

    private void Start()
    {
        Debug.Log("RoomModuleBGameManager - Start");

        roomModuleBGameController = GetComponent<RoomModuleBGameController>();
        startButtonController = GetComponent<StartButtonController>();

        if (roomModuleBGameController == null)
        {
            Debug.Log("RoomModuleBGameManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"RoomModuleBGameManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (IsServer)
        {
            for (int i = 0; i < MaxNumberOfStudents; i++) startedPanels.Add(false);
            for (int i = 0; i < MaxNumberOfQuestions; i++) panelsAnswered.Add(0);
        }

        if (!isSubscribed)
        {
            startedPanels.OnListChanged += OnStartedPanelsChanged;

            panelsAnswered.OnListChanged += OnPanelsAnsweredChanged;

            startReadyToGame.OnValueChanged += OnStartReadyToGameChanged;

            startReadyToNextRoom.OnValueChanged += OnStartReadyToNextRoomChanged;

            enableStartReadyToNextRoom.OnValueChanged += OnEnableStartReadyToNextRoomChanged;

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

    //public override void OnDestroy()
    //{
    //    startedPanels?.Dispose();
    //    panelsAnswered?.Dispose();
    //    startReadyToGame?.Dispose();
    //    startReadyToNextRoom?.Dispose();
    //}

    private void OnStartedPanelsChanged(NetworkListEvent<bool> changeEvent)
    {
        if (changeEvent.Type == NetworkListEvent<bool>.EventType.Insert)
        {
            ChangeStartedPanelsClientRpc(changeEvent.Index, changeEvent.Value);
        }
    }

    private void OnPanelsAnsweredChanged(NetworkListEvent<int> changeEvent)
    {
        if (changeEvent.Type == NetworkListEvent<int>.EventType.Insert)
        {
            ChangePanelsAnsweredClientRpc(changeEvent.Index, changeEvent.Value);
        }
    }

    private void OnStartReadyToGameChanged(bool oldValue, bool newValue)
    {
        ChangeStartReadyToGameClientRpc(oldValue, newValue);
    }

    private void OnStartReadyToNextRoomChanged(bool oldValue, bool newValue)
    {
        ChangeStartReadyToNextRoomClientRpc(oldValue, newValue);
    }

    private void OnEnableStartReadyToNextRoomChanged(bool oldValue, bool newValue)
    {
        ChangeEnableStartReadyToNextRoomClientRpc(oldValue, newValue);
    }

    public override void OnDestroy()
    {
        if (isSubscribed)
        {
            startedPanels.OnListChanged -= OnStartedPanelsChanged;
            panelsAnswered.OnListChanged -= OnPanelsAnsweredChanged;
            startReadyToGame.OnValueChanged -= OnStartReadyToGameChanged;
            startReadyToNextRoom.OnValueChanged -= OnStartReadyToNextRoomChanged;
            enableStartReadyToNextRoom.OnValueChanged -= OnEnableStartReadyToNextRoomChanged;

            isSubscribed = false;
        }

        base.OnDestroy();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartedPanelsServerRpc(int index, bool newValue)
    {
        Debug.Log($"ChangeStartedPanelsServerRpc - index: {index}");
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
            roomModuleBGameController.OnPlayerReady(index);
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - roomModuleBGameController es null");
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
            roomModuleBGameController.CheckAnswer(index, newValue);
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToGameServerRpc(bool newStartReadyToGame)
    {
        Debug.Log("RoomModuleBGameManager - ChangeStartReadyToGameServerRpc");
        if (roomModuleBGameController != null && startReadyToGame.Value != newStartReadyToGame)
        {
            startReadyToGame.Value = newStartReadyToGame;
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - roomModuleBGameController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToGameClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log("RoomModuleBGameManager - ChangeStartReadyToGameClientRpc");
        if (roomModuleBGameController != null)
        {
            roomModuleBGameController.StartGame();
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - roomModuleBGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToNextRoomServerRpc(bool newValue)
    {
        Debug.Log($"RoomModuleBGameManager - ChangeStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null && startReadyToNextRoom.Value != newValue)
        {
            startReadyToNextRoom.Value = newValue;
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log($"RoomModuleBGameManager - ChangeStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            startButtonController.EnableNextRooms();
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - startButtonController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeEnableStartReadyToNextRoomServerRpc(bool newValue)
    {
        Debug.Log($"RoomModuleBGameManager - ChangeEnableStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null && enableStartReadyToNextRoom.Value != newValue)
        {
            enableStartReadyToNextRoom.Value = newValue;
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - enableStartButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeEnableStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log($"RoomModuleBGameManager - ChangeEnableStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            startButtonController.EnableButtonToEnableNextRooms();
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - enableStartButtonController es null");
        }
    }


}