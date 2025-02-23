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
    public AudioClip pickSound; // Sonido del bot�n
    private AudioSource audioSource;

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>();
    private List<KeyValuePair<string, string>> orderedSecurityTerms;
    private int currentIndex = 0;

    // Hacer privado el servicio y asignarlo dentro del c�digo
    private ActivityChallengeConfigItemService activityChallengeConfigItemService;

    void Start()
    {
        // Crear la instancia del servicio en el c�digo, sin necesidad del Inspector
        activityChallengeConfigItemService = gameObject.AddComponent<ActivityChallengeConfigItemService>();

        // Llamar al servicio para cargar los t�rminos de seguridad (usando el id din�mico)
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
                // Filtrar solo los t�rminos dentro del rango 100 - 150
                if (item.id < 100 || item.id > 150)
                {
                    continue; // Ignorar elementos fuera del rango
                }

                // Verificaci�n de datos v�lidos
                if (string.IsNullOrEmpty(item.item) || string.IsNullOrEmpty(item.value))
                {
                    Debug.LogWarning($"Elemento inv�lido o con valores vac�os: {item.item} - {item.value}");
                    continue;
                }

                Debug.Log($"Procesando t�rmino: {item.item} - Definici�n: {item.value}");

                // Agregar a las listas de InActivityChallengeConfigItemValueDictionary
                termsDictionary.keys.Add(item.item);
                termsDictionary.values.Add(item.value);
            }

            // Convertir a un diccionario est�ndar
            var securityTerms = termsDictionary.ToDictionary();

            // Ordenar los t�rminos
            orderedSecurityTerms = securityTerms.OrderBy(term => term.Key).ToList();

            Debug.Log($"T�rminos cargados y ordenados: {orderedSecurityTerms.Count}");

            // Mostrar el primer t�rmino
            if (orderedSecurityTerms.Count > 0)
            {
                ShowSecurityTerm(); // Aqu� puedes definir c�mo mostrar el t�rmino
            }
            else
            {
                Debug.LogError("No se cargaron t�rminos v�lidos.");
            }
        }
        else
        {
            Debug.LogError("No se recibieron datos o el array est� vac�o.");
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
            Debug.LogError("A�n no se han cargado los t�rminos. Por favor, espere.");
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
            currentIndex--; // Primero retrocede al t�rmino anterior
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
