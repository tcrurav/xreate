using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{
    // Referencias a componentes
    private TextMeshPro textMeshPro; // Referencia al TextMeshPro del prefab
    private BoxCollider boxCollider;

    // Estados
    private string wordText; // Texto de la palabra
    private bool isGrabbed = false; // Indica si la palabra est� siendo agarrada

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
            Debug.LogWarning("Aviso: Falta el componente TextMeshPro en Word. Esto puede afectar la visualizaci�n del texto.");
        }

        if (boxCollider == null)
        {
            Debug.LogWarning("Aviso: Falta el componente BoxCollider en Word. Esto puede afectar la funcionalidad de colisiones o arrastre.");
        }
    }




    void Start()
    {
        // Ajustar el tama�o del collider al texto inicial
        UpdateColliderSize();
    }

    // M�todo para establecer la palabra y convertir el texto a vertical
    public void SetWord(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("El texto proporcionado a SetWord est� vac�o o es nulo.");
            return;
        }

        wordText = text;
        // Transformar el texto a vertical
        string verticalText = string.Join("\n", wordText.ToCharArray());
        if (textMeshPro != null)
        {
            textMeshPro.text = verticalText; // Asignar el texto vertical
            textMeshPro.ForceMeshUpdate(); // Forzar la actualizaci�n del renderizado
            Debug.Log($"Texto actualizado: {verticalText}");
        }
        else
        {
            Debug.LogError("TextMeshPro no est� asignado en el prefab.");
        }

        // Actualizar el tama�o del collider seg�n el nuevo texto
        UpdateColliderSize();

        // Ajustar el tama�o y posici�n del Plane
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

    // Actualizar el tama�o del collider basado en el texto renderizado
    private void UpdateColliderSize()
    {
        if (textMeshPro == null || boxCollider == null)
            return;

        textMeshPro.ForceMeshUpdate(); // Asegura que los l�mites del texto sean precisos
        var bounds = textMeshPro.textBounds; // Usar los l�mites del texto
        boxCollider.size = bounds.size;
        boxCollider.center = bounds.center;
    }

    // Ajustar el tama�o del Plane seg�n el tama�o del texto
    private void UpdatePlaneSize()
    {
        if (textMeshPro == null || planeTransform == null)
            return;

        textMeshPro.ForceMeshUpdate(); // Asegura que los l�mites sean precisos
        var bounds = textMeshPro.textBounds;

        // Ajustar el tama�o del Plane seg�n el tama�o del texto
        planeTransform.localScale = new Vector3(bounds.size.x, 1, bounds.size.y);
    }

    // Actualizar visuales seg�n el estado
    private void UpdateVisuals()
    {
        var renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = isGrabbed ? grabbedMaterial : defaultMaterial;
        }
    }
}
