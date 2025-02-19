'use strict';

require('dotenv').config();
const bcrypt = require('bcryptjs');

/** @type {import('sequelize-cli').Migration} */
module.exports = {
    async up(queryInterface, Sequelize) {

        // All the questions and answers for the Security Concepts challenge

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 1,
            activityChallengeConfigId: 2,
            item: "What is Phishing?",
            value: JSON.stringify({
                answers: [
                    "A: A method of attack that seeks to obtain personal information through deception.",
                    "B: A type of malware that infects operating systems.",
                    "C: An attack to block access to a network.",
                    "D: A method to intercept encrypted communications.",
                    "E: A system to improve password security."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 2,
            activityChallengeConfigId: 2,
            item: "What does Ransomware do?",
            value: JSON.stringify({
                answers: [
                    "A: Steals access credentials.",
                    "B: Encrypts user data and demands a ransom to return it.",
                    "C: Infects the hardware of the device.",
                    "D: Monitors activity in the web browser.",
                    "E: Changes system settings without permission."
                ],
                correctAnswerIndex: 1
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 3,
            activityChallengeConfigId: 2,
            item: "What is a Trojan?",
            value: JSON.stringify({
                answers: [
                    "A: A type of virus designed to replicate.",
                    "B: A tool for protection against malware.",
                    "C: A malicious program disguised as legitimate software.",
                    "D: An attack designed to saturate a network.",
                    "E: A system to detect suspicious activity."
                ],
                correctAnswerIndex: 2
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 4,
            activityChallengeConfigId: 2,
            item: "What is a brute force attack?",
            value: JSON.stringify({
                answers: [
                    "A: An attack that tries all possible combinations to crack passwords.",
                    "B: A strategy to deceive a user through social engineering.",
                    "C: A virus that automatically deletes data.",
                    "D: A method to gain physical access to servers.",
                    "E: A technique to manipulate databases."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 5,
            activityChallengeConfigId: 2,
            item: "What is a Botnet?",
            value: JSON.stringify({
                answers: [
                    "A: An advanced antivirus program.",
                    "B: A network of infected devices controlled remotely.",
                    "C: A technique for performing phishing.",
                    "D: A method to protect online privacy.",
                    "E: A system to prevent DoS attacks."
                ],
                correctAnswerIndex: 1
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 6,
            activityChallengeConfigId: 2,
            item: "What is a DoS (Denial of Service) attack?",
            value: JSON.stringify({
                answers: [
                    "A: An attempt to exploit hardware vulnerabilities.",
                    "B: An attack that steals user credentials.",
                    "C: Malware designed to encrypt personal data.",
                    "D: An attack that blocks a service by saturating it with requests.",
                    "E: A method of malicious redirection in browsers."
                ],
                correctAnswerIndex: 3
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 7,
            activityChallengeConfigId: 2,
            item: "What is a Rootkit?",
            value: JSON.stringify({
                answers: [
                    "A: Software that allows hidden access to a system and avoids detection.",
                    "B: A network monitoring tool.",
                    "C: An advanced DoS attack.",
                    "D: A program to encrypt personal data.",
                    "E: A remote control system for servers."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 8,
            activityChallengeConfigId: 2,
            item: "What is an SSL certificate?",
            value: JSON.stringify({
                answers: [
                    "A: An attack to decrypt encrypted data.",
                    "B: An authentication system for encrypted communications.",
                    "C: A system for performing advanced social engineering.",
                    "D: A security standard for local networks.",
                    "E: A type of malware designed for servers."
                ],
                correctAnswerIndex: 1
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 9,
            activityChallengeConfigId: 2,
            item: "What is an SQL Injection attack?",
            value: JSON.stringify({
                answers: [
                    "A: A method to prevent the use of weak passwords.",
                    "B: A technique for performing phishing attacks.",
                    "C: An attack that injects malicious code into databases through queries.",
                    "D: A vulnerability that allows unauthorized remote access.",
                    "E: A technique to disable security settings."
                ],
                correctAnswerIndex: 2
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 10,
            activityChallengeConfigId: 2,
            item: "What does the term Spoofing mean?",
            value: JSON.stringify({
                answers: [
                    "A: An attack designed to steal personal data through social networks.",
                    "B: A type of malware that infects mobile devices.",
                    "C: A method to encrypt sensitive data.",
                    "D: Software to control devices remotely.",
                    "E: A technique to falsify identity or origin address."
                ],
                correctAnswerIndex: 4
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 11,
            activityChallengeConfigId: 2,
            item: "What is a Social Engineering attack?",
            value: JSON.stringify({
                answers: [
                    "A: A technical attack targeting operating systems.",
                    "B: A strategy to manipulate people and obtain confidential information.",
                    "C: A technique to protect personal information.",
                    "D: A type of malware designed for social networks.",
                    "E: An intrusion detection system."
                ],
                correctAnswerIndex: 1
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 12,
            activityChallengeConfigId: 2,
            item: "What is a Man-in-the-Middle (MITM) attack?",
            value: JSON.stringify({
                answers: [
                    "A: An attack where a third party intercepts and manipulates communication between two parties.",
                    "B: Malware that spreads automatically on a network.",
                    "C: Software that allows decrypting passwords.",
                    "D: A method to avoid secure connections.",
                    "E: A system to deceive users through social engineering."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 13,
            activityChallengeConfigId: 2,
            item: "What is Cryptojacking?",
            value: JSON.stringify({
                answers: [
                    "A: An attack that steals cryptocurrencies directly from digital wallets.",
                    "B: An attack that uses resources from infected devices to mine cryptocurrencies.",
                    "C: A protection technique against ransomware.",
                    "D: Software for making secure payments.",
                    "E: An attack to disable the mining network."
                ],
                correctAnswerIndex: 1
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 14,
            activityChallengeConfigId: 2,
            item: "What is a Zero-Day?",
            value: JSON.stringify({
                answers: [
                    "A: An unknown vulnerability exploited before the developer releases a patch.",
                    "B: Malware designed to disable antivirus software.",
                    "C: A network traffic monitoring system.",
                    "D: A method to protect weak passwords.",
                    "E: An attack targeting obsolete hardware."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 15,
            activityChallengeConfigId: 2,
            item: "What is a Keylogger?",
            value: JSON.stringify({
                answers: [
                    "A: Malware that records keystrokes.",
                    "B: An attack to block social networks.",
                    "C: A data recovery technique.",
                    "D: A system to automatically encrypt passwords.",
                    "E: An attack designed to disable firewalls."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

        await queryInterface.bulkInsert('activity_challenge_config_items', [{
            id: 16,
            activityChallengeConfigId: 2,
            item: "What is a Spear Phishing attack?",
            value: JSON.stringify({
                answers: [
                    "A: A more specific and targeted version of a phishing attack.",
                    "B: An attack to disable encrypted passwords.",
                    "C: Advanced monitoring software.",
                    "D: A technique to infect databases.",
                    "E: A system to track spoofed emails."
                ],
                correctAnswerIndex: 0
            }),
            createdAt: new Date(),
            updatedAt: new Date()
        }], {});

    },

    async down(queryInterface, Sequelize) {
        await queryInterface.bulkDelete('activities', null, {});
    }
};