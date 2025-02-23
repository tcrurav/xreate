using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class CurrentActivityManager : MonoBehaviour
{
    public static CurrentActivityManager Instance;

    private int currentActivityId;

    //TODO : ATTENTION!!! This should be changed because in an activity and a challenge there could be more than 1 ActivityChallengeConfig 
    private ActivityChallengeConfigService activityChallengeConfigService;
    private ActivityChallengeConfig[] inCurrentActivityChallengeConfigs;

    private int NumberOfChallengesInActivity;

    private InActivityStudentParticipationService inActivityStudentParticipationService;
    private InActivityStudentParticipation[] inCurrentActivityStudentParticipations;

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

    private void Start()
    {
        Instance.inActivityStudentParticipationService = gameObject.AddComponent<InActivityStudentParticipationService>();
        Instance.activityChallengeConfigService = gameObject.AddComponent<ActivityChallengeConfigService>();
    }

    public static void SetCurrentActivityId(int activityId)
    {
        Instance.currentActivityId = activityId;
    }

    public static int GetCurrentActivityId()
    {
        return Instance.currentActivityId;
    }

    public static int GetNumberOfChallengesInActivity()
    {
        return Instance.NumberOfChallengesInActivity;
    }

    public static void SetNumberOfChallengesInActivity(int n)
    {
        Instance.NumberOfChallengesInActivity = n;
    }

    public static IEnumerator Refresh()
    {
        yield return Instance.GetInActivityStudentParticipationsByActivityId();

        yield return Instance.GetActivityChallengeConfigsByActivityId();

        SetNumberOfChallengesInActivity(Instance.inCurrentActivityChallengeConfigs.Length);
    }

    private IEnumerator GetInActivityStudentParticipationsByActivityId()
    {
        yield return Instance.inActivityStudentParticipationService.GetAllByActivityId(Instance.currentActivityId);

        if (Instance.inActivityStudentParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        Instance.inCurrentActivityStudentParticipations = Instance.inActivityStudentParticipationService.inActivityStudentParticipations;

        StartCoroutine(WaitForPlayerObjectAndThenChangeTeamId());
    }

    public static IEnumerator WaitForPlayerObjectAndThenChangeTeamId()
    {
        // TODO - Maybe this should be for a certain number of seconds only
        while (NetworkManager.Singleton.LocalClient == null || NetworkManager.Singleton.LocalClient.PlayerObject == null)
        {
            yield return null;
        }

        ChangeTeamIdInPlayerSync();
    }

    private static void ChangeTeamIdInPlayerSync()
    {
        int teamId = GetTeamIdByStudentId(MainManager.GetUser().id);

        Debug.Log($"ChangeTeamIdInPlayerSync ANTES - teamId: {teamId}");
        DebugManager.Log($"ChangeTeamIdInPlayerSync ANTES - teamId: {teamId}");

        if (NetworkManager.Singleton.LocalClient != null)
        {
            Debug.Log($"ChangeTeamIdInPlayerSync DESPUES - teamId: {teamId}");
            DebugManager.Log($"ChangeTeamIdInPlayerSync DESPUES - teamId: {teamId}");

            PlayerSync player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerSync>();

            if (player != null)
            {
                player.SetPlayerTeamId(teamId);
            }
        }
    }

    private IEnumerator GetActivityChallengeConfigsByActivityId()
    {
        yield return Instance.activityChallengeConfigService.GetAllByActivityId(Instance.currentActivityId);

        if (Instance.activityChallengeConfigService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        Debug.Log($"GetActivityChallengeConfigsByActivityId - Instance.activityChallengeConfigService.activityChallengeConfigsByActivityId[0].id: {Instance.activityChallengeConfigService.activityChallengeConfigsByActivityId[0].id}");

        Instance.inCurrentActivityChallengeConfigs = Instance.activityChallengeConfigService.activityChallengeConfigsByActivityId;
    }

    public static int GetTeamIdByStudentId(int studentId)
    {
        if (MainManager.GetUser().role != "STUDENT")
        {
            Debug.Log("Only students are in a team");
            return 0;
        }

        if (Instance.inCurrentActivityStudentParticipations == null) return 0; // inCurrentActivityStudentParticipation is not updated yet.

        foreach (InActivityStudentParticipation i in Instance.inCurrentActivityStudentParticipations)
        {
            if (i.studentId == studentId) return i.teamId;
        }
        return 0; // It should not happen
    }
}
