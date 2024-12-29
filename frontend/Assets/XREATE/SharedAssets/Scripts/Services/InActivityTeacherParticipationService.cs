using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InActivityTeacherParticipationService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/inActivityTeacherParticipations";

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public InActivityTeacherParticipation[] inActivityTeacherParticipations;
    public InActivityTeacherParticipationWithActivityAndChallenge[] inActivityTeacherParticipationsWithActivityAndChallenge;

    public IEnumerator GetAll()
    {
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllWithActivityAndChallenge(int teacherId)
    {
        yield return StartCoroutine(RestGetAllWithActivityAndChallenge(teacherId));
    }

    public IEnumerator Create(InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        yield return StartCoroutine(RestCreate(inActivityTeacherParticipation));
    }

    public IEnumerator UpdateById(int id, InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        yield return StartCoroutine(RestUpdateById(id, inActivityTeacherParticipation));
    }

    public IEnumerator DeleteById(int id)
    {
        yield return StartCoroutine(RestDeleteById(id));
    }

    IEnumerator RestGetAll()
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

        Debug.Log("InActivityTeacherParticipations data returned successfully!");

        inActivityTeacherParticipations = JsonHelper.getJsonArray<InActivityTeacherParticipation>(result);

        request.Dispose();
    }

    IEnumerator RestGetAllWithActivityAndChallenge(int teacherId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/teachers/" + teacherId.ToString());

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

        Debug.Log("InActivityTeacherParticipations with Activity and Challenge data returned successfully!");

        inActivityTeacherParticipationsWithActivityAndChallenge =
            JsonHelper.getJsonArray<InActivityTeacherParticipationWithActivityAndChallenge>(result);

        foreach (var i in inActivityTeacherParticipationsWithActivityAndChallenge)
        {
            Debug.Log(i.order);
            Debug.Log(i.activityName);
        }

        request.Dispose();
    }

    IEnumerator RestCreate(InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(inActivityTeacherParticipation);
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

    IEnumerator RestUpdateById(int id, InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        var request = new UnityWebRequest(URL + "/" + id.ToString(), "PUT");

        var bodyJsonString = JsonUtility.ToJson(inActivityTeacherParticipation);
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

    IEnumerator RestDeleteById(int id)
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
