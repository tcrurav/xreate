using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButton : MonoBehaviour
{
    public CyberSecurityPanel panel; // Referencia al panel que muestra los términos
    public bool isNextButton; // Si es true, avanza; si es false, retrocede

    private void Start()
    {
        // Obtener el componente XR Interactable
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        // Agregar la función al evento de selección
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    // Método que se ejecuta cuando se selecciona el botón en VR
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Si es el botón siguiente, avanza al siguiente término
        if (isNextButton)
            panel.OnNextButtonPressed();
        // Si es el botón anterior, retrocede al término anterior
        else
            panel.OnPreviousButtonPressed();
    }
}
