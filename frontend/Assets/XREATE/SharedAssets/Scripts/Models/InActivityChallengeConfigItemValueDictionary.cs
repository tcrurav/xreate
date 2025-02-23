using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InActivityChallengeConfigItemValueDictionary
{
    public List<string> keys;
    public List<string> values;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }

        return dictionary;
    }
}