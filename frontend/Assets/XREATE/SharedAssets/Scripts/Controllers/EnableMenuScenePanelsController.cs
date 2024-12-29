using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Android.Gradle.Manifest;

public class EnableMenuScenePanelsController : MonoBehaviour
{
    private void OnEnable()
    {
        EnableAccordingToRole();
    }
    private void EnableAccordingToRole()
    {
        GameObject learningPathGameObject = FindObject.FindInsideParentByName(gameObject, "LearningPath Spatial Panel UI");
        GameObject InActivityTeacherParticipationsGameObject = FindObject.FindInsideParentByName(gameObject, "InActivityTeacherParticipations Spatial Panel UI");
        GameObject AllActivitiesGameObject = FindObject.FindInsideParentByName(gameObject, "AllActivities Spatial Panel UI");

        switch (MainManager.GetUser().role)
        {
            case "student":
                learningPathGameObject.SetActive(true);
                InActivityTeacherParticipationsGameObject.SetActive(false);
                AllActivitiesGameObject.SetActive(false);
                break;
            case "teacher":
                learningPathGameObject.SetActive(false);
                InActivityTeacherParticipationsGameObject.SetActive(true);
                AllActivitiesGameObject.SetActive(false);
                break;
            case "guest":
                learningPathGameObject.SetActive(false);
                InActivityTeacherParticipationsGameObject.SetActive(false);
                AllActivitiesGameObject.SetActive(true);
                break;
        }
    }
}
