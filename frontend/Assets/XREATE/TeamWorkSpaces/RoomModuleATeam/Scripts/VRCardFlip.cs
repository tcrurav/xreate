using UnityEngine;

public class VRCardFlip : MonoBehaviour
{
    public GameObject FrontSide;  // Referencia a la cara frontal
    public GameObject BackSide;   // Referencia a la cara trasera
    public float flipSpeed = 2.0f;  // Velocidad del giro
    public AudioClip flipSound;   // Sonido al voltear la carta
    private AudioSource audioSource; // Componente para reproducir el sonido
    private bool isFlipped = false; // Estado de la carta (frontal o trasera)
    private bool isFlipping = false; // Evita que se inicien m�ltiples giros simult�neos
    private Quaternion initialRotation; // Almacena la rotaci�n inicial

    private void Start()
    {
        // Almacena la rotaci�n inicial de la carta
        initialRotation = transform.rotation;

        // Configuraci�n inicial
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        // Configurar el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = flipSound;
        audioSource.playOnAwake = false;
    }

    public void FlipCard()
    {
        // Evita que el giro ocurra si ya se est� girando
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

        // Iniciar temporizador para volver a girar despu�s de 5 segundos si es necesario
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

        // Animaci�n del giro
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar la rotaci�n final
        transform.rotation = endRotation;

        // Permitir nuevos giros
        isFlipping = false;
    }

    private System.Collections.IEnumerator AutoFlipBack()
    {
        // Espera 5 segundos antes de hacer el giro de vuelta
        yield return new WaitForSeconds(5f);

        // Restablecer la carta a la posici�n inicial
        isFlipped = false;

        // Reproducir sonido de volteo
        PlayFlipSound();

        // Alternar las caras antes del giro
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        // Iniciar animaci�n para volver a la rotaci�n inicial
        StartCoroutine(FlipAnimation());
    }
    private void PlayFlipSound()
    {
        // Verifica que el AudioSource y el clip est�n configurados
        if (audioSource != null && flipSound != null)
        {
            audioSource.Play();
        }
    }
}
