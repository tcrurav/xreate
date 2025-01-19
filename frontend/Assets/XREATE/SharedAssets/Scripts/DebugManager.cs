using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;

    public TMP_Text debugText;
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
        Instance.debugText = debugText;
    }

    public static void Log(string message)
    {
        Instance.debugText.text += "\n" + message;
    }

    public static void Clear()
    {
        Instance.debugText.text = "";
    }

}
