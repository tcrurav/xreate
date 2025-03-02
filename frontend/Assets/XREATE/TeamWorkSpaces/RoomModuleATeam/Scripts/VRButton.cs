using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRButton : MonoBehaviour
{
    // Reference to the panel that displays the terms
    public CyberSecurityPanel panel; 
    
    // If true, advance; if false, go back
    public bool isNextButton; // If true, advance; if false, go back


    private void Start()
    {
        // Verify that the XRBaseInteractable is correctly configured in the bucket (button)
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("XRBaseInteractable not found in bucket!");
        }
    }

    // Method that runs when button is selected in VR
    public void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("Button pressed: " + gameObject.name);
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
