using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActivityService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/activities";
    public void GetActivities()
    {
        StartCoroutine(RestGetAll());
    }

    public void CreateBicycle(Activity activity)
    {
        StartCoroutine(RestCreate(activity));
    }

    public void UpdateBicycle(int id, Activity activity)
    {
        StartCoroutine(RestUpdate(id, activity));
    }

    public void DeleteActivity(int id)
    {
        StartCoroutine(RestDelete(id));
    }

    IEnumerator RestGetAll()
    {
        Debug.Log(URL);
        UnityWebRequest request = UnityWebRequest.Get(URL);

        Debug.Log(MainManager.GetAccessToken());

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string result = request.downloadHandler.text;

            Debug.Log(result);

            var activities = JsonHelper.getJsonArray<Activity>(result);
            foreach (var a in activities)
            {
                Debug.Log(a.id);
            }
        }

        request.Dispose();
    }

    IEnumerator RestCreate(Activity activity)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(activity);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Activity upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestUpdate(int id, Activity activity)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(activity);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Activity upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestDelete(int id)
    {
        string URI = URL + "/" + id.ToString();
        UnityWebRequest request = UnityWebRequest.Delete(URI);

        request.SetRequestHeader("Authorization", "Bearer " + MainManager.GetAccessToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Activity Deleted successfully!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    // Calling to future Tibu's Me - Maybe helpfull for Image upload - To be done USING Unity documentation
    //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
    //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
    //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

    //UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myform", formData);
    //yield return www.SendWebRequest();

    //if (www.result != UnityWebRequest.Result.Success)
    //{
    //    Debug.Log(www.error);
    //}
    //else
    //{
    //    Debug.Log("Form upload complete!");
    //}
}
