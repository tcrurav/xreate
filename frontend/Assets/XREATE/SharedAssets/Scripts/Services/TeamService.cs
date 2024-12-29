using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/teams";

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public Team[] teams;
    public TeamWithPoints[] teamsWithPoints;

    public IEnumerator GetAll()
    {
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllWithPoints()
    {
        yield return StartCoroutine(RestGetAllWithPoints());
    }

    public IEnumerator Create(Team team)
    {
        yield return StartCoroutine(RestCreate(team));
    }

    public IEnumerator UpdateById(int id, Team team)
    {
        yield return StartCoroutine(RestUpdateById(id, team));
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

        Debug.Log("Teams data returned successfully!");

        teams = JsonHelper.getJsonArray<Team>(result);

        request.Dispose();
    }

    IEnumerator RestGetAllWithPoints()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/points");

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

        teamsWithPoints = JsonHelper.getJsonArray<TeamWithPoints>(result);

        request.Dispose();
    }

    IEnumerator RestCreate(Team team)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(team);
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

    IEnumerator RestUpdateById(int id, Team team)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(team);
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
