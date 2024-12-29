using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InActivityStudentParticipationWithActivityAndPoints
{
    // This is an element of a student learning path

    // InActivityStudentParticipation
    public int teamId;
    public int activityId;
    public int studentId;
    public string participationState;
    public int order;

    // Activity
    public System.DateTime startDate;
    public System.DateTime endDate;
    public string state;
    public string type;
    public string name;
    public string description;

    // points
    public int points;
}
