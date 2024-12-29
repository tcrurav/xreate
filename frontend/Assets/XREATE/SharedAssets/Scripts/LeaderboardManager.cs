using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    //private TeamService teamService;

    private const float startLinePositionX = 2380.0f;
    private const float taskLineLength = 848.0f;

    //private TeamWithPoints[] teamsWithPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //private void Start()
    //{
    //    //Instance.teamsWithPoints = new TeamWithPoints[0];

    //    teamService = gameObject.AddComponent<TeamService>();

    //}

    //public static void SetAccessToken(string accessToken)
    //{
    //    Instance.accessToken = accessToken;
    //}

    //public static void SetUser(User user)
    //{
    //    Instance.user = user;
    //}

    public static float GetStartLinePositionX()
    {
        return startLinePositionX;
    }

    public static float GetTaskLineLength()
    {
        return taskLineLength;
    }

}

// https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#
