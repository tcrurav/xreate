using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class MainNavigationManager : MonoBehaviour
{
    public static MainNavigationManager Instance;
    public GameObject XRInteractionSetupMPVariant;
    private TeleportationProvider m_TeleportationProvider;

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

    public void Start()
    {
        Instance.m_TeleportationProvider = m_TeleportationProvider;
        Instance.XRInteractionSetupMPVariant = XRInteractionSetupMPVariant;
        m_TeleportationProvider = Instance.XRInteractionSetupMPVariant.GetComponentInChildren<TeleportationProvider>();
    }

    public static void ChangePosition(Vector3 position)
    {
        TeleportRequest teleportRequest = new()
        {
            destinationPosition = position,
            destinationRotation = Quaternion.identity
        };

        if (!Instance.m_TeleportationProvider.QueueTeleportRequest(teleportRequest))
        {
            Debug.LogWarning("Failed to queue teleport request");
        }
    }

    public static void EnableSceneContainer(string container)
    {
        string containerParent = container + "Parent";
        Debug.Log($"container: {container}");
        GameObject containerParentGameObject = GameObject.FindGameObjectWithTag(containerParent);
        GameObject containerGameObject = FindObject.FindInsideParentByName(containerParentGameObject, container);
        containerGameObject.SetActive(true);
    }

    public static void DisableSceneContainer(string container)
    {
        try
        {
            GameObject containerGameObject = GameObject.FindGameObjectWithTag(container);
            containerGameObject.SetActive(false);
        }
        catch
        {
            Debug.Log($"Scene {container} not found");
        }


    }

    public static IEnumerator WaitForPlayerObjectAndThenChangeSceneForLocalNetworkPlayer(Scene scene)
    {
        // TODO - Maybe this should be for a certain number of seconds only
        while (NetworkManager.Singleton.LocalClient == null || NetworkManager.Singleton.LocalClient.PlayerObject == null)
        {
            yield return null;
        }

        ChangeSceneForLocalNetworkPlayer(scene);
    }

    private static void ChangeSceneForLocalNetworkPlayer(Scene scene)
    {

        if (NetworkManager.Singleton.LocalClient != null)
        {
            PlayerSync player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerSync>();
            if (player != null)
            {

                if (!player.IsSpawned)
                {
                    Debug.Log($"ChangeSceneForLocalNetworkPlayer !IsSpawned - scene: {scene}");
                    DebugManager.Log($"ChangeSceneForLocalNetworkPlayer !IsSpawned - scene: {scene}");
                }

                player.SetPlayerScene(scene);
            }
        }
    }

}

// Data persistence between scenes has been done using a singleton class
// From: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#
