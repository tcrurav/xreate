using UnityEngine;

public class BillboardPanel : MonoBehaviour
{
    public Transform cameraTransform; // Referencia a la cámara del jugador
    public bool invertDirection = false; // Si necesitas invertir la dirección del texto

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Usa la cámara principal si no está configurada
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Gira el objeto para que mire hacia la cámara
            Vector3 direction = (cameraTransform.position - transform.position).normalized;

            // Invertir dirección si es necesario (por ejemplo, para que el texto no esté al revés)
            if (invertDirection)
            {
                direction = -direction;
            }

            // Mantener la rotación solo hacia la cámara
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0); // Solo rota en el eje Y
        }
        else
        {
            Debug.LogError("No se ha asignado una cámara al script BillboardText.");
        }
    }
}
