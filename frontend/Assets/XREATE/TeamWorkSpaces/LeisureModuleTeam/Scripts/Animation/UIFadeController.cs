using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIFadeController : MonoBehaviour
{
    [Header("Fade Configuración")]
    public CanvasGroup canvasGroup;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 0.5f;
    public float fadeInDelay = 0f;  // ⏳ Tiempo de espera antes de iniciar el fade-in
    public float fadeOutDelay = 0f; // ⏳ Tiempo de espera antes de iniciar el fade-out

    [Header("Efectos Visuales (Shader en UI)")]
    public Material hologramMaterial;
    private Dictionary<Graphic, Material> originalMaterials = new Dictionary<Graphic, Material>();

    [Header("Efectos de Partículas")]
    public ParticleSystem appearEffect;   // 💥 Partículas al aparecer
    public ParticleSystem disappearEffect; // 💨 Partículas al desaparecer
    public bool flashEffectOnAppear = true; // ⚡ Hacer un destello al aparecer

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip appearSound;
    public AudioClip disappearSound;

    private Coroutine currentFadeRoutine;

    void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Guardar materiales originales y detectar todos los elementos gráficos
        foreach (Graphic graphic in GetComponentsInChildren<Graphic>(true))
        {
            originalMaterials[graphic] = graphic.material;
        }

        // Asegurar que la UI empieza oculta
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowUI()
    {
        if (currentFadeRoutine != null) StopCoroutine(currentFadeRoutine);
        currentFadeRoutine = StartCoroutine(ShowUIRoutine());
    }

    private IEnumerator ShowUIRoutine()
    {
        yield return new WaitForSeconds(fadeInDelay); // ⏳ Espera antes de mostrar

        if (appearEffect != null)
        {
            appearEffect.Play(); // 💥 Activa las partículas de aparición
        }

        if (flashEffectOnAppear)
        {
            StartCoroutine(FlashEffect()); // ⚡ Destello de holograma
        }

        if (appearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(appearSound);
        }

        ApplyHologramMaterial(); // Aplicar shader a todos los elementos

        currentFadeRoutine = StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, fadeInDuration));
    }

    public void HideUI()
    {
        if (currentFadeRoutine != null) StopCoroutine(currentFadeRoutine);
        currentFadeRoutine = StartCoroutine(HideUIRoutine());
    }

    private IEnumerator HideUIRoutine()
    {
        yield return new WaitForSeconds(fadeOutDelay); // ⏳ Espera antes de ocultar

        if (disappearEffect != null)
        {
            disappearEffect.Play(); // 💨 Activa las partículas de desaparición
        }

        if (disappearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(disappearSound);
        }

        StartCoroutine(RestoreMaterial(fadeOutDuration)); // Restaurar materiales originales
        currentFadeRoutine = StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, fadeOutDuration));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float targetAlpha, float duration)
    {
        float startAlpha = group.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        group.alpha = targetAlpha;

        bool isVisible = (targetAlpha > 0.9f);
        group.interactable = isVisible;
        group.blocksRaycasts = isVisible;
    }

    private void ApplyHologramMaterial()
    {
        foreach (var graphic in originalMaterials.Keys)
        {
            if (hologramMaterial != null)
            {
                graphic.material = hologramMaterial;
            }
        }
    }

    private IEnumerator RestoreMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var kvp in originalMaterials)
        {
            kvp.Key.material = kvp.Value;
        }
    }

    private IEnumerator FlashEffect()
    {
        float originalAlpha = canvasGroup.alpha;
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(0.1f); // ⚡ Destello corto

        canvasGroup.alpha = originalAlpha;
    }
}
