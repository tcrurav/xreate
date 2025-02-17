using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ChallengeService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public Challenge[] challenges;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/challenges";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator Create(Challenge challenge)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(challenge));
    }

    public IEnumerator UpdateById(int id, Challenge challenge)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, challenge));
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

        Debug.Log("Teams data returned successfully!");

        challenges = JsonHelper.getJsonArray<Challenge>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(Challenge challenge)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(challenge);
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

        request.Dispose();
    }

    private IEnumerator RestUpdateById(int id, Challenge challenge)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(challenge);
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

        request.Dispose();
    }

}
