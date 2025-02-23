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

        Debug.Log($"Datos recibidos: {activityChallengeConfigItemService.activityChallengeConfigItems?.Length ?? 0} elementos");

        if (activityChallengeConfigItemService.activityChallengeConfigItems != null && activityChallengeConfigItemService.activityChallengeConfigItems.Length > 0)
        {
            // Crear una instancia de InActivityChallengeConfigItemValueDictionary
            var termsDictionary = new InActivityChallengeConfigItemValueDictionary();

            foreach (var item in activityChallengeConfigItemService.activityChallengeConfigItems)
            {
                // Filtrar solo los términos dentro del rango 100 - 150
                if (item.id < 100 || item.id > 150)
                {
                    continue; // Ignorar elementos fuera del rango
                }

                // Verificación de datos válidos
                if (string.IsNullOrEmpty(item.item) || string.IsNullOrEmpty(item.value))
                {
                    Debug.LogWarning($"Elemento inválido o con valores vacíos: {item.item} - {item.value}");
                    continue;
                }

                Debug.Log($"Procesando término: {item.item} - Definición: {item.value}");

                // Agregar a las listas de InActivityChallengeConfigItemValueDictionary
                termsDictionary.keys.Add(item.item);
                termsDictionary.values.Add(item.value);
            }

            // Convertir a un diccionario estándar
            var securityTerms = termsDictionary.ToDictionary();

            // Ordenar los términos
            orderedSecurityTerms = securityTerms.OrderBy(term => term.Key).ToList();

            Debug.Log($"Términos cargados y ordenados: {orderedSecurityTerms.Count}");

            // Mostrar el primer término
            if (orderedSecurityTerms.Count > 0)
            {
                ShowSecurityTerm(); // Aquí puedes definir cómo mostrar el término
            }
            else
            {
                Debug.LogError("No se cargaron términos válidos.");
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
