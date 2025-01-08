using TMPro;
using Unity.Netcode;
using UnityEngine;

public class MainNetworkManager : MonoBehaviour
{
    public static MainNetworkManager Instance;

    public UnityEngine.UI.Button ConfirmButton;
    public UnityEngine.UI.Button QuickJoinButton;

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

    void Start()
    {
        Instance.ConfirmButton = ConfirmButton;
        Instance.QuickJoinButton = QuickJoinButton;
    }

    public static void GetAllPlayers()
    {
        Debug.Log("GET ALL PLAYERS...............................");
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            // Access each player's client and identity information
            var playerObject = client.PlayerObject;
            // You can then check the playerObject or get components from it

            Debug.Log(playerObject.name);
        }
    }

    public static void NetworkQuickJoinLoginUsingUnity6TemplateMenus()
    {
        XRMultiplayer.XRINetworkGameManager.LocalPlayerName.Value = MainManager.GetUser().username;
        Instance.ConfirmButton.onClick.Invoke();
        Instance.QuickJoinButton.onClick.Invoke();
    }

    public static void HideAllStudentsOfOtherTeams()
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            Debug.Log("Only students are in a team. All other roles see all players");
            return;
        }

        int thisUserTeamId = CurrentActivityManager.GetTeamIdByStudentId(MainManager.GetUser().id);
        Debug.Log("thisUserTeamId:");
        Debug.Log(thisUserTeamId);

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            // playerObject will have the NetworkObject of the client
            NetworkObject playerObject = client.PlayerObject;
            int studentId = playerObject.GetComponent<PlayerIdSync>().PlayerId.Value;
            Debug.Log("studentId:");
            Debug.Log(studentId);
            int clientTeamId = CurrentActivityManager.GetTeamIdByStudentId(studentId);
            Debug.Log("clientTeamId:");
            Debug.Log(clientTeamId);

            if (clientTeamId != thisUserTeamId)
            {
                client.PlayerObject.enabled = false;
            }
        }
    }

    public static void ShowAllStudents()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {                
            client.PlayerObject.enabled = true;
        }
    }
}

