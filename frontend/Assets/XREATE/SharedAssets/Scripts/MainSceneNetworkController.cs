using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CurrentActivityManager.Refresh());
    }

    public void GetAllPlayers()
    {
        MainNetworkManager.GetAllPlayers();
    }

    public void HideAllStudentsOfOtherTeams()
    {
        MainNetworkManager.HideAllStudentsOfOtherTeams();
    }

    public void ShowAllStudents()
    {
        MainNetworkManager.ShowAllStudents();
    }

    public void HideAndGetAll()
    {
        HideAllStudentsOfOtherTeams();
        GetAllPlayers();
    }

    public void ShowAndGetAll()
    {
        ShowAllStudents();
        GetAllPlayers();
    }

}
