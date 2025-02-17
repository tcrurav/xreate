using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButton : MonoBehaviour
{
    public CyberSecurityPanel panel; // Referencia al panel que muestra los t�rminos
    public bool isNextButton; // Si es true, avanza; si es false, retrocede

    private void Start()
    {
        // Obtener el componente XR Interactable
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        // Agregar la funci�n al evento de selecci�n
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    // M�todo que se ejecuta cuando se selecciona el bot�n en VR
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Si es el bot�n siguiente, avanza al siguiente t�rmino
        if (isNextButton)
            panel.OnNextButtonPressed();
        // Si es el bot�n anterior, retrocede al t�rmino anterior
        else
            panel.OnPreviousButtonPressed();
    }
}
