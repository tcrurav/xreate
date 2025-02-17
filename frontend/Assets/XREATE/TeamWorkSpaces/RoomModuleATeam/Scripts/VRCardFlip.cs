using TMPro;
using UnityEngine;
using System.Collections;
using System.Data;

public class VRCardFlip : MonoBehaviour
{
    public GameObject FrontSide;
    public GameObject BackSide;
    public TextMeshPro backText;
    public GameManager gameManager;
    public string cardText;
    public AudioClip flipSound;
    private AudioSource audioSource;

    private bool isFlipped = false;
    private bool isFlipping = false;
    private bool isLocked = false;
    private Quaternion initialRotation;
    public float flipSpeed = 2.0f;

    public bool IsLocked => isLocked;

    private void Start()
    {
        initialRotation = transform.rotation;

        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = flipSound;
        audioSource.playOnAwake = false;
    }

    public void SetCardText(string text)
    {
        cardText = text;
        backText.text = text;
    }

    public void FlipCard()
    {
        if (isLocked || isFlipping || isFlipped) return;

        isFlipping = true;
        StartCoroutine(FlipAnimation());

       // gameManager.UpdateDisplayedTextVR(cardText);
    }

    public void ResetCard()
    {
        if (isLocked) return;

        isFlipped = false;
        isFlipping = false;
        transform.rotation = initialRotation;
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        //gameManager.UpdateDisplayedTextVR("");
    }

    public void LockCard()
    {
        isLocked = true;
    }

    private void PlayFlipSound()
    {
        if (audioSource != null && flipSound != null)
        {
            audioSource.Play();
        }
    }

    private IEnumerator FlipAnimation()
    {
        float elapsedTime = 0f;
        float duration = 1f / flipSpeed;

        Quaternion startRotation = transform.rotation;
        Quaternion midRotation = initialRotation * Quaternion.Euler(0, 0, 90);  //  Levanta la carta 90° sobre Z
        Quaternion endRotation = initialRotation * Quaternion.Euler(0, 0, 180); //  Voltea completamente 180° sobre Z

        PlayFlipSound();

        // Primera mitad del giro (levantando la carta)
        while (elapsedTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(startRotation, midRotation, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //  Asegurar que la carta esté en 90° y cambiar la cara visible
        transform.rotation = midRotation;
        FrontSide.SetActive(false);
        BackSide.SetActive(true);

        elapsedTime = 0f;

        // Segunda mitad del giro (termina de voltear)
        while (elapsedTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(midRotation, endRotation, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //  Asegurar que la rotación termine exactamente en 180° sobre Z
        transform.rotation = endRotation;

        isFlipped = true;
        isFlipping = false;

        gameManager.CardFlipped(this);
    }
}
