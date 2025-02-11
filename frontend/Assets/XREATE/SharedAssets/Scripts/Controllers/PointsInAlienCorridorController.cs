using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsInAlienCorridorController : MonoBehaviour
{
    public string challengeName;
    public string challengeItemItem;
    public TMP_InputField PasswordInputField;

    private AchievementItemService achievementItemService;

    private bool PasswordIsLongerThanEigth = false;


    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private void Start()
    {
        achievementItemService = gameObject.AddComponent<AchievementItemService>();
    }

    public void CalculatePointsAndUpdateInAPI()
    {
        int points = CalculatePoints();
        StartCoroutine(UpdatePoints(points));
    }

    private int CalculatePoints()
    {
        string passwordEntered = PasswordInputField.text;

        int points = 0;

        if (passwordEntered.Length >= 8)
        {
            PasswordIsLongerThanEigth = true;
            points++;
        }

        points++; // TODO just to test

        // TODO More cases and points 
        return points;
    }

    private IEnumerator UpdatePoints(int points)
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            throw new System.Exception("Error: Only students get points");
        }

        int studentId = MainManager.GetUser().id;
        int activityId = CurrentActivityManager.GetCurrentActivityId();

        yield return achievementItemService.UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
             challengeName, challengeItemItem, studentId, activityId, points);

        if (achievementItemService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

    }


}
