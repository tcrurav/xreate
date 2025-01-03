using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Android.Gradle.Manifest;

public class AllActivitiesController : MonoBehaviour
{
    private ActivityService activityService;

    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private void Start()
    {
        activityService = gameObject.AddComponent<ActivityService>();

        Refresh();
    }

    public void Refresh()
    {
        Debug.Log("Refresh");
        //loadingCanvas.SetActive(true);
        StartCoroutine(GetActivitiesNonExpired());
    }

    IEnumerator GetActivitiesNonExpired()
    {
        if (MainManager.GetUser().role != "guest")
        {
            throw new System.Exception("Error: Only teachers can see their activities");
        }

        yield return activityService.GetAllNonExpired();

        Debug.Log("reponseCode");
        Debug.Log(activityService.responseCode);

        if (activityService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        CreateButtons();
    }

    public void CreateButtons()
    {
        for (int i = 0; i < activityService.activitiesNonExpired.Length; i++)
        {
            Activity data =
                activityService.activitiesNonExpired[i];

            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(buttonContainer.transform, false);

            GameObject textObject = FindObject.FindInsideParentByName(newButton, "Text (TMP)");
            textObject.GetComponent<TMP_Text>().SetText(
                data.type + "<br>" +
                data.name);

            Button tempButton = newButton.GetComponent<Button>();
            tempButton.onClick.AddListener(() => ButtonClicked(data.type, data.name));
        }
    }
    // From: https://discussions.unity.com/t/how-to-create-ui-button-dynamically/621275/5

    private void ButtonClicked(string activityType, string activityName)
    {
        
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
