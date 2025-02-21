using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CyberSecurityPanel : MonoBehaviour
{
    public TextMeshPro lblIntro;
    public TextMeshPro lblTerm;
    public TextMeshPro lblDefinition;

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
        // Ordena el diccionario por clave
        orderedSecurityTerms = securityTerms.OrderBy(term => term.Key).ToList();

        lblIntro.text = "Welcome to Find the Pairs!!!\n" +
                        "Next, you will learn new concepts\n" +
                        " related to Cybersecurity.\n" +
                        "Read them carefully, discuss with\n" +
                        "your classmates and teachers.\n" +
                        "Then, you will dive into the challenge\n" +
                        "of finding the pairs.\n" +
                        "Stay alert, discover the pairs and \n" +
                        "earn points before time runs out.";

        // Mostrar el primer término al inicio
        ShowSecurityTerm();
    }

    private void ShowSecurityTerm()
    {
        // Asegurarnos de que los TextMeshPro se actualicen correctamente
        lblTerm.text = "";  // Limpiar antes de actualizar
        lblDefinition.text = "";  // Limpiar antes de actualizar

        if (orderedSecurityTerms.Count > 0 && currentIndex >= 0 && currentIndex < orderedSecurityTerms.Count)
        {
            var term = orderedSecurityTerms[currentIndex];
            lblTerm.text = term.Key;
            lblDefinition.text = term.Value;
        }
    }

    public void OnNextButtonPressed()
    {
        if (currentIndex < orderedSecurityTerms.Count - 1)
        {
            currentIndex++;
            ShowSecurityTerm();
        }
    }

    public void OnPreviousButtonPressed()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowSecurityTerm();
        }
    }
}
