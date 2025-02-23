'use strict';

require('dotenv').config();

/** @type {import('sequelize-cli').Migration} */
module.exports = {
    async up(queryInterface, Sequelize) {
        // We insert the challenge with all the terms and definitions  IMPORTANT the id =>100
        await queryInterface.bulkInsert('activity_challenge_config_items', [{ id: 101, activityChallengeConfigId: 1, item: 'Phishing', value: 'A type of cyber attack', createdAt: new Date(), updatedAt: new Date() },
            { id: 102, activityChallengeConfigId: 1, item: 'Malware', value: 'Software designed to disrupt', createdAt: new Date(), updatedAt: new Date() },
            { id: 103, activityChallengeConfigId: 1, item: 'Firewall', value: 'A network security system', createdAt: new Date(), updatedAt: new Date() },
            { id: 104, activityChallengeConfigId: 1, item: 'RAT', value: 'Remote Access Trojan', createdAt: new Date(), updatedAt: new Date() },
            { id: 105, activityChallengeConfigId: 1, item: 'DDoS', value: 'Distributed Denial of Service', createdAt: new Date(), updatedAt: new Date() },
            { id: 106, activityChallengeConfigId: 1, item: 'Spoofing', value: 'The act of disguising', createdAt: new Date(), updatedAt: new Date() },
            { id: 107, activityChallengeConfigId: 1, item: 'Encryption', value: 'The process of converting data', createdAt: new Date(), updatedAt: new Date() },
            { id: 108, activityChallengeConfigId: 1, item: 'Brute Force', value: 'A trial-and-error attack', createdAt: new Date(), updatedAt: new Date() },
            { id: 109, activityChallengeConfigId: 1, item: 'Botnet', value: 'A network of infected computers', createdAt: new Date(), updatedAt: new Date() },
            { id: 110, activityChallengeConfigId: 1, item: 'Zero-Day', value: 'A vulnerability unknown to vendors', createdAt: new Date(), updatedAt: new Date() },
            { id: 111, activityChallengeConfigId: 1, item: 'SQL Injection', value: 'A code injection technique', createdAt: new Date(), updatedAt: new Date() },
            { id: 112, activityChallengeConfigId: 1, item: 'Keylogger', value: 'A software that records keystrokes', createdAt: new Date(), updatedAt: new Date() },
            { id: 113, activityChallengeConfigId: 1, item: 'Ransomware', value: 'A malware that locks files for ransom', createdAt: new Date(), updatedAt: new Date() },
            { id: 114, activityChallengeConfigId: 1, item: 'Trojan', value: 'A deceptive malware', createdAt: new Date(), updatedAt: new Date() },
            { id: 115, activityChallengeConfigId: 1, item: 'Spyware', value: 'A malware that spies on user data', createdAt: new Date(), updatedAt: new Date() },
            { id: 116, activityChallengeConfigId: 1, item: 'Adware', value: 'A software that displays unwanted ads', createdAt: new Date(), updatedAt: new Date() },
            { id: 117, activityChallengeConfigId: 1, item: 'Backdoor', value: 'An undocumented way to access a system', createdAt: new Date(), updatedAt: new Date() },
            { id:118, activityChallengeConfigId: 1, item: 'Social Engineering', value: 'Manipulating people to divulge secrets', createdAt: new Date(), updatedAt: new Date() },
            { id: 119, activityChallengeConfigId: 1, item: 'Man-in-the-Middle', value: 'Intercepting communication between parties', createdAt: new Date(), updatedAt: new Date() },
            { id: 120, activityChallengeConfigId: 1, item: 'Penetration Testing', value: 'Assessing security by simulating attacks', createdAt: new Date(), updatedAt: new Date() },
            { id: 121, activityChallengeConfigId: 1, item: 'Dark Web', value: 'A hidden part of the internet', createdAt: new Date(), updatedAt: new Date() },
            { id: 122, activityChallengeConfigId: 1, item: 'Ethical Hacking', value: 'Hacking for security improvement', createdAt: new Date(), updatedAt: new Date() },
            { id: 123, activityChallengeConfigId: 1, item: 'Rootkit', value: 'A software that enables unauthorized access', createdAt: new Date(), updatedAt: new Date() },
            { id: 124, activityChallengeConfigId: 1, item: 'Session Hijacking', value: 'Taking control of an active session', createdAt: new Date(), updatedAt: new Date() },
            { id: 125, activityChallengeConfigId: 1, item: 'Two-Factor Authentication', value: 'A security measure requiring two forms of verification', createdAt: new Date(), updatedAt: new Date() },
            { id: 126, activityChallengeConfigId: 1, item: 'Cryptojacking', value: 'Unauthorized use of a device to mine cryptocurrency', createdAt: new Date(), updatedAt: new Date() },
            { id: 127, activityChallengeConfigId: 1, item: 'Digital Forensics', value: 'Investigating cybercrimes', createdAt: new Date(), updatedAt: new Date() },
            { id: 128, activityChallengeConfigId: 1, item: 'Patch Management', value: 'Keeping software updated to fix vulnerabilities', createdAt: new Date(), updatedAt: new Date() },
            { id: 129, activityChallengeConfigId: 1, item: 'IDS', value: 'Intrusion Detection System', createdAt: new Date(), updatedAt: new Date() },
            { id: 130, activityChallengeConfigId: 1, item: 'VPN', value: 'Virtual Private Network', createdAt: new Date(), updatedAt: new Date() }], {});
    },

    async down(queryInterface, Sequelize) {
        await queryInterface.bulkDelete('activity_challenge_config_items', { activityChallengeConfigId: 1 }, {});
    }
};
