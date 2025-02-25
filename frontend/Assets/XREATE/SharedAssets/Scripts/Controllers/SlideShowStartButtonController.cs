using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideShowStartButtonController : MonoBehaviour
{
    public Button EnableNextRoomButton;
    public Button[] ClickToGoButton;
    public TMP_Text[] RedText;
    public TMP_Text[] GreenText;
    public GameObject[] signs;

    public void EnableNextRooms()
    {
        EnableNextRoomButton.gameObject.SetActive(false);

        for (int i = 0; i < ClickToGoButton.Length; i++)
        {
            ClickToGoButton[i].gameObject.SetActive(true);
            RedText[i].gameObject.SetActive(false);
            GreenText[i].gameObject.SetActive(true);
        }

        DoOtherThingsDependingOnCurrentScene();
    }

    private void DoOtherThingsDependingOnCurrentScene()
    {
        switch (MainManager.GetScene())
        {
            case Scene.Main:
            case Scene.Auditorium:
                ActivateSigns();
                break;
        }
    }

    private void ActivateSigns()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            signs[i].gameObject.SetActive(true);
        }
    }
}
