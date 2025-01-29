using UnityEngine;

public class MainNavigationManager : MonoBehaviour
{
    public static MainNavigationManager Instance;

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

    public static void EnableSceneContainer(string container)
    {
        string containerParent = container + "Parent";
        GameObject containerParentGameObject = GameObject.FindGameObjectWithTag(containerParent);
        GameObject containerGameObject = FindObject.FindInsideParentByName(containerParentGameObject, container);
        containerGameObject.SetActive(true);
    }

    public static void DisableSceneContainer(string container)
    {
        GameObject containerGameObject = GameObject.FindGameObjectWithTag(container);
        containerGameObject.SetActive(false);
    }

}

// Data persistence between scenes has been done using a singleton class
// From: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#
