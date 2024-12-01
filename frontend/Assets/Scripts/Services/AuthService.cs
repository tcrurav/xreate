using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthService : MonoBehaviour
{
    private readonly string URL = MainManager.GetURL() + "/api/users";

    public void Login(User user)
    {
        StartCoroutine(RestLogin(user));
    }

    public void Register(User user)
    {
        StartCoroutine(RestRegister(user));
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

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string result = request.downloadHandler.text;
            Debug.Log("User registered successfully!");
            Debug.Log(result);

            UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

            MainManager.SetUser(user);
            MainManager.SetAccessToken(userWithAccessToken.access_token);

            SceneManager.LoadScene("Login");
        }

        Debug.Log("Status Code: " + request.responseCode);

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

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string result = request.downloadHandler.text;
            Debug.Log("User logged in successfully!");
            Debug.Log(result);

            UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

            MainManager.SetUser(user);
            MainManager.SetAccessToken(userWithAccessToken.access_token);

            SceneManager.LoadScene("SpaceshipScene");
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
