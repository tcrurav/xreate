using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    
    public bool TeacherPermissionsActivated;
    public string Team1Teacher;
    public string Team2Teacher;


    private User user;
    private Scene scene = Scene.Login;
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

    private void Start()
    {
        Instance.TeacherPermissionsActivated = TeacherPermissionsActivated;
        Instance.Team1Teacher = Team1Teacher;
        Instance.Team2Teacher = Team2Teacher;
    }

    public static bool IsTeacherPermissionsActivated()
    {
        return Instance.TeacherPermissionsActivated;
    }

    public static string GetTeam1Teacher()
    {
        return Instance.Team1Teacher;
    }

    public static string GetTeam2Teacher()
    {
        return Instance.Team2Teacher;
    }

    public static void SetAccessToken(string accessToken)
    {
        Instance.accessToken = accessToken;
    }

    public static void SetUser(User user)
    {
        Instance.user = user;
    }

    public static void SetScene(Scene scene)
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
    public static Scene GetScene()
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
}

// https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#