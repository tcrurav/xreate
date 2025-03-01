using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CyberSecurityPanel : MonoBehaviour
{
    public TextMeshPro lblIntro;
    public TextMeshPro lblTerm;
    public TextMeshPro lblDefinition;
    public AudioClip pickSound;
    private AudioSource audioSource;

    private Dictionary<string, string> securityTerms = new Dictionary<string, string>
    {
        { "VPN", "Virtual Private Network" },
        { "Phishing", "A type of cyber attack" },
        { "Malware", "Software designed to disrupt" },
        { "Firewall", "A network security system" },
        { "RAT", "Remote Access Trojan" },
        { "DDoS", "Distributed Denial of Service" },
        { "Spoofing", "The act of disguising" },
        { "Encryption", "The process of converting data" },
        { "Brute Force", "A trial-and-error attack" },
        { "Botnet", "A network of infected computers" },
        { "Zero-Day", "A vulnerability unknown to vendors" },
        { "SQL Injection", "A code injection technique" },
        { "Keylogger", "A software that records keystrokes" },
        { "Ransomware", "A malware that locks files for ransom" },
        { "Trojan", "A deceptive malware" },
        { "Spyware", "A malware that spies on user data" },
        { "Adware", "A software that displays unwanted ads" },
        { "Backdoor", "An undocumented way to access a system" },
        { "Social Engineering", "Manipulating people to divulge secrets" },
        { "Man-in-the-Middle", "Intercepting communication between parties" },
        { "Penetration Testing", "Assessing security by simulating attacks" },
        { "Dark Web", "A hidden part of the internet" },
        { "Ethical Hacking", "Hacking for security improvement" },
        { "Rootkit", "A software that enables unauthorized access" },
        { "Session Hijacking", "Taking control of an active session" },
        { "Two-Factor Authentication", "A security measure requiring two forms of verification" },
        { "Cryptojacking", "Unauthorized use of a device to mine cryptocurrency" },
        { "Digital Forensics", "Investigating cybercrimes" },
        { "Patch Management", "Keeping software updated to fix vulnerabilities" },
        { "IDS", "Intrusion Detection System" }
    };

    private List<KeyValuePair<string, string>> orderedSecurityTerms;
    private int currentIndex = 0;

    void Start()
    {
        // We initialize the text of the terms and definitions to empty
        lblTerm.text = "";
        lblDefinition.text = "";

        // Call the method that loads the security terms (without needing the service) LoadSecurityTerms();
        LoadSecurityTerms();

        lblIntro.text = "Welcome to Find the Pairs!!!\n" +
                        "Next, you will learn new concepts" +
                        " related to Cybersecurity.\n" +
                        "Read them carefully, discuss with" +
                        " your classmates\n and teachers.\n" +
                        "Then, you will dive into the challenge" +
                        " of finding the pairs.\n" +
                        "Stay alert, discover the pairs and\n" +
                        "earn points before time runs out.";

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void LoadSecurityTerms()
    {
        // Sort the dictionary terms
        orderedSecurityTerms = securityTerms.OrderBy(term => term.Key).ToList();
    }

    // Este método ahora recibe el diccionario de términos (completos)
    public void ReceiveSecurityTerms(Dictionary<string, string> terms)
    {
        securityTerms = terms;
        orderedSecurityTerms = new List<KeyValuePair<string, string>>(securityTerms);
        orderedSecurityTerms.Sort((term1, term2) => term1.Key.CompareTo(term2.Key)); // Ordenar términos

        if (orderedSecurityTerms.Count > 0)
        {
            ShowSecurityTerm();  // Mostrar el primer término si existe
        }
    }



    private void ShowSecurityTerm()
    {
        // Make sure the TextMeshPro's are updated correctly
        lblTerm.text = "Terms";
        lblDefinition.text = "Definitions. Push the button green to start";

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
            Debug.LogError("Terms have not been uploaded yet. Please wait.");
            return;
        }

        if (currentIndex < orderedSecurityTerms.Count - 1)
        {
            currentIndex++;
            ShowSecurityTerm();
            PlaySound();
        }
        else
        {
            Debug.Log("You have already reached the last term.");
        }
    }

    public void OnPreviousButtonPressed()
    {
        if (currentIndex > 0)
        {
            // First go back to the previous term
            currentIndex--;
            ShowSecurityTerm();
            // Play sound
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