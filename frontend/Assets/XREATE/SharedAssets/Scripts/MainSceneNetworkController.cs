using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        MainNetworkManager.NetworkQuickJoinLoginUsingUnity6TemplateMenus();
    }
}
