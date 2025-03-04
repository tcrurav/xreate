using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnableNextRoomController : MonoBehaviour
{
    public Button ClickToGoButton;
    public Button EnableNextRoomButton;
    public TMP_Text RedText;
    public TMP_Text GreenText;

    private readonly Vector3 OffsetMenuScene = new(0, 0, -12); // Maybe should have own controller
    private readonly Vector3 OffsetMainScene = new(0, 0, 50);
    private readonly Vector3 OffsetAuditoriumScene = new(-103, 0, 50);

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

    public void ChangeToMenuScene()
    {
        MainNavigationManager.ChangePosition(OffsetMenuScene);
        MainNavigationManager.EnableSceneContainer("MenuSceneContainer");
        SetScene(Scene.Menu);
    }

    public void ChangeToMainScene()
    {
        MainNavigationManager.ChangePosition(OffsetMainScene);
        MainNavigationManager.EnableSceneContainer("MainSceneContainer");
        SetScene(Scene.Main);
    }
    public void ChangeToAuditoriumScene()
    {
        MainNavigationManager.ChangePosition(OffsetAuditoriumScene);
        MainNavigationManager.EnableSceneContainer("AuditoriumSceneContainer");
        SetScene(Scene.Auditorium);
    }

    private void SetScene(Scene scene) 
    {
        // TODO - This has to be tested - May be put it in MainNavigationMAnager.ChangeSceneForLocalNetworkPlayer Or just don't do this call
        //StartCoroutine(CurrentActivityManager.WaitForPlayerObjectAndThenChangeTeamId());

        StartCoroutine(MainNavigationManager.WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(scene));
        MainManager.SetScene(scene);
    }
}
