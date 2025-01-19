using UnityEngine;

public class Card3DScript : MonoBehaviour
{
    public MeshRenderer cardRenderer; // Renderer para cambiar materiales
    private int cardId; // ID de la carta
    private Material frontMaterial; // Material de la cara frontal
    private Material backMaterial; // Material de la cara trasera
    private MemoryGame3D gameController;
    private bool isMatched = false; // Si la carta ya está emparejada

    public void Setup(int id, Material frontMat, MemoryGame3D controller)
    {
        cardId = id;
        frontMaterial = frontMat;
        backMaterial = cardRenderer.material;
        gameController = controller;
    }

    public void OnCardClicked()
    {
        if (!isMatched)
        {
            FlipUp();
            gameController.OnCardSelected(gameObject);
        }
    }

    public void FlipUp()
    {
        cardRenderer.material = frontMaterial;
    }

    public void FlipDown()
    {
        cardRenderer.material = backMaterial;
    }

    public void SetMatched()
    {
        isMatched = true;
    }
}
