using Unity.Netcode;
using UnityEngine;

public class RoomModuleAGameManager : NetworkBehaviour
{
    public NetworkList<bool> panelsAnswered = new();
    public NetworkVariable<bool> startReadyToGame = new(false);
    public NetworkVariable<bool> startReadyToNextRoom = new(false);
    public NetworkVariable<bool> enableStartReadyToNextRoom = new(false);

    private RoomModuleAGameController roomModuleAGameController;
    private StartButtonController startButtonController;

    public int MaxNumberOfStudents;

    private bool isSubscribed = false;

    private void Start()
    {
        Debug.Log("RoomModuleAGameManager - Start");

        roomModuleAGameController = GetComponent<RoomModuleAGameController>();
        startButtonController = GetComponent<StartButtonController>();

        if (roomModuleAGameController == null || startButtonController == null)
        {
            Debug.Log("RoomModuleAGameManager - Start - missing mandatory components in GameObject.");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Debug.Log($"RoomModuleAGameManager - OnNetworkSpawn - IsServer: {IsServer} - IsClient: {IsClient}");

        if (IsServer)
        {
            for (int i = 0; i < MaxNumberOfStudents; i++) panelsAnswered.Add(false);
        }

        if (!isSubscribed)
        {
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

    private void OnPanelsAnsweredChanged(NetworkListEvent<bool> changeEvent)
    {
        if (changeEvent.Type == NetworkListEvent<bool>.EventType.Insert)
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
            panelsAnswered.OnListChanged -= OnPanelsAnsweredChanged;
            startReadyToGame.OnValueChanged -= OnStartReadyToGameChanged;
            startReadyToNextRoom.OnValueChanged -= OnStartReadyToNextRoomChanged;
            enableStartReadyToNextRoom.OnValueChanged -= OnEnableStartReadyToNextRoomChanged;

            isSubscribed = false;
        }

        base.OnDestroy();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangePanelsAnsweredServerRpc(int index, bool newValue)
    {
        if (panelsAnswered[index] != newValue)
        {
            panelsAnswered.RemoveAt(index);
            panelsAnswered.Insert(index, newValue);
        }
    }

    [ClientRpc]
    public void ChangePanelsAnsweredClientRpc(int index, bool newValue)
    {
        if (roomModuleAGameController != null)
        {
            int numberOfFinishedPanels = 0;
            for(int i = 0; i < panelsAnswered.Count; i++)
            {
                if(panelsAnswered[i]) numberOfFinishedPanels++;
            }
            roomModuleAGameController.UpdateTextureForNumberOfFinishedPanels(index);
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - roomModuleAGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToGameServerRpc(bool newStartReadyToGame)
    {
        Debug.Log("RoomModuleAGameManager - ChangeStartReadyToGameServerRpc");
        if (roomModuleAGameController != null && startReadyToGame.Value != newStartReadyToGame)
        {
            startReadyToGame.Value = newStartReadyToGame;
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - roomModuleAGameController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToGameClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log("RoomModuleAGameManager - ChangeStartReadyToGameClientRpc");
        if (roomModuleAGameController != null)
        {
            roomModuleAGameController.StartGame();
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - roomModuleAGameController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStartReadyToNextRoomServerRpc(bool newValue)
    {
        Debug.Log($"RoomModuleAGameManager - ChangeStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null && startReadyToNextRoom.Value != newValue)
        {
            startReadyToNextRoom.Value = newValue;
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - startButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log($"RoomModuleAGameManager - ChangeStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            startButtonController.EnableNextRooms();
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - startButtonController es null");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeEnableStartReadyToNextRoomServerRpc(bool newValue)
    {
        Debug.Log($"RoomModuleAGameManager - ChangeEnableStartReadyToNextRoomServerRpc - newValue: {newValue}");
        if (startButtonController != null && enableStartReadyToNextRoom.Value != newValue)
        {
            enableStartReadyToNextRoom.Value = newValue;
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - enableStartButtonController es null");

        }
    }

    [ClientRpc]
    public void ChangeEnableStartReadyToNextRoomClientRpc(bool oldValue, bool newValue)
    {
        Debug.Log($"RoomModuleAGameManager - ChangeEnableStartReadyToNextRoomClientRpc - newValue: {newValue}");
        if (startButtonController != null)
        {
            startButtonController.EnableButtonToEnableNextRooms();
        }
        else
        {
            Debug.Log("RoomModuleAGameManager - enableStartButtonController es null");
        }
    }


}