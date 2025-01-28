using System.Collections;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    private TeamService teamService;

    public GameObject[] teamRawImageRings;

    //public GameObject loadingCanvas;
    //public GameObject errorCanvas;

    private void Start()
    {
        teamService = gameObject.AddComponent<TeamService>();

        Refresh();
    }

    public void Refresh()
    {
        Debug.Log("Refresh");
        //loadingCanvas.SetActive(true);
        StartCoroutine(GetTeamsWithPoints());
    }

    IEnumerator GetTeamsWithPoints()
    {
        yield return teamService.GetAllWithPoints();

        Debug.Log("reponseCode");
        Debug.Log(teamService.responseCode);

        if (teamService.responseCode != 200)
        {
            //loadingCanvas.SetActive(false);
            //errorCanvas.SetActive(true);
            yield break;
        }

        ShowTeamsWithPoints();
    }

    public void ShowTeamsWithPoints()
    {
        for (int i = 0; i < teamService.teamsWithPoints.Length; i++)
        {
            float newX = LeaderboardManager.GetStartLinePositionX() +
                teamService.teamsWithPoints[i].points / teamService.teamsWithPoints[i].maxPoints * LeaderboardManager.GetTaskLineLength();

            teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                newX,
                teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D.y,
                teamRawImageRings[i].GetComponent<RectTransform>().anchoredPosition3D.z);
        }

        // Hide Leaderboard Rings not playing. Leaderboard shows 4 Teams even when maybe less teams are playing.
        for (int i = teamService.teamsWithPoints.Length; i < 4; i++)
        {
            teamRawImageRings[i].SetActive(false);
        }
    }
}
