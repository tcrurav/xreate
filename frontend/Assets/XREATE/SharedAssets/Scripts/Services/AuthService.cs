using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class AuthService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/users";

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    public IEnumerator Login(User user)
    {
        yield return StartCoroutine(RestLogin(user));
    }

    public IEnumerator Register(User user)
    {
        yield return StartCoroutine(RestRegister(user));
    }

    string GetBasicAuthString(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));

        return auth;
    }

    IEnumerator RestRegister(User user)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(user);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        string basicAuthString = GetBasicAuthString(user.username, user.password);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Basic " + basicAuthString);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        requestError = request.error;
        responseCode = request.responseCode;

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            request.Dispose();
            yield break;
        }

        string result = request.downloadHandler.text;
        Debug.Log(result);
        Debug.Log("Status Code: " + request.responseCode);

        if (request.responseCode != 200)
        {
            request.Dispose();
            yield break;
        }

        Debug.Log("User registered successfully!");

        UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

        MainManager.SetUser(user);
        MainManager.SetAccessToken(userWithAccessToken.access_token);

        request.Dispose();
    }

    IEnumerator RestLogin(User user)
    {
        var request = new UnityWebRequest(URL + "/signin", "POST");

        var bodyJsonString = JsonUtility.ToJson(user);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        string basicAuthString = GetBasicAuthString(user.username, user.password);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Basic " + basicAuthString);
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

        Debug.Log("User logged in successfully!");      

        UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

        MainManager.SetUser(user);
        MainManager.SetAccessToken(userWithAccessToken.access_token);
 
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
