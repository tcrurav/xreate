using UnityEngine;
using TMPro;

public class PlaneAdjuster : MonoBehaviour
{
    public TextMeshPro textMeshPro; // Referencia al componente TextMeshPro
    public Transform planeTransform; // Transform del Plane
    public float padding = 0.1f; // Espaciado adicional alrededor del texto

    private Transform cameraTransform; // C�mara del jugador para el Billboard

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asigna la c�mara principal por defecto
        }

        // Ajustar el tama�o inicial
        AdjustPlaneSize();
    }

    void LateUpdate()
    {
        // Asegurarse de que el Plane siga mirando al jugador
        if (cameraTransform != null)
        {
            transform.LookAt(cameraTransform);
            transform.Rotate(0, 180, 0); // Invertir para que no est� al rev�s
        }
    }

    public void AdjustPlaneSize()
    {
        if (textMeshPro == null || planeTransform == null)
        {
            Debug.LogError("TextMeshPro o PlaneTransform no asignado en PlaneAdjuster.");
            return;
        }

        // Obtener los l�mites renderizados del texto
        Bounds bounds = textMeshPro.bounds;

        // Ajustar el tama�o del Plane seg�n los l�mites del texto
        planeTransform.localScale = new Vector3(bounds.size.x + padding, 1, bounds.size.y + padding);

        // Reposicionar el Plane para que quede detr�s del texto
        planeTransform.localPosition = new Vector3(bounds.center.x, bounds.center.y, -0.01f); // Mover ligeramente hacia atr�s
    }
}
