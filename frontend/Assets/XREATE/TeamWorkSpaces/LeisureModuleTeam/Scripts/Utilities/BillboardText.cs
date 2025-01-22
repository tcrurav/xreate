using UnityEngine;

public class BillboardText : MonoBehaviour
{
    public Transform cameraTransform; // La cámara del jugador
    public float minDistance = 2f; // Distancia mínima para ajustar el texto
    public float maxDistance = 50f; // Distancia máxima para ajustar el texto

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asigna la cámara principal si no está configurada
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Calcula la distancia entre el texto y la cámara
            float distance = Vector3.Distance(transform.position, cameraTransform.position);

            // Solo ajusta la orientación si la distancia está dentro del rango
            if (distance >= minDistance && distance <= maxDistance)
            {
                transform.LookAt(cameraTransform);
                transform.Rotate(0, 180, 0); // Asegúrate de que el texto no esté invertido
            }
        }
        else
        {
            Debug.LogError("La cámara no está asignada al BillboardText.");
        }
    }
}
