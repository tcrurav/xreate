using UnityEngine;

public class EnableSlideShowPanelController : MonoBehaviour
{
    public GameObject slidePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        slidePanel.SetActive(true);
    }

    private void OnDisable()
    {
        slidePanel.SetActive(false);
    }
}
