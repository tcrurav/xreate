using UnityEngine;
using UnityEngine.UI;

public class RoomModuleBGameController : MonoBehaviour
{
    public GameObject[] Slides;
    public GameObject GotoNextSlideButton;
    public GameObject GotoPreviousSlideButton;

    private SlideShowManager slideShowManager;

    private void Start()
    {
        slideShowManager = GetComponent<SlideShowManager>();
    }

    public void HideOldSlideAndShowNewSlide(int oldValue, int newValue)
    {
        DebugManager.Log($"SlideController - HideOldSlideAndShowNewSlide");

        Slides[oldValue].gameObject.SetActive(false);
        Slides[newValue].gameObject.SetActive(true);

        if (newValue >= (Slides.Length - 1))
        {
            GotoNextSlideButton.SetActive(false);
            GotoPreviousSlideButton.SetActive(true);
            return;
        }

        if (newValue <= 0)
        {
            GotoNextSlideButton.SetActive(true);
            GotoPreviousSlideButton.SetActive(false);
            return;
        }

        GotoNextSlideButton.SetActive(true);
        GotoPreviousSlideButton.SetActive(true);
    }

    public void GotoNextSlide()
    {
        if (MainManager.GetUser().role != "TEACHER") return; //Only teachers have permission to change slides

        if (slideShowManager.currentSlide.Value >= (Slides.Length - 1)) return;

        slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value + 1);
    }

    public void GotoPreviousSlide()
    {
        if (MainManager.GetUser().role != "TEACHER") return; //Only teachers have permission to change slides

        if (slideShowManager.currentSlide.Value <= 0) return;

        slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value - 1);
    }
}
