using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public Button EnableNextRoomButton;
    public Button[] ClickToGoButton;
    public TMP_Text[] RedText;
    public TMP_Text[] GreenText;
    public GameObject[] signs;

    public int teamId;

    public TeamMapManager teamMapManager;

    public void EnableNextRooms()
    {
        EnableNextRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < ClickToGoButton.Length; i++)
        {
            ClickToGoButton[i].gameObject.SetActive(true);
            RedText[i].gameObject.SetActive(false);
            GreenText[i].gameObject.SetActive(true);
        }

        DoOtherThingsDependingOnCurrentScene();
    }

    public void EnableButtonToEnableNextRooms()
    {
        // Only for teacher in charge of the team 1 in case is kiosk of team 1
        if (teamId == 1 && MainManager.GetUser().username == MainManager.GetTeam1Teacher())
        {
            EnableNextRoomButton.GetComponent<Button>().interactable = true;
        }

        // Only for teacher in charge of the team 2 in case is kiosk of team 2
        if (teamId == 2 && MainManager.GetUser().username == MainManager.GetTeam2Teacher())
        {
            EnableNextRoomButton.GetComponent<Button>().interactable = true;
        }
    }

    private void DoOtherThingsDependingOnCurrentScene()
    {
        int teamId = CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id);

        switch (MainManager.GetScene())
        {
            case Scene.Main:
                ActivateSigns();
                teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.RoomModuleB); // team 1 goes to RoomModuleB. (index = teamId - 1) 
                teamMapManager.ChangeCurrentTeamSceneServerRpc(1, (int)Scene.LeisureModule); // team 2 goes to LeisureModule
                break;
            case Scene.RoomModuleB:
                ActivateSigns(); // Activates Leaderboard in the corridor
                if (teamId == 1) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.TunnelConnectorC);
                if (teamId == 2) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.TunnelConnectorC);
                break;
            case Scene.TunnelConnectorC:
                if (teamId == 1) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.RoomModuleA);
                if (teamId == 2) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.RoomModuleA);
                break;
            case Scene.RoomModuleA:
                ActivateSigns(); // Activates Leaderboard in the corridor
                if (teamId == 1) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.TunnelConnectorF);
                if (teamId == 2) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.Main);
                break;
            case Scene.TunnelConnectorF:
                if (teamId == 1) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.LeisureModule);
                if (teamId == 2) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.RoomModuleB);
                break;
            case Scene.LeisureModule:
                if (teamId == 1) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.Main);
                if (teamId == 2) teamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.TunnelConnectorF);
                break;
            case Scene.Auditorium:
                ActivateSigns();
                break;
        }
    }

    private void ActivateSigns()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            signs[i].gameObject.SetActive(true);
        }
    }
}
