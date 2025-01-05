using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Android.Gradle.Manifest;
using System;

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
            case "STUDENT":
                learningPathGameObject.SetActive(true);
                InActivityTeacherParticipationsGameObject.SetActive(false);
                AllActivitiesGameObject.SetActive(false);
                break;
            case "TEACHER":
                learningPathGameObject.SetActive(false);
                InActivityTeacherParticipationsGameObject.SetActive(true);
                AllActivitiesGameObject.SetActive(false);
                break;
            case "GUEST":
                learningPathGameObject.SetActive(false);
                InActivityTeacherParticipationsGameObject.SetActive(false);
                AllActivitiesGameObject.SetActive(true);
                break;
        }
    }
}
