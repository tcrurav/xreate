using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private User user;
    private string accessToken;
    private string currentUrl = "http://localhost:8080";
    private readonly string[] URL = new string[] { "http://localhost:8080", "", "" };

    //TODO: URLs should be in .env

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void SetAccessToken(string accessToken)
    {
        Instance.accessToken = accessToken;
    }

    public static void SetUser(User user)
    {
        Instance.user = user;
    }

    public static string GetAccessToken()
    {
        return Instance.accessToken;
    }

    public static User GetUser()
    {
        return Instance.user;
    }

    public static string GetURL()
    {
        return Instance.currentUrl;
    }

    public static void SetURL(int locationId)
    {
        Instance.currentUrl = Instance.URL[locationId];
        Debug.Log(Instance.currentUrl);
    }

    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
    // https://discussions.unity.com/t/how-to-find-an-inactive-game-object/129521
}

// https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#
