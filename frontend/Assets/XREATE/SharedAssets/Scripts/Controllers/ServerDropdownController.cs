using TMPro;
using UnityEngine;

public class ServerDropdownController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Dropdown serverDropdown;

    public void ChangeURL()
    {
        MainManager.SetURL(serverDropdown.value);

        MainNetworkManager.GetAllPlayers();
    }
}
