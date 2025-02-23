using System;
using System.Collections.Generic;

[System.Serializable]
public class InActivityChallengeConfigItemValueDictionary
{
    public List<string> keys = new List<string>();  // Lista para almacenar las claves
    public List<string> values = new List<string>(); // Lista para almacenar los valores

    public InActivityChallengeConfigItemValueDictionary() { }

    public InActivityChallengeConfigItemValueDictionary(int id, string item, string value)
    {
        // Este constructor no es necesario para el funcionamiento del diccionario
        // pero puedes dejarlo si lo consideras útil.
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        for (int i = 0; i < keys.Count; i++)
        {
            if (i < values.Count) // Asegurarse de que el índice no exceda la lista de valores
            {
                dictionary[keys[i]] = values[i];
            }
        }

        return dictionary;
    }
}
