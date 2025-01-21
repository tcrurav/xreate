using UnityEngine;
using TMPro;

public class AutoSizeCollider : MonoBehaviour
{
    public BoxCollider boxCollider;
    public TextMeshPro textMeshPro;

    void Start()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (textMeshPro == null)
            textMeshPro = GetComponentInChildren<TextMeshPro>();

        UpdateColliderSize();
    }

    private void UpdateColliderSize()
    {
        if (textMeshPro == null || boxCollider == null)
            return;

        // Calcular los límites del texto renderizado
        var bounds = textMeshPro.bounds;

        // Ajustar el tamaño del BoxCollider según los límites del texto
        boxCollider.size = bounds.size;
        boxCollider.center = bounds.center;

        // Añadir un margen opcional para facilitar el agarre
        float margin = 0.1f;
        boxCollider.size += new Vector3(margin, margin, margin);
    }

}

