using UnityEngine;

public class WordSnapToCamera : MonoBehaviour
{
    public Transform cameraTransform; // Referencia a la cámara
    public float snapThreshold = 45f; // Ángulo mínimo para cambiar de dirección
    public float rotationSpeed = 5f; // Velocidad de rotación suave

    private Quaternion targetRotation; // Rotación objetivo

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Detectar automáticamente la cámara
        }

        UpdateTargetRotation(); // Calcular rotación inicial
    }

    void Update()
    {
        // Aplicar rotación suave SOLO en el eje Y
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Verificar si es necesario actualizar la rotación
        if (ShouldSnapRotation())
        {
            UpdateTargetRotation();
        }
    }

    // Comprueba si la palabra debe rotarse a la nueva dirección
    private bool ShouldSnapRotation()
    {
        Vector3 toCamera = cameraTransform.position - transform.position;
        toCamera.y = 0; // Ignorar altura para solo rotar en el plano horizontal

        float angle = Vector3.Angle(transform.forward, toCamera);

        return angle > snapThreshold;
    }

    // Ajusta la rotación objetivo en pasos de 90° SOLO en el eje Y
    private void UpdateTargetRotation()
    {
        Vector3 toCamera = cameraTransform.position - transform.position;
        toCamera.y = 0; // Mantener solo la rotación horizontal

        float angle = Mathf.Atan2(toCamera.x, toCamera.z) * Mathf.Rad2Deg; // Obtener ángulo en el plano XZ
        float snappedAngle = Mathf.Round(angle / 90f) * 90f; // Redondear a múltiplos de 90°

        //targetRotation = Quaternion.Euler(0, snappedAngle, 0); // Solo girar en Y
        targetRotation = Quaternion.Euler(0, snappedAngle + 180f, 0); // 🔹 SUMAMOS 180° PARA INVERTIR
    }
}