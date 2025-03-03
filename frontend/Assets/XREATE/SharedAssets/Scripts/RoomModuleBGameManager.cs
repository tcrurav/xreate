using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomModuleBGameManager : NetworkBehaviour
{
    //public NetworkList<bool> startedPanels = new();                     // Contain the if the panel has been started
    //public NetworkList<int> panelsAnswered = new();                      // Contains the panel answered for each question
    public NetworkVariable<bool> startReadyToGame = new(false);
    public NetworkVariable<bool> startReadyToNextRoom = new(false);

    // TODO - Get how many students are participating and how many questions
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
            //for (int i = 0; i < MaxNumberOfStudents; i++) startedPanels.Add(false);
            //for (int i = 0; i < MaxNumberOfQuestions; i++) panelsAnswered.Add(0);
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
                Debug.Log($"RoomModuleBGameManager - startReadyToNextRoom.OnValueChanged - newValue: {newValue}");
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

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartedPanelsServerRpc(int index, bool newValue)
    {
        //if (startedPanels[index] != newValue)
        //{
        //    startedPanels.RemoveAt(index);
        //    startedPanels.Insert(index, newValue);
        //}
        ChangeStartedPanelsClientRpc(index, newValue);
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
        //if (panelsAnswered[index] != newValue)
        //{
        //    panelsAnswered.RemoveAt(index);
        //    panelsAnswered.Insert(index, newValue);
        //}
        ChangePanelsAnsweredClientRpc(index, newValue);
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
            GetComponent<StartButtonController>().EnableNextRooms();
        }
        else
        {
            Debug.Log("RoomModuleBGameManager - startButtonController es null");
        }
    }


}