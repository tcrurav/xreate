using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ActivityChallengeConfigService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public ActivityChallengeConfig[] activityChallengeConfigs;
    public ActivityChallengeConfig[] activityChallengeConfigsByActivityId;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/activityChallengeConfigs";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllByActivityId(int activityId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllByActivityId(activityId));
    }

    public IEnumerator Create(ActivityChallengeConfig activityChallengeConfig)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(activityChallengeConfig));
    }

    public IEnumerator UpdateById(int id, ActivityChallengeConfig activityChallengeConfig)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, activityChallengeConfig));
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

        activityChallengeConfigs = JsonHelper.getJsonArray<ActivityChallengeConfig>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllByActivityId(int activityId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + $"/activity/{activityId}");

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

        activityChallengeConfigsByActivityId = JsonHelper.getJsonArray<ActivityChallengeConfig>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(ActivityChallengeConfig activityChallengeConfig)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(activityChallengeConfig);
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

    private IEnumerator RestUpdateById(int id, ActivityChallengeConfig activityChallengeConfig)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(activityChallengeConfig);
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
