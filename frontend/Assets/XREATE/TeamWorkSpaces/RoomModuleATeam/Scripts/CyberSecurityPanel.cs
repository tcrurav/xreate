using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CyberSecurityPanel : MonoBehaviour
{
    public TextMeshPro lblIntro;
    public TextMeshPro lblTerm;
    public TextMeshPro lblDefinition;
    public AudioClip pickSound; // Sonido del botón
    private AudioSource audioSource;

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>();
    private List<KeyValuePair<string, string>> orderedSecurityTerms;
    private int currentIndex = 0;

    // Hacer privado el servicio y asignarlo dentro del código
    private ActivityChallengeConfigItemService activityChallengeConfigItemService;

    void Start()
    {
        // Crear la instancia del servicio en el código, sin necesidad del Inspector
        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

        // Llamar al servicio para cargar los términos de seguridad (usando el id dinámico)
        StartCoroutine(LoadSecurityTerms());

        lblIntro.text = "Welcome to Find the Pairs!!!\n" +
                        "Next, you will learn new concepts" +
                        " related to Cybersecurity.\n" +
                        "Read them carefully, discuss with" +
                        " your classmates\n and teachers.\n" +
                        "Then, you will dive into the challenge" +
                        " of finding the pairs.\n" +
                        "Stay alert, discover the pairs and\n" +
                        "earn points before time runs out.";

        // Agregar un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }


    private IEnumerator LoadSecurityTerms()
    {
        yield return activityChallengeConfigItemService.GetAllById(CurrentActivityManager.GetCurrentActivityId());
        Debug.Log($"Datos recibidos: {activityChallengeConfigItemService.activityChallengeConfigItems.Length} elementos");

        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
        {
            foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
            {
                ActivityChallengeConfigItemValue value = JsonUtility.FromJson<ActivityChallengeConfigItemValue>(item.value);

                for (int i = 0; i < value.answers.Length; i += 2)
                {
                    securityTerms.Add(value.answers[i],value.answers[i+1]);
                }

            }

            // Ahora ordenamos los términos
            orderedSecurityTerms = securityTerms.OrderBy(term => term.Key).ToList();

            Debug.Log($"Términos cargados: {orderedSecurityTerms.Count}");

            // Mostramos el primer término
            if (orderedSecurityTerms.Count > 0)
            {
                ShowSecurityTerm();
            }
        }
        else
        {
            Debug.LogError("No se recibieron datos o el array está vacío.");
        }
    }



    private void ShowSecurityTerm()
    {
        // Asegurarnos de que los TextMeshPro se actualicen correctamente
        lblTerm.text = "";
        lblDefinition.text = "";

        if (orderedSecurityTerms.Count > 0 && currentIndex < orderedSecurityTerms.Count)
        {
            var term = orderedSecurityTerms[currentIndex];
            lblTerm.text = term.Key;
            lblDefinition.text = term.Value;
        }
    }

    public void OnNextButtonPressed()
    {
        if (orderedSecurityTerms == null || orderedSecurityTerms.Count == 0)
        {
            Debug.LogError("Aún no se han cargado los términos. Por favor, espere.");
            return;
        }

        if (currentIndex < orderedSecurityTerms.Count - 1)
        {
            currentIndex++;
            ShowSecurityTerm();
            PlaySound();
        }
    }

    public void OnPreviousButtonPressed()
    {
        if (currentIndex > 0)
        {
            currentIndex--; // Primero retrocede al término anterior
            ShowSecurityTerm();
            PlaySound(); // Reproducir sonido
        }
    }

    private void PlaySound()
    {
        if (pickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickSound);
        }
    }
}
