using UnityEngine;

public class BillboardPanel : MonoBehaviour
{
    public Transform cameraTransform; // Referencia a la c�mara del jugador
    public bool invertDirection = false; // Si necesitas invertir la direcci�n del texto

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Usa la c�mara principal si no est� configurada
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Gira el objeto para que mire hacia la c�mara
            Vector3 direction = (cameraTransform.position - transform.position).normalized;

            // Invertir direcci�n si es necesario (por ejemplo, para que el texto no est� al rev�s)
            if (invertDirection)
            {
                direction = -direction;
            }

            // Mantener la rotaci�n solo hacia la c�mara
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0); // Solo rota en el eje Y
        }
        else
        {
            Debug.LogError("No se ha asignado una c�mara al script BillboardText.");
        }
    }
}
