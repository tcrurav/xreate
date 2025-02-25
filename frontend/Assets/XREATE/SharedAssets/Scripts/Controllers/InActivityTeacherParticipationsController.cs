using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InActivityTeacherParticipationsController : MonoBehaviour
{
    private InActivityTeacherParticipationService inActivityTeacherParticipationService;

    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private void Start()
    {
        inActivityTeacherParticipationService = gameObject.AddComponent<InActivityTeacherParticipationService>();

        Refresh();
    }

    public void Refresh()
    {
        //loadingCanvas.SetActive(true);
        StartCoroutine(GetInActivityTeacherParticipationsWithActivityAndChallenge());
    }

    IEnumerator GetInActivityTeacherParticipationsWithActivityAndChallenge()
    {
        if (MainManager.GetUser().role != "TEACHER")
        {
            throw new System.Exception("Error: Only teachers can see their activities");
        }

        yield return inActivityTeacherParticipationService.GetAllWithActivityAndChallenge(MainManager.GetUser().id);

        if (inActivityTeacherParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        CreateButtons();
    }

    public void CreateButtons()
    {
        for (int i = 0; i < inActivityTeacherParticipationService.inActivityTeacherParticipationsWithActivityAndChallenge.Length; i++)
        {
            InActivityTeacherParticipationWithActivityAndChallenge data =
                inActivityTeacherParticipationService.inActivityTeacherParticipationsWithActivityAndChallenge[i];

            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(buttonContainer.transform, false);

            GameObject textObject = FindObject.FindInsideParentByName(newButton, "Text (TMP)");
            textObject.GetComponent<TMP_Text>().SetText(
                data.activityType + "<br>" +
                data.activityName);

            Button tempButton = newButton.GetComponent<Button>();
            tempButton.onClick.AddListener(() => ButtonClicked(data.activityType, data.activityName, data.activityId));
        }
    }
    // From: https://discussions.unity.com/t/how-to-create-ui-button-dynamically/621275/5

    // TODO - (DRY - Don't Repeat Yourselfe) ButtonClicked should NOT be repeated in: InActivityTeacherParticipationController, AllActivitiesController and LearningPathController. 
    private void ButtonClicked(string activityType, string activityName, int activityId)
    {
        CurrentActivityManager.SetCurrentActivityId(activityId);

        switch (activityType)
        {
            case "TRAINING_LAB":
                // TODO - Only 1 training lab at the moment (There will be many "activityName" in the future)
                MainNavigationManager.EnableSceneContainer("MainSceneContainer");
                MainManager.SetScene(Scene.Main);
                break;
            case "VIRTUAL_CLASSROOM":
                // TODO - Only 1 virtual classroom at the moment (There will be many "activityName" in the future)
                MainNavigationManager.EnableSceneContainer("AuditoriumSceneContainer");
                MainManager.SetScene(Scene.Auditorium);
                break;
            case "ASSET_LAB":
                // TODO - No VIRTUAL_CLASSROOM yet
                MainNavigationManager.EnableSceneContainer("TODO - No ASSET_LAB yet");
                break;
        }
    }
}
