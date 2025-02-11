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
        Instance.currentActivityId = activityId;
    }

    public static int GetCurrentActivityId()
    {
        return Instance.currentActivityId;
    }

    public static IEnumerator Refresh()
    {
        yield return Instance.GetInActivityStudentParticipationsByActivityId();
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
