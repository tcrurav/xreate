using System.Collections;
using TMPro;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public GameObject[] teamRawImageRings;
    public TMP_Text[] teams;
    public TMP_Text[] teamPoints;
    public TMP_Text[] topPlayerNames;

    public TeamMapManager teamMapManager;

    private TeamService teamService;
    private InActivityStudentParticipationService inActivityStudentParticipationService;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private float[] startLinePositionX = { -1132.0f, 24.0f, 1179.0f, 2347.0f };
    private float taskLineLength = 844.0f;

    private void OnEnable()
    {
        teamService = gameObject.AddComponent<TeamService>();
        inActivityStudentParticipationService = gameObject.AddComponent<InActivityStudentParticipationService>();
        Refresh();
    }

    public void Refresh()
    {
        //loadingCanvas.SetActive(true);
        StartCoroutine(GetTeamsWithChallengesAndPoints());
        StartCoroutine(GetAllWithPointsGroupedByTeams(CurrentActivityManager.GetCurrentActivityId()));
        StartCoroutine(GetAllTopPlayersWithPoints(CurrentActivityManager.GetCurrentActivityId()));

        // TODO - It should be refactored
        teamMapManager.GetComponent<TeamMapController>().RefreshTeamPositionsInMap(0);
        //teamMapManager.GetComponent<TeamMapController>().RefreshTeamPositionsInAllMaps();
    }

    IEnumerator GetTeamsWithChallengesAndPoints()
    {
        yield return teamService.GetAllWithChallengesAndPoints();

        if (teamService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        ShowTeamsWithChallengesAndPoints();
    }

    public void ShowTeamsWithChallengesAndPoints()
    {
        for (int i = 0; i < teamService.teamsWithChallengesAndPoints.Length; i++)
        {
            float newX = startLinePositionX[i % 4];
            if (CurrentActivityManager.GetNumberOfChallengesInActivity() > 0) newX = startLinePositionX[i % CurrentActivityManager.GetNumberOfChallengesInActivity()];
            
            int maxPoints = teamService.teamsWithChallengesAndPoints[i].maxPoints;
            if (maxPoints > 0)
            {
                newX = maxPoints + teamService.teamsWithChallengesAndPoints[i].points / teamService.teamsWithChallengesAndPoints[i].maxPoints * taskLineLength;
            }

            teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                newX,
                teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D.y,
                teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D.z);
        }
    }

    IEnumerator GetAllWithPointsGroupedByTeams(int activityId)
    {
        yield return inActivityStudentParticipationService.GetAllWithPointsGroupedByTeams(activityId);

        if (inActivityStudentParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        ShowInActivityStudentParticipationsWithPointsGroupedByTeams();
    }

    public void ShowInActivityStudentParticipationsWithPointsGroupedByTeams()
    {
        for (int i = 0; i < inActivityStudentParticipationService.inActivityStudentParticipationWithPointsGroupedByTeams.Length; i++)
        {
            teams[i].text = "TEAM " + inActivityStudentParticipationService.inActivityStudentParticipationWithPointsGroupedByTeams[i].teamId +
                " (" + inActivityStudentParticipationService.inActivityStudentParticipationWithPointsGroupedByTeams[i].teamName + ")";
            teamPoints[i].text = inActivityStudentParticipationService.inActivityStudentParticipationWithPointsGroupedByTeams[i].points.ToString() + " points";
        }
    }

    IEnumerator GetAllTopPlayersWithPoints(int activityId)
    {
        yield return inActivityStudentParticipationService.GetAllTopPlayersWithPoints(activityId);

        if (inActivityStudentParticipationService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        ShowInActivityStudentParticipationsTopPlayersWithPoints();
    }

    public void ShowInActivityStudentParticipationsTopPlayersWithPoints()
    {
        int actualTeamId = 0;
        int teamIndex = 0;
        for (int i = 0; i < inActivityStudentParticipationService.inActivityStudentParticipationTopPlayersWithPoints.Length; i++)
        {
            if (actualTeamId != inActivityStudentParticipationService.inActivityStudentParticipationTopPlayersWithPoints[i].teamId)
            {
                actualTeamId = inActivityStudentParticipationService.inActivityStudentParticipationTopPlayersWithPoints[i].teamId;
                if (inActivityStudentParticipationService.inActivityStudentParticipationTopPlayersWithPoints[i].points > 0)
                {
                    topPlayerNames[teamIndex].text = inActivityStudentParticipationService.inActivityStudentParticipationTopPlayersWithPoints[i].userName;
                } else
                {
                    topPlayerNames[teamIndex].text = "_________";
                }
                teamIndex++;
            }

        }
    }

}
