using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NotYetSignedUpTextController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            SceneManager.LoadScene("Signup");
        }
    }
}
