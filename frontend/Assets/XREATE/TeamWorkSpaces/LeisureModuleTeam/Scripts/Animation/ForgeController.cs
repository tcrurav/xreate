using UnityEngine;

public class ForgeController : MonoBehaviour
{
    public Animator animator; // Asigna el Animator aquí
    private bool isOpening = false; // True si está abriendo, false si está cerrando

    public void ToggleForge()
    {
        if (isOpening)
        {
            CloseForge();
        }
        else
        {
            OpenForge();
        }
    }

    public void OpenForge()
    {
        animator.ResetTrigger("Close"); // Resetea cualquier trigger previo
        animator.SetTrigger("Open"); // Activa el trigger para abrir
        isOpening = true;
    }

    public void CloseForge()
    {
        animator.ResetTrigger("Open"); // Resetea cualquier trigger previo
        animator.SetTrigger("Close"); // Activa el trigger para cerrar
        isOpening = false;
    }
}
