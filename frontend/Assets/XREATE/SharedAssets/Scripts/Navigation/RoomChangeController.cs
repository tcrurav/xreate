using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomChangeController : MonoBehaviour
{
    public Button ClickToGoButton;
    public Button EnableNextRoomButton;
    public TMP_Text RedText;
    public TMP_Text GreenText;
    public int teamId;

    private readonly Vector3 OffsetMenu = new(0, 0, -12); // Maybe should have own controller

    private readonly Vector3 OffsetRoomModuleA = new(87f, 6.39f, 25.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(87f, 6.75f, 35.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(87f, 6.75f, 69f);
    private readonly Vector3 OffsetRoomModuleB = new(87f, 6.39f, 78f);
    //private readonly Vector3 OffsetCorridorToLeisureModule = new(99f, 6.75f, 11.5f);
    private readonly Vector3 OffsetLeisureModule = new(104f, 5.75f, 66f);
    private readonly Vector3 OffsetBuildingA = new(0, 0, 38);
    private readonly Vector3 OffsetBuildingATopStairs = new(9, 4.02f, 50);

    private void Start()
    {
        // TODO - Uncomment this for final version
        //if(MainManager.GetUser().role != "TEACHER")
        //{
        //    // Only a Teacher can enable next Room
        //    EnableNextRoomButton.gameObject.GetComponent<Button>().enabled = false;
        //}
    }

    public void EnableNextRoom()
    {
        EnableNextRoomButton.gameObject.SetActive(false);
        ClickToGoButton.gameObject.SetActive(true);
        RedText.gameObject.SetActive(false);
        GreenText.gameObject.SetActive(true);
    }

    public void DisableNextRoom()
    {
        EnableNextRoomButton.gameObject.SetActive(true);
        ClickToGoButton.gameObject.SetActive(false);
        RedText.gameObject.SetActive(true);
        GreenText.gameObject.SetActive(false);
    }

    //public void ChangeToMenuScene()
    //{
    //    MainNavigationManager.ChangePosition(OffsetMenu);
    //    SetScene(Scene.Menu);
    //}

    public void ChangeToRoomModuleA()
    {
        // TODO - UNCOMMENT ALL THIS LINES
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetRoomModuleA);
        SetScene(Scene.RoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleA);
        SetScene(Scene.TunnelConnectorD);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleB);
        SetScene(Scene.TunnelConnectorC);
    }

    public void ChangeToRoomModuleB()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetRoomModuleB);
        SetScene(Scene.RoomModuleB);
    }

    //public void ChangeToCorridorToLeisureModule()
    //{
    //    MainNavigationManager.ChangePosition(OffsetCorridorToLeisureModule);
    //    SetScene(Scene.TunnelConnectorF);
    //}

    public void ChangeToLeisureModule()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetLeisureModule);
        SetScene(Scene.LeisureModule);
    }

    public void ChangeToBuildingA()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetBuildingA);
        SetScene(Scene.Main);
    }

    public void ChangeToBuildingATopStairs()
    {
        // Teachers and Guest are allowed to go everywhere. Only students in the right team are allowed in the next room.
        //if (MainManager.GetUser().role == "STUDENT" && teamId != CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id)) return;

        MainNavigationManager.ChangePosition(OffsetBuildingATopStairs);
        SetScene(Scene.Main);
    }

    private void SetScene(Scene scene) 
    {
        // TODO - This has to be tested - May be put it in MainNavigationMAnager.ChangeSceneForLocalNetworkPlayer Or just don't do this call
        //StartCoroutine(CurrentActivityManager.WaitForPlayerObjectAndThenChangeTeamId());

        StartCoroutine(MainNavigationManager.WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(scene));
        MainManager.SetScene(scene);

        //MainNavigationManager.EnableSceneContainer("MenuSceneContainer");
    }
}
