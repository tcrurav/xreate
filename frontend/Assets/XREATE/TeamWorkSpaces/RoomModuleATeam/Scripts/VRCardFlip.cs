using TMPro;
using UnityEngine;

public class VRCardFlip : MonoBehaviour
{
    public GameObject FrontSide;  // Referencia a la cara frontal
    public GameObject BackSide;   // Referencia a la cara trasera
    public TextMeshPro backText;  // Referencia al texto en la cara trasera
    public GameManager gameManager; // Referencia al GameManager
    public string cardText;       // Texto asociado a la carta (clave o valor)
    public AudioClip flipSound;   // Sonido al voltear la carta
    private AudioSource audioSource; // Componente para reproducir el sonido

    private bool isFlipped = false; // Estado de la carta (frontal o trasera)
    private bool isFlipping = false; // Controla si la carta está girando
    private bool isLocked = false;   // Indica si la carta está bloqueada
    private Quaternion initialRotation; // Rotación inicial de la carta
    public float flipSpeed = 2.0f;   // Velocidad de volteo de la carta

    // Propiedad para verificar si la carta está bloqueada
    public bool IsLocked => isLocked;

    private void Start()
    {
        // Almacena la rotación inicial de la carta
        initialRotation = transform.rotation;

        // Configuración inicial de las caras
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        // Configurar el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = flipSound;
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Configura el texto de la carta.
    /// </summary>
    public void SetCardText(string text)
    {
        cardText = text;        // Asigna el texto de la carta (clave o valor)
        backText.text = text;   // Asigna el mismo texto al Back Text (lo que se ve al voltear la carta)
    }

    /// <summary>
    /// Voltea la carta.
    /// </summary>
    public void FlipCard()
    {
        // Evitar giros si ya está bloqueada, girando o volteada
        if (isLocked || isFlipping || isFlipped) return;

        isFlipping = true; // Evitar múltiples giros

        // Alternar el estado de volteo
        isFlipped = true;

        // Reproducir sonido de volteo
        PlayFlipSound();

        // Alternar las caras de la carta
        FrontSide.SetActive(false);
        BackSide.SetActive(true);

        // Notificar al GameManager que esta carta ha sido volteada
        gameManager.CardFlipped(this);

        // Iniciar la animación del giro
        StartCoroutine(FlipAnimation());
    }

    /// <summary>
    /// Restaura la carta a su estado inicial.
    /// </summary>
    public void ResetCard()
    {
        if (isLocked) return;

        isFlipped = false;
        FrontSide.SetActive(true);
        BackSide.SetActive(false);
    }

    /// <summary>
    /// Bloquea la carta para que permanezca volteada.
    /// </summary>
    public void LockCard()
    {
        isLocked = true;
    }

    /// <summary>
    /// Reproduce el sonido de volteo de la carta.
    /// </summary>
    private void PlayFlipSound()
    {
        if (audioSource != null && flipSound != null)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Animación de volteo de la carta.
    /// </summary>
    private System.Collections.IEnumerator FlipAnimation()
    {
        float elapsedTime = 0f;
        float duration = 1f / flipSpeed;

        // Configurar las rotaciones inicial y final
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = initialRotation * Quaternion.Euler(0, isFlipped ? 180 : 0, 0);

        // Interpolar la rotación de la carta
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que la carta termine en la rotación exacta
        transform.rotation = endRotation;

        isFlipping = false; // Permitir nuevos giros
    }

}
