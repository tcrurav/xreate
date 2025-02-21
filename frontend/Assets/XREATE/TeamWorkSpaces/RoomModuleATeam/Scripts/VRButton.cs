using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButton : MonoBehaviour
{
    public CyberSecurityPanel panel; // Referencia al panel que muestra los t�rminos
    public bool isNextButton; // Si es true, avanza; si es false, retrocede

    private void Start()
    {
        // Verifica que el XRBaseInteractable est� bien configurado en el cubo (bot�n)
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("XRBaseInteractable no encontrado en el cubo!");
        }
    }

    // M�todo que se ejecuta cuando se selecciona el bot�n en VR
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Bot�n pulsado: " + gameObject.name);
        if (isNextButton)
        {
            panel.OnNextButtonPressed();
        }
        else
        {
            panel.OnPreviousButtonPressed();
        }

    }
}
