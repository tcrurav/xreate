[System.Serializable]
public class InActivityStudentParticipationWithActivityAndPoints
{
    // This is an element of a student learning path

    // InActivityStudentParticipation
    public int teamId;
    public int activityId;
    public int studentId;
    public string state;
    public int order;

    // Activity
    public System.DateTime activityStartDate;
    public System.DateTime activityEndDate;
    public string activityState;
    public string activityType;
    public string activityName;
    public string activityDescription;

    // points
    public int points;
}
