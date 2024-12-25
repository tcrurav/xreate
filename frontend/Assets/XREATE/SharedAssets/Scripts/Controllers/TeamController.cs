using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamController : MonoBehaviour
{
    private TeamService teamService;

    public TMP_InputField AddNameInputField;

    public TMP_InputField UpdateNameInputField;
    public TMP_InputField UpdateIdInputField;

    public TMP_InputField DeleteIdInputField;

    private void Start()
    {
        teamService = gameObject.AddComponent<TeamService>();
    }

    public void GetTeams()
    {
        teamService.GetTeams();
    }

    public void AddTeam()
    {
        Team team = new();
        team.name = AddNameInputField.text;

        teamService.CreateTeam(team);
    }

    public void UpdateTeam()
    {
        Team team = new();
        team.id = int.Parse(UpdateIdInputField.text);
        team.name = UpdateNameInputField.text;

        teamService.UpdateTeam(team.id, team);
    }

    public void DeleteTeam()
    {
        int id = int.Parse(DeleteIdInputField.text);

        teamService.DeleteTeam(id);
    }
}
