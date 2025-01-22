// Utilities/JsonLoader.cs
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TeamWorkSpaces.LeisureModule;

public static class JsonLoader
{
    public static List<Question> LoadQuestions(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            QuestionData data = JsonUtility.FromJson<QuestionData>(jsonContent);
            return data.questions;
        }
        else
        {
            Debug.LogError($"Archivo JSON no encontrado en {filePath}");
            return new List<Question>();
        }
    }

    public static List<string> LoadDictionary(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            DictionaryData data = JsonUtility.FromJson<DictionaryData>(jsonContent);
            return data.cybersecurity_words;
        }
        else
        {
            Debug.LogError($"Archivo JSON no encontrado en {filePath}");
            return new List<string>();
        }
    }
}
