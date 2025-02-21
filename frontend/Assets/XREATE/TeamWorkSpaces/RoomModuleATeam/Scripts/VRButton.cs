using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButton : MonoBehaviour
{
    public CyberSecurityPanel panel; // Referencia al panel que muestra los términos
    public bool isNextButton; // Si es true, avanza; si es false, retrocede

    private void Start()
    {
        // Verifica que el XRBaseInteractable está bien configurado en el cubo (botón)
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

    // Método que se ejecuta cuando se selecciona el botón en VR
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Botón pulsado: " + gameObject.name);
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
