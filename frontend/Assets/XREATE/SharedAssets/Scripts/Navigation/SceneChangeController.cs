using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeController : MonoBehaviour
{
    public Button ClickToGoButton;
    public Button EnableNextRoomButton;
    public TMP_Text RedText;
    public TMP_Text GreenText;

    private readonly Vector3 OffsetMenu = new(0, 0, -12); // Maybe should have own controller

    private void Start()
    {
        if(MainManager.GetUser().role != "TEACHER")
        {
            // Only a Teacher can enable next Room
            EnableNextRoomButton.gameObject.GetComponent<Button>().enabled = false;
        }
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

    public void ChangeToMenuScene()
    {
        MainNavigationManager.ChangePosition(OffsetMenu);
        SetScene(Scene.Menu);
    }

    private void SetScene(Scene scene) 
    {
        // TODO - This has to be tested - May be put it in MainNavigationMAnager.ChangeSceneForLocalNetworkPlayer Or just don't do this call
        //StartCoroutine(CurrentActivityManager.WaitForPlayerObjectAndThenChangeTeamId());

        StartCoroutine(MainNavigationManager.WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(scene));
        MainManager.SetScene(scene);

        MainNavigationManager.EnableSceneContainer("MenuSceneContainer");
    }
}
