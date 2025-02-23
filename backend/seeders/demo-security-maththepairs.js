'use strict';

require('dotenv').config();

/** @type {import('sequelize-cli').Migration} */
module.exports = {
    async up(queryInterface, Sequelize) {
        // We insert the challenge with all the terms and definitions as a single JSON field
        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 50,
            activityChallengeConfigId: 1, // ID challenger Match the Pairs
            item: 'Security Terms - Match the Pairs',
            value: JSON.stringify({
                answers: [
                    "VPN",
                    "Virtual Private Network",
                    "Phishing",
                    "A type of cyber attack",
                    "Malware",
                    "Software designed to disrupt",
                    "Firewall",
                    "A network security system",
                    "RAT",
                    "Remote Access Trojan",
                    "DDoS",
                    "Distributed Denial of Service",
                    "Spoofing",
                    "The act of disguising",
                    "Encryption",
                    "The process of converting data",
                    "Brute Force",
                    "A trial-and-error attack",
                    "Botnet",
                    "A network of infected computers",
                    "Zero-Day",
                    "A vulnerability unknown to vendors",
                    "SQL Injection",
                    "A code injection technique",
                    "Keylogger",
                    "A software that records keystrokes",
                    "Ransomware",
                    "A malware that locks files for ransom",
                    "Trojan",
                    "A deceptive malware",
                    "Spyware",
                    "A malware that spies on user data",
                    "Adware",
                    "A software that displays unwanted ads",
                    "Backdoor",
                    "An undocumented way to access a system",
                    "Social Engineering",
                    "Manipulating people to divulge secrets",
                    "Man-in-the-Middle",
                    "Intercepting communication between parties",
                    "Penetration Testing",
                    "Assessing security by simulating attacks",
                    "Dark Web",
                    "A hidden part of the internet",
                    "Ethical Hacking",
                    "Hacking for security improvement",
                    "Rootkit",
                    "A software that enables unauthorized access",
                    "Session Hijacking",
                    "Taking control of an active session",
                    "Two-Factor Authentication",
                    "A security measure requiring two forms of verification",
                    "Cryptojacking",
                    "Unauthorized use of a device to mine cryptocurrency",
                    "Digital Forensics",
                    "Investigating cybercrimes",
                    "Patch Management",
                    "Keeping software updated to fix vulnerabilities",
                    "IDS",
                    "Intrusion Detection System"
                ]
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});
    },

    async down(queryInterface, Sequelize) {
        await queryInterface.bulkDelete('activity_challenge_config_items', { id: 50 }, {});
    }
};