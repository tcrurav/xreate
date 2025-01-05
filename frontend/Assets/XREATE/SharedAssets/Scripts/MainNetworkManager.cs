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
        // TODO - Reduce Menu scale to near 0 so that it's virtually as hiding the menu windows
        XRMultiplayer.XRINetworkGameManager.LocalPlayerName.Value = MainManager.GetUser().username;
        Instance.ConfirmButton.onClick.Invoke();
        Instance.QuickJoinButton.onClick.Invoke();
    }
}

