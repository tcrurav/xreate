using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private User user;
    private string scene = "LoginScene";
    private string accessToken;
    private string currentUrl = "https://8231ade2-f282-4550-af85-e7fedd0b338a.escritorios.ieselrincon.es";
    private readonly string[] URL = new string[] {
        "http://localhost:80",                                                          // Localhost
        "https://8231ade2-f282-4550-af85-e7fedd0b338a.escritorios.ieselrincon.es",      // Gran Canaria Server
        "" };                                                                           // TODO - Switzerland Server not available yet

    //TODO: URLs should be in .env maybe

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

    public static void SetScene(string scene)
    {
        Instance.scene = scene;
    }

    public static string GetAccessToken()
    {
        return Instance.accessToken;
    }

    public static User GetUser()
    {
        return Instance.user;
    }
    public static string GetScene()
    {
        return Instance.scene;
    }

    public static string GetURL()
    {
        return Instance.currentUrl;
    }

    public static void SetURL(int locationId)
    {
        Instance.currentUrl = Instance.URL[locationId];
    }

    //public static GameObject FindObject(GameObject parent, string name)
    //{
    //    Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
    //    foreach (Transform t in trs)
    //    {
    //        if (t.name == name)
    //        {
    //            return t.gameObject;
    //        }
    //    }
    //    return null;
    //}
    //// https://discussions.unity.com/t/how-to-find-an-inactive-game-object/129521
}

// https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#