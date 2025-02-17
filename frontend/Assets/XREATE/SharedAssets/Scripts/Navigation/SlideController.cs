using System;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    public GameObject[] Slides;
    public GameObject GotoNextSlideButton;
    public GameObject GotoPreviousSlideButton;

    //private int currentSlide = 0;
    private void Start()
    {
        //SlideShowNetworkManager.Instance.ChangeCurrentSlideServerRpc(0);
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
        if (SlideShowNetworkManager.Instance.currentSlide.Value >= (Slides.Length - 1)) return;

        HideCurrentSlide();
        SlideShowNetworkManager.Instance.ChangeCurrentSlideServerRpc(SlideShowNetworkManager.Instance.currentSlide.Value + 1);
        ShowCurrentSlide();
        ShowGotoPreviousSlideButton();
        if (SlideShowNetworkManager.Instance.currentSlide.Value >= (Slides.Length - 1)) HideGotoNextSlideButton();
    }

    public void GotoPreviousSlide()
    {
        if (SlideShowNetworkManager.Instance.currentSlide.Value <= 0) return;

        HideCurrentSlide();
        SlideShowNetworkManager.Instance.ChangeCurrentSlideServerRpc(SlideShowNetworkManager.Instance.currentSlide.Value - 1);
        ShowCurrentSlide();
        ShowGotoNextSlideButton();
        if (SlideShowNetworkManager.Instance.currentSlide.Value <= 0) HideGotoPreviousSlideButton();
    }

    private void ShowCurrentSlide()
    {
        Slides[SlideShowNetworkManager.Instance.currentSlide.Value].gameObject.SetActive(true);
    }

    private void HideCurrentSlide()
    {
        Slides[SlideShowNetworkManager.Instance.currentSlide.Value].gameObject.SetActive(false);
    }

    private void HideGotoNextSlideButton()
    {
        GotoNextSlideButton.SetActive(false);
    }
    private void HideGotoPreviousSlideButton()
    {
        GotoPreviousSlideButton.SetActive(false);
    }

    private void ShowGotoPreviousSlideButton()
    {
        GotoPreviousSlideButton.SetActive(true);
    }

    private void ShowGotoNextSlideButton()
    {
        GotoNextSlideButton.gameObject.SetActive(true);
    }
}
