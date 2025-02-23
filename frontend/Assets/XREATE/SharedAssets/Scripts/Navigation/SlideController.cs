using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public GameObject[] Slides;
    public GameObject GotoNextSlideButton;
    public GameObject GotoPreviousSlideButton;

    private SlideShowManager slideShowManager;

    private void Start()
    {
        slideShowManager = GetComponent<SlideShowManager>();

        if (MainManager.GetUser().role != "TEACHER")
        {
            // Only teachers can show the slide show
            Debug.Log("SlideController - Only a Teacher can enable next Room");
            GotoNextSlideButton.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void HideOldSlideAndShowNewSlide(int oldValue, int newValue)
    {
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
        if (slideShowManager.currentSlide.Value >= (Slides.Length - 1)) return;

        slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value + 1);
    }

    public void GotoPreviousSlide()
    {
        if (slideShowManager.currentSlide.Value <= 0) return;

        slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value - 1);
    }

    // TODO - ALL the code below can be removed if network working

    //public void GotoNextSlide()
    //{
    //    if (MainManager.GetUser().role != "TEACHER") return; // Only teachers can show the slide show

    //    if (slideShowManager.currentSlide.Value >= (Slides.Length - 1)) return;

    //    HideCurrentSlide();
    //    slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value + 1);
    //    ShowCurrentSlide();
    //    ShowGotoPreviousSlideButton();
    //    if (slideShowManager.currentSlide.Value >= (Slides.Length - 1)) HideGotoNextSlideButton();
    //}

    //public void GotoPreviousSlide()
    //{
    //    if (MainManager.GetUser().role != "TEACHER") return; // Only teachers can show the slide show

    //    if (slideShowManager.currentSlide.Value <= 0) return;

    //    HideCurrentSlide();
    //    slideShowManager.ChangeCurrentSlideServerRpc(slideShowManager.currentSlide.Value - 1);
    //    ShowCurrentSlide();
    //    ShowGotoNextSlideButton();
    //    if (slideShowManager.currentSlide.Value <= 0) HideGotoPreviousSlideButton();
    //}

    //private void ShowCurrentSlide()
    //{
    //    Slides[slideShowManager.currentSlide.Value].gameObject.SetActive(true);
    //}

    //private void HideCurrentSlide()
    //{
    //    Slides[slideShowManager.currentSlide.Value].gameObject.SetActive(false);
    //}

    //private void HideGotoNextSlideButton()
    //{
    //    GotoNextSlideButton.SetActive(false);
    //}
    //private void HideGotoPreviousSlideButton()
    //{
    //    GotoPreviousSlideButton.SetActive(false);
    //}

    //private void ShowGotoPreviousSlideButton()
    //{
    //    GotoPreviousSlideButton.SetActive(true);
    //}

    //private void ShowGotoNextSlideButton()
    //{
    //    GotoNextSlideButton.gameObject.SetActive(true);
    //}
}
