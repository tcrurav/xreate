using TMPro;
using UnityEngine;

public class XcapeVersionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI versionText = GetComponent<TextMeshProUGUI>();

        // Display the project version from Player Settings
        versionText.text = "Xcape v." + Application.version;
    }
}
