using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AchievementItemService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public AchievementItem[] achievementItems;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/achievementItems";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator Create(AchievementItem achievementItem)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(achievementItem));
    }

    public IEnumerator UpdateById(int id, AchievementItem achievementItem)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, achievementItem));
    }
    public IEnumerator ResetPoints()
    {
        UpdateURL();
        yield return StartCoroutine(RestResetPoints());
    }

    public IEnumerator ResetPointsByActivityId(int id)
    {
        UpdateURL();
        yield return StartCoroutine(RestResetPointsByActivityId(id));
    }

    public IEnumerator UpdatePointsByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
        string challengeName, string challengeItemItem, int studentId, int activityId, int points)
    {
        UpdateURL();
        AchievementItem achievementItem = new()
        {
            points = points
        };

        yield return StartCoroutine(RestUpdateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
            challengeName, challengeItemItem, studentId, activityId, achievementItem));
    }

    public IEnumerator UpdateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
        string challengeName, string challengeItemItem, int studentId, int activityId, AchievementItem achievementItem)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
            challengeName, challengeItemItem, studentId, activityId, achievementItem));
    }

    public IEnumerator DeleteById(int id)
    {
        UpdateURL();
        yield return StartCoroutine(RestDeleteById(id));
    }

    private IEnumerator RestGetAll()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        achievementItems = JsonHelper.getJsonArray<AchievementItem>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(AchievementItem achievementItem)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(achievementItem);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

    private IEnumerator RestUpdateById(int id, AchievementItem achievementItem)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(achievementItem);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

    private IEnumerator RestResetPoints()
    {
        var request = new UnityWebRequest(URL + "/resetPoints", "PUT");

        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

    private IEnumerator RestResetPointsByActivityId(int id)
    {
        var request = new UnityWebRequest(URL + "/resetPoints/activity/" + id, "PUT");

        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

    private IEnumerator RestUpdateByChallengeNameAndChallengeItemItemAndStudentIdAndActivityId(
        string challengeName, string challengeItemItem, int studentId, int activityId, AchievementItem achievementItem)
    {
        var request = new UnityWebRequest(URL + $"/challenge/{challengeName}/challengeItem/{challengeItemItem}/student/{studentId}/activity/{activityId}", "PUT");

        var bodyJsonString = JsonUtility.ToJson(achievementItem);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

    private IEnumerator RestDeleteById(int id)
    {
        string URI = URL + "/" + id.ToString();
        UnityWebRequest request = UnityWebRequest.Delete(URI);

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        request.Dispose();
    }

}
