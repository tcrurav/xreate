using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeController : MonoBehaviour
{

    private readonly Vector3 OffsetMenuScene = new(0, 0, -12); // Maybe should have own controller
    private readonly Vector3 OffsetMainScene = new(0, 0, 50);
    private readonly Vector3 OffsetAuditoriumScene = new(-103, 0, 50);

    public void ChangeToMenuScene()
    {
        MainNavigationManager.ChangePosition(OffsetMenuScene);
        SetScene(Scene.Menu);
        MainNavigationManager.EnableSceneContainer("MenuSceneContainer");
    }

    public void ChangeToMainScene()
    {
        MainNavigationManager.ChangePosition(OffsetMainScene);
        SetScene(Scene.Main);
        MainNavigationManager.EnableSceneContainer("MainSceneContainer");
    }
    public void ChangeToAuditoriumScene()
    {
        MainNavigationManager.ChangePosition(OffsetAuditoriumScene);
        SetScene(Scene.Auditorium);
        MainNavigationManager.EnableSceneContainer("AuditoriumSceneContainer"); 
    }

    private void SetScene(Scene scene) 
    {
        // TODO - This has to be tested - May be put it in MainNavigationMAnager.ChangeSceneForLocalNetworkPlayer Or just don't do this call
        //StartCoroutine(CurrentActivityManager.WaitForPlayerObjectAndThenChangeTeamId());

        StartCoroutine(MainNavigationManager.WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(scene));
        MainManager.SetScene(scene);
    }
}
