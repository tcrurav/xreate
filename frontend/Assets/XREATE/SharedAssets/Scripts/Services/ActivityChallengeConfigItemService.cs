using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ActivityChallengeConfigItemService : MonoBehaviour
{
    private string URL;

    public string requestError;
    public long responseCode;
    public ActivityChallengeConfigItem[] activityChallengeConfigItems;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/activityChallengeConfigItems";
    }

    public IEnumerator GetAllById(int id)
    {
        UpdateURL();
        yield return StartCoroutine(RestGetAllById(id));
    }

    private IEnumerator RestGetAllById(int configId)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/config/" + configId);

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

        Debug.Log("ActivityChallengeConfigItems returned successfully!");

        activityChallengeConfigItems = JsonHelper.getJsonArray<ActivityChallengeConfigItem>(result);

        request.Dispose();
    }


}
