using UnityEngine;

public class EnableSlideShowPanelController : MonoBehaviour
{
    public GameObject slidePanel;

    private void OnEnable()
    {
        slidePanel.SetActive(true);
    }

    private void OnDisable()
    {
        slidePanel.SetActive(false);
    }
}
