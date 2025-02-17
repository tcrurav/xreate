using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static List<float> allPlayerTimes = new List<float>();
    public static List<int> allPlayerScores = new List<int>();

    public void AddPlayerScore(float time)
    {
        int score = CalculateScore(time);
        allPlayerTimes.Add(time);
        allPlayerScores.Add(score);
    }

    private int CalculateScore(float time)
    {
        if (time <= 180) return 10;
        if (time <= 300) return 8;
        if (time <= 360) return 6;
        if (time <= 480) return 4;
        if (time <= 600) return 2;
        return 0;
    }

    public void DisplayLeaderboard()
    {
        for (int i = 0; i < allPlayerScores.Count; i++)
        {
            Debug.Log($"Player {i + 1}: Time = {allPlayerTimes[i]:F2}s, Score = {allPlayerScores[i]}");
        }
    }
}
