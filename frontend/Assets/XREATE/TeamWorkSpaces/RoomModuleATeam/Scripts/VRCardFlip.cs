using UnityEngine;

public class VRCardFlip : MonoBehaviour
{
    public GameObject FrontSide;  // Referencia a la cara frontal
    public GameObject BackSide;   // Referencia a la cara trasera
    public float flipSpeed = 2.0f;  // Velocidad del giro
    public AudioClip flipSound;   // Sonido al voltear la carta
    private AudioSource audioSource; // Componente para reproducir el sonido
    private bool isFlipped = false; // Estado de la carta (frontal o trasera)
    private bool isFlipping = false; // Evita que se inicien múltiples giros simultáneos
    private Quaternion initialRotation; // Almacena la rotación inicial

    private void Start()
    {
        // Almacena la rotación inicial de la carta
        initialRotation = transform.rotation;

        // Configuración inicial
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        // Configurar el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = flipSound;
        audioSource.playOnAwake = false;
    }

    public void FlipCard()
    {
        // Evita que el giro ocurra si ya se está girando
        if (isFlipping) return;

        isFlipping = true;

        // Alternar el estado de la carta
        isFlipped = !isFlipped;

        // Reproducir sonido de volteo
        PlayFlipSound();

        // Alternar las caras al inicio del giro
        FrontSide.SetActive(!isFlipped);
        BackSide.SetActive(isFlipped);

        // Iniciar el giro
        StartCoroutine(FlipAnimation());

        // Iniciar temporizador para volver a girar después de 5 segundos si es necesario
        if (isFlipped)
        {
            StartCoroutine(AutoFlipBack());
        }
    }

    private System.Collections.IEnumerator FlipAnimation()
    {
        float elapsedTime = 0f;
        float duration = 1f / flipSpeed;

        // Obtener las rotaciones inicial y final
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = initialRotation * Quaternion.Euler(isFlipped ? 180 : 0, 0, 0);

        // Animación del giro
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar la rotación final
        transform.rotation = endRotation;

        // Permitir nuevos giros
        isFlipping = false;
    }

    private System.Collections.IEnumerator AutoFlipBack()
    {
        // Espera 5 segundos antes de hacer el giro de vuelta
        yield return new WaitForSeconds(5f);

        // Restablecer la carta a la posición inicial
        isFlipped = false;

        // Reproducir sonido de volteo
        PlayFlipSound();

        // Alternar las caras antes del giro
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        // Iniciar animación para volver a la rotación inicial
        StartCoroutine(FlipAnimation());
    }
    private void PlayFlipSound()
    {
        // Verifica que el AudioSource y el clip estén configurados
        if (audioSource != null && flipSound != null)
        {
            audioSource.Play();
        }
    }
}
