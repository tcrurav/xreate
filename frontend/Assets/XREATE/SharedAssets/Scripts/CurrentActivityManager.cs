using System.Collections;
using UnityEngine;

public class CurrentActivityManager : MonoBehaviour
{
    public static CurrentActivityManager Instance;

    private int currentActivityId;
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
        //Instance.currentActivityId = currentActivityId;
        //Instance.inCurrentActivityStudentParticipations = inCurrentActivityStudentParticipations;
        Instance.inActivityStudentParticipationService = gameObject.AddComponent<InActivityStudentParticipationService>();
    }

    public static void SetCurrentActivityId(int activityId)
    {
        Debug.Log("ActivityId:");
        Debug.Log(activityId);
        Instance.currentActivityId = activityId;
    }

    public static IEnumerator Refresh()
    {
        Debug.Log("CurrentActivityManager, Refresh");
        yield return Instance.GetInActivityStudentParticipationsByActivityId();
    }

    private IEnumerator GetInActivityStudentParticipationsByActivityId()
    {
        Debug.Log("CurrentActivityManager, GetInActivityStudentParticipationsByActivityId");
        yield return Instance.inActivityStudentParticipationService.GetAllByActivityId(Instance.currentActivityId);

        Debug.Log("HOLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

        Debug.Log("CurrentActivityManager, reponseCode");
        Debug.Log(Instance.inActivityStudentParticipationService.responseCode);

        if (Instance.inActivityStudentParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        Instance.inCurrentActivityStudentParticipations = Instance.inActivityStudentParticipationService.inActivityStudentParticipations;

        Debug.Log($"Instance.inCurrentActivityStudentParticipations: {Instance.inCurrentActivityStudentParticipations}");
        Debug.Log($"Instance.inCurrentActivityStudentParticipations[0].studentId: {Instance.inCurrentActivityStudentParticipations}");
    }

    public static int GetTeamIdByStudentId(int studentId)
    {
        if(MainManager.GetUser().role != "STUDENT")
        {
            Debug.Log("Only students are in a team");
            return 0;
        }

        foreach (InActivityStudentParticipation i in Instance.inCurrentActivityStudentParticipations)
        {
            if (i.studentId == studentId) return i.teamId;
        }
        return 0; // It should not happen
    }
}
