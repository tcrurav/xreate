using TMPro;
using UnityEngine;
using System.Collections;

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
        if (isLocked || isFlipping || isFlipped || !gameManager.CanFlip()) return;

        isFlipping = true;
        // Notify that an animation is occurring
        gameManager.SetFlippingState(true);
        StartCoroutine(FlipAnimation());

        gameManager.UpdateDisplayedTextVR(cardText);
    }

    public void ResetCard()
    {
        if (isLocked) return;

        isFlipped = false;
        isFlipping = false;
        transform.rotation = initialRotation;
        FrontSide.SetActive(true);
        BackSide.SetActive(false);

        gameManager.UpdateDisplayedTextVR("");
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

        //  Raise the card 90° above Z
        Quaternion midRotation = initialRotation * Quaternion.Euler(0, 0, 90);

        //  Flips completely 180° about Z
        Quaternion endRotation = initialRotation * Quaternion.Euler(0, 0, 180); 

        PlayFlipSound();

        // First half of the turn (raising the card)
        while (elapsedTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(startRotation, midRotation, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure the card is at 90° and change the visible side
        transform.rotation = midRotation;
        FrontSide.SetActive(false);
        BackSide.SetActive(true);

        elapsedTime = 0f;

        // Second half of the turn (finish turning)
        while (elapsedTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(midRotation, endRotation, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the rotation ends at exactly 180° about Z


        transform.rotation = endRotation;

        isFlipped = true;
        isFlipping = false;

        // Animation finished
        gameManager.SetFlippingState(false); 
        gameManager.CardFlipped(this);
    }
}
