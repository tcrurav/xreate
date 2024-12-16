using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BicycleService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/bicycles";
    public void GetBicycles()
    {
        StartCoroutine(RestGetAll());
    }

    public void CreateBicycle(Bicycle bicycle)
    {
        StartCoroutine(RestCreate(bicycle));
    }

    public void UpdateBicycle(int id, Bicycle bicycle)
    {
        StartCoroutine(RestUpdate(id, bicycle));
    }

    public void DeleteBicycle(int id)
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

            var bicycles = JsonHelper.getJsonArray<Bicycle>(result);
            foreach (var b in bicycles)
            {
                Debug.Log(b.brand);
            }
        }

        request.Dispose();
    }

    IEnumerator RestCreate(Bicycle bicycle)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(bicycle);
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
            Debug.Log("Bicycle upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestUpdate(int id, Bicycle bicycle)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(bicycle);
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
            Debug.Log("Bicycle upload complete!");
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
            Debug.Log("Bicycle Deleted successfully!");
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
