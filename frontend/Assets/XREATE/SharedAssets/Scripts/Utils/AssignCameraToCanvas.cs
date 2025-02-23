using UnityEngine;

public class AssignCameraToCanvas : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
        {
            canvas.worldCamera = Camera.main; // Assign the main camera
        }
    }
}
