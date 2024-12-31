using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActivityService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public Activity[] activities;
    public Activity[] activitiesNonExpired;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/activities";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllNonExpired()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllNonExpired());
    }

    public IEnumerator Create(Activity activity)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(activity));
    }

    public IEnumerator UpdateById(int id, Activity activity)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, activity));
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
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        Debug.Log("Activities data returned successfully!");

        activities = JsonHelper.getJsonArray<Activity>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllNonExpired()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/nonExpired");

        Debug.Log(MainManager.GetAccessToken());

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        Debug.Log("Teams with points data returned successfully!");

        activitiesNonExpired = JsonHelper.getJsonArray<Activity>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(Activity activity)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(activity);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        // TODO - It has to be tested. If Ok then delete following comments bellow

        //if (request.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(request.error);
        //    request.Dispose();
        //    yield break;
        //}
        //else
        //{
        //    Debug.Log("Team upload complete!");
        //}

        //Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    private IEnumerator RestUpdateById(int id, Activity activity)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(activity);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        // TODO - It has to be tested. If Ok then delete following comments bellow

        //if (request.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.Log(request.error);
        //}
        //else
        //{
        //    Debug.Log("Team upload complete!");
        //}

        //Debug.Log("Status Code: " + request.responseCode);

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
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        // TODO - It has to be tested. If Ok then delete following comments bellow

        //if (request.result == UnityWebRequest.Result.ConnectionError)
        //{
        //    Debug.Log(request.error);
        //}
        //else
        //{
        //    Debug.Log("Team Deleted successfully!");
        //}

        //Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

}
