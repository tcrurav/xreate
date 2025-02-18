using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InActivityTeacherParticipationService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public InActivityTeacherParticipation[] inActivityTeacherParticipations;
    public InActivityTeacherParticipationWithActivityAndChallenge[] inActivityTeacherParticipationsWithActivityAndChallenge;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/inActivityTeacherParticipations";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllWithActivityAndChallenge(int teacherId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllWithActivityAndChallenge(teacherId));
    }

    public IEnumerator Create(InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(inActivityTeacherParticipation));
    }

    public IEnumerator UpdateById(int id, InActivityTeacherParticipation inActivityTeacherParticipation)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, inActivityTeacherParticipation));
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

        inActivityTeacherParticipations = JsonHelper.getJsonArray<InActivityTeacherParticipation>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllWithActivityAndChallenge(int teacherId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/teachers/" + teacherId.ToString());

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

        inActivityTeacherParticipationsWithActivityAndChallenge =
            JsonHelper.getJsonArray<InActivityTeacherParticipationWithActivityAndChallenge>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(InActivityTeacherParticipation inActivityTeacherParticipation)
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

    private IEnumerator RestUpdateById(int id, InActivityTeacherParticipation inActivityTeacherParticipation)
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
