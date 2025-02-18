using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class InActivityStudentParticipationService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    // TODO - Result data should be returned other way than through this public members
    public InActivityStudentParticipation[] inActivityStudentParticipations;
    public InActivityStudentParticipationWithActivityAndPoints[] inActivityStudentParticipationsWithActivityAndPoints;
    public InActivityStudentParticipationWithPointsGroupedByTeams[] inActivityStudentParticipationWithPointsGroupedByTeams;
    public InActivityStudentParticipationTopPlayersWithPoints[] inActivityStudentParticipationTopPlayersWithPoints;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/inActivityStudentParticipations";
    }

    public IEnumerator GetAll()
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAll());
    }

    public IEnumerator GetAllWithActivityAndPoints(int studentId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllWithActivityAndPoints(studentId));
    }

    public IEnumerator GetAllWithPointsGroupedByTeams(int activityId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllWithPointsGroupedByTeams(activityId));
    }

    public IEnumerator GetAllTopPlayersWithPoints(int activityId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllTopPlayersWithPoints(activityId));
    }

    public IEnumerator GetAllByActivityId(int activityId)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllByActivityId(activityId));
    }

    public IEnumerator Create(InActivityStudentParticipation inActivityStudentParticipation)
    {
        UpdateURL();
        yield return StartCoroutine(RestCreate(inActivityStudentParticipation));
    }

    public IEnumerator UpdateById(int id, InActivityStudentParticipation inActivityStudentParticipation)
    {
        UpdateURL();
        yield return StartCoroutine(RestUpdateById(id, inActivityStudentParticipation));
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

        inActivityStudentParticipations = JsonHelper.getJsonArray<InActivityStudentParticipation>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllWithActivityAndPoints(int studentId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/learningPath/students/" + studentId.ToString());

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

        inActivityStudentParticipationsWithActivityAndPoints =
            JsonHelper.getJsonArray<InActivityStudentParticipationWithActivityAndPoints>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllTopPlayersWithPoints(int activityId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/points/teams/topPlayers/activities/" + activityId.ToString());

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

        inActivityStudentParticipationTopPlayersWithPoints =
            JsonHelper.getJsonArray<InActivityStudentParticipationTopPlayersWithPoints>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllWithPointsGroupedByTeams(int activityId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/points/teams/activities/" + activityId.ToString());

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

        inActivityStudentParticipationWithPointsGroupedByTeams =
            JsonHelper.getJsonArray<InActivityStudentParticipationWithPointsGroupedByTeams>(result);

        request.Dispose();
    }

    private IEnumerator RestGetAllByActivityId(int activityId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/activities/" + activityId.ToString());

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

        inActivityStudentParticipations =
            JsonHelper.getJsonArray<InActivityStudentParticipation>(result);

        request.Dispose();
    }

    private IEnumerator RestCreate(InActivityStudentParticipation inActivityStudentParticipation)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(inActivityStudentParticipation);
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

    private IEnumerator RestUpdateById(int id, InActivityStudentParticipation inActivityStudentParticipation)
    {
        var request = new UnityWebRequest(URL + "/" + id.ToString(), "PUT");

        var bodyJsonString = JsonUtility.ToJson(inActivityStudentParticipation);
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
