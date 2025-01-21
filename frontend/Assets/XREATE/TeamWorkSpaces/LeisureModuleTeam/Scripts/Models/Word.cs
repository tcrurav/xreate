using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{
    // Referencias a componentes
    private TextMeshPro textMeshPro; // Referencia al TextMeshPro del prefab
    private BoxCollider boxCollider;

    // Estados
    private string wordText; // Texto de la palabra
    private bool isGrabbed = false; // Indica si la palabra está siendo agarrada

    // Material para cambios visuales
    public Material defaultMaterial;
    public Material grabbedMaterial;

    public Transform planeTransform; // Asigna el Plane en el inspector

    void Awake()
    {
        // Obtener referencias a los componentes
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        boxCollider = GetComponent<BoxCollider>();

        if (textMeshPro == null)
        {
            Debug.LogWarning("Aviso: Falta el componente TextMeshPro en Word. Esto puede afectar la visualización del texto.");
        }

        if (boxCollider == null)
        {
            Debug.LogWarning("Aviso: Falta el componente BoxCollider en Word. Esto puede afectar la funcionalidad de colisiones o arrastre.");
        }
    }




    void Start()
    {
        // Ajustar el tamaño del collider al texto inicial
        UpdateColliderSize();
    }

    // Método para establecer la palabra y convertir el texto a vertical
    public void SetWord(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("El texto proporcionado a SetWord está vacío o es nulo.");
            return;
        }

        wordText = text;
        // Transformar el texto a vertical
        string verticalText = string.Join("\n", wordText.ToCharArray());
        if (textMeshPro != null)
        {
            textMeshPro.text = verticalText; // Asignar el texto vertical
            textMeshPro.ForceMeshUpdate(); // Forzar la actualización del renderizado
            Debug.Log($"Texto actualizado: {verticalText}");
        }
        else
        {
            Debug.LogError("TextMeshPro no está asignado en el prefab.");
        }

        // Actualizar el tamaño del collider según el nuevo texto
        UpdateColliderSize();

        // Ajustar el tamaño y posición del Plane
        if (planeTransform != null)
        {
            UpdatePlaneSize();
        }
    }

    // Cambiar visuales al ser agarrado
    public void OnGrabbed()
    {
        isGrabbed = true;
        UpdateVisuals();
    }

    // Cambiar visuales al ser soltado
    public void OnReleased()
    {
        isGrabbed = false;
        UpdateVisuals();
    }

    // Actualizar el tamaño del collider basado en el texto renderizado
    private void UpdateColliderSize()
    {
        if (textMeshPro == null || boxCollider == null)
            return;

        textMeshPro.ForceMeshUpdate(); // Asegura que los límites del texto sean precisos
        var bounds = textMeshPro.textBounds; // Usar los límites del texto
        boxCollider.size = bounds.size;
        boxCollider.center = bounds.center;
    }

    // Ajustar el tamaño del Plane según el tamaño del texto
    private void UpdatePlaneSize()
    {
        if (textMeshPro == null || planeTransform == null)
            return;

        textMeshPro.ForceMeshUpdate(); // Asegura que los límites sean precisos
        var bounds = textMeshPro.textBounds;

        // Ajustar el tamaño del Plane según el tamaño del texto
        planeTransform.localScale = new Vector3(bounds.size.x, 1, bounds.size.y);
    }

    // Actualizar visuales según el estado
    private void UpdateVisuals()
    {
        var renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = isGrabbed ? grabbedMaterial : defaultMaterial;
        }
    }
}
