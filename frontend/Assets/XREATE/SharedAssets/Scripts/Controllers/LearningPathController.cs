using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearningPathController : MonoBehaviour
{
    private InActivityStudentParticipationService inActivityStudentParticipationService;

    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private void Start()
    {
        inActivityStudentParticipationService = gameObject.AddComponent<InActivityStudentParticipationService>();

        Refresh();
    }

    public void Refresh()
    {
        //loadingCanvas.SetActive(true);
        StartCoroutine(GetInActivityStudentParticipationsWithActivityAndPoints());
    }

    IEnumerator GetInActivityStudentParticipationsWithActivityAndPoints()
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            throw new System.Exception("Error: Only students have a Learning path");
        }

        yield return inActivityStudentParticipationService.GetAllWithActivityAndPoints(MainManager.GetUser().id);

        if (inActivityStudentParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        CreateButtons();
    }

    public void CreateButtons()
    {
        for (int i = 0; i < inActivityStudentParticipationService.inActivityStudentParticipationsWithActivityAndPoints.Length; i++)
        {
            InActivityStudentParticipationWithActivityAndPoints data =
                inActivityStudentParticipationService.inActivityStudentParticipationsWithActivityAndPoints[i];

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
                Debug.Log("Only 1 activityName at the moment: " + activityName);
                MainNavigationManager.EnableSceneContainer("MainSceneContainer");
                break;
            case "VIRTUAL_CLASSROOM":
                // TODO - No VIRTUAL_CLASSROOM yet
                MainNavigationManager.EnableSceneContainer("TODO - No VIRTUAL_CLASSROOM yet");
                break;
            case "ASSET_LAB":
                // TODO - No VIRTUAL_CLASSROOM yet
                MainNavigationManager.EnableSceneContainer("TODO - No ASSET_LAB yet");
                break;
        }
    }
}
