using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CyberSecurityPanel : MonoBehaviour
{
    public TextMeshPro lblIntro;
    public TextMeshPro lblTerm;
    public TextMeshPro lblDefinition;
    public AudioClip pickSound; // Sonido del bot�n
    private AudioSource audioSource;

    private Dictionary<string, string> securityTerms;
    private List<KeyValuePair<string, string>> orderedSecurityTerms;
    private int currentIndex = 0;

    void Start()
    {
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

    // Este m�todo ahora recibe el diccionario de t�rminos (completos)
    public void ReceiveSecurityTerms(Dictionary<string, string> terms)
    {
        securityTerms = terms;
        orderedSecurityTerms = new List<KeyValuePair<string, string>>(securityTerms);
        orderedSecurityTerms.Sort((term1, term2) => term1.Key.CompareTo(term2.Key)); // Ordenar t�rminos

        if (orderedSecurityTerms.Count > 0)
        {
            ShowSecurityTerm();  // Mostrar el primer t�rmino si existe
        }
    }

    // M�todo para mostrar el primer t�rmino de la lista ordenada
    private void ShowSecurityTerm()
    {
        if (orderedSecurityTerms.Count > 0 && currentIndex >= 0 && currentIndex < orderedSecurityTerms.Count)
        {
            var firstTerm = orderedSecurityTerms[currentIndex];  // Primer t�rmino de la lista
            lblTerm.text = firstTerm.Key;
            lblDefinition.text = firstTerm.Value;
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
            currentIndex--;
            ShowSecurityTerm();
            PlaySound();
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