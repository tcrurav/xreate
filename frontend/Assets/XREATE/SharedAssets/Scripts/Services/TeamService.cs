using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/teams";
    public void GetTeams()
    {
        StartCoroutine(RestGetAll());
    }

    public void CreateTeam(Team team)
    {
        StartCoroutine(RestCreate(team));
    }

    public void UpdateTeam(int id, Team team)
    {
        StartCoroutine(RestUpdate(id, team));
    }

    public void DeleteTeam(int id)
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

            var teams = JsonHelper.getJsonArray<Team>(result);
            foreach (var t in teams)
            {
                Debug.Log(t.name);
                Debug.Log(t.points);
            }
        }

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

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Team upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestUpdate(int id, Team team)
    {
        var request = new UnityWebRequest(URL + "/" + id, "PUT");

        var bodyJsonString = JsonUtility.ToJson(team);
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
            Debug.Log("Team upload complete!");
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
            Debug.Log("Team Deleted successfully!");
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
