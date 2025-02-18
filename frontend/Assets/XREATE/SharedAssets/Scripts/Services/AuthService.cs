using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthService : MonoBehaviour
{
    private string URL;

    // TODO - Error handling should be handled other way than through this public members
    public string requestError;
    public long responseCode;

    private void UpdateURL()
    {
        URL = MainManager.GetURL() + "/api/users";
    }

    public IEnumerator Login(User user)
    {
        UpdateURL();
        yield return StartCoroutine(RestLogin(user));
    }

    public IEnumerator Register(User user)
    {
        UpdateURL();
        yield return StartCoroutine(RestRegister(user));
    }

    private string GetBasicAuthString(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));

        return auth;
    }

    private IEnumerator RestRegister(User user)
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

        UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

        MainManager.SetUser(user);
        MainManager.SetAccessToken(userWithAccessToken.access_token);

        request.Dispose();
    }

    private IEnumerator RestLogin(User user)
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

        UserWithAccessToken userWithAccessToken = JsonUtility.FromJson<UserWithAccessToken>(result);

        MainManager.SetUser(userWithAccessToken.user);
        MainManager.SetAccessToken(userWithAccessToken.access_token);

        request.Dispose();
    }

}
