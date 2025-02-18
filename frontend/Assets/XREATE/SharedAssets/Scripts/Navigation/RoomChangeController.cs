using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomChangeController : MonoBehaviour
{
    public Button ClickToGoButton;
    public Button EnableNextRoomButton;
    public TMP_Text RedText;
    public TMP_Text GreenText;

    private readonly Vector3 OffsetRoomModuleA = new(87f, 6.39f, -25.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleA = new(87f, 6.75f, -15.75f);
    private readonly Vector3 OffsetCorridorToRoomModuleB = new(87f, 6.75f, 19f);
    private readonly Vector3 OffsetRoomModuleB = new(87f, 6.39f, 28f);
    //private readonly Vector3 OffsetCorridorToLeisureModule = new(99f, 6.75f, 11.5f);
    private readonly Vector3 OffsetLeisureModule = new(104f, 5.75f, 16f);
    private readonly Vector3 OffsetBuildingA = new(0, 0, -12);
    private readonly Vector3 OffsetBuildingATopStairs = new(9, 4.02f, 0);

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

    public void ChangeToRoomModuleA()
    {
        MainNavigationManager.ChangePosition(OffsetRoomModuleA);
        SetScene(Scene.RoomModuleA);
    }

    public void ChangeToCorridorToRoomModuleA()
    {
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleA);
        SetScene(Scene.TunnelConnectorD);
    }

    public void ChangeToCorridorToRoomModuleB()
    {
        MainNavigationManager.ChangePosition(OffsetCorridorToRoomModuleB);
        SetScene(Scene.TunnelConnectorC);
    }

    public void ChangeToRoomModuleB()
    {
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
        MainNavigationManager.ChangePosition(OffsetLeisureModule);
        SetScene(Scene.LeisureModule);
    }

    public void ChangeToBuildingA()
    {
        MainNavigationManager.ChangePosition(OffsetBuildingA);
        SetScene(Scene.Main);
    }

    public void ChangeToBuildingATopStairs()
    {
        MainNavigationManager.ChangePosition(OffsetBuildingATopStairs);
        SetScene(Scene.Main);
    }

    public void SetScene(Scene scene) 
    {
        // TODO - This has to be tested - May be put it in MainNavigationMAnager.ChangeSceneForLocalNetworkPlayer Or just don't do this call
        StartCoroutine(CurrentActivityManager.WaitForPlayerObjectAndThenChangeTeamId());

        MainManager.SetScene(scene);
    }
}
