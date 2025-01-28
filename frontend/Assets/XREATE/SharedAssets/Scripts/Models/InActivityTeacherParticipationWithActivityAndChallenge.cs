[System.Serializable]
public class InActivityTeacherParticipationWithActivityAndChallenge
{
    // InActivityTeacherParticipation
    public int challengeId;
    public int activityId;
    public int teacherId;
    public string state;
    public int order;

    // Activity
    public System.DateTime activityStartDate;
    public System.DateTime activityEndDate;
    public string activityState;
    public string activityType;
    public string activityName;
    public string activityDescription;

    // Challenge
    public string challengeType;
    public string challengeName;
}
