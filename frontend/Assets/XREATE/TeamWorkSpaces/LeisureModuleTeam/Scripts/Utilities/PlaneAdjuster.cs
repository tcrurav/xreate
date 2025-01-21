using UnityEngine;
using TMPro;

public class PlaneAdjuster : MonoBehaviour
{
    public TextMeshPro textMeshPro; // Referencia al componente TextMeshPro
    public Transform planeTransform; // Transform del Plane
    public float padding = 0.1f; // Espaciado adicional alrededor del texto

    private Transform cameraTransform; // Cámara del jugador para el Billboard

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asigna la cámara principal por defecto
        }

        // Ajustar el tamaño inicial
        AdjustPlaneSize();
    }

    void LateUpdate()
    {
        // Asegurarse de que el Plane siga mirando al jugador
        if (cameraTransform != null)
        {
            transform.LookAt(cameraTransform);
            transform.Rotate(0, 180, 0); // Invertir para que no esté al revés
        }
    }

    public void AdjustPlaneSize()
    {
        if (textMeshPro == null || planeTransform == null)
        {
            Debug.LogError("TextMeshPro o PlaneTransform no asignado en PlaneAdjuster.");
            return;
        }

        // Obtener los límites renderizados del texto
        Bounds bounds = textMeshPro.bounds;

        // Ajustar el tamaño del Plane según los límites del texto
        planeTransform.localScale = new Vector3(bounds.size.x + padding, 1, bounds.size.y + padding);

        // Reposicionar el Plane para que quede detrás del texto
        planeTransform.localPosition = new Vector3(bounds.center.x, bounds.center.y, -0.01f); // Mover ligeramente hacia atrás
    }
}
