using UnityEngine;

public class AlienInCorridorController : MonoBehaviour
{
    public GameObject AlienContainer;
    public GameObject BubblesContainer;
    public GameObject TransparentWallsBeforeAlienContainer;
    public GameObject TransparentWallsAtEntryContainer;
    public GameObject alienSpeach;

    private int numberOfStudentsOfPlayerTeamInScene = 5; // TODO At the beginning should be 0
    private int numberOfStudentsInTeam = 5;
    private int numberOfTeachersInScene = 1; // TODO At the beginning should be 0
    private AudioSource alienSpeechAudioSource;

    private void Start()
    {
        alienSpeechAudioSource = alienSpeach.GetComponent<AudioSource>();
    }

    public void PlayerArrivesToAtEntryTransparentWall()
    {
        AlienContainer.SetActive(true);
        BubblesContainer.SetActive(true);
        TransparentWallsBeforeAlienContainer.SetActive(true);

        alienSpeach.SetActive(true);
        alienSpeechAudioSource.Play();

        TransparentWallsAtEntryContainer.SetActive(false);
    }

    public void PlayerArrivesToBeforeAlienTransparentWall()
    {
        if (numberOfStudentsOfPlayerTeamInScene == numberOfStudentsInTeam && numberOfTeachersInScene > 0)
        {
            AlienContainer.SetActive(false);
            BubblesContainer.SetActive(false);
            TransparentWallsBeforeAlienContainer.SetActive(false);

            alienSpeechAudioSource.Stop();
            alienSpeach.SetActive(false);
        }
    }
}
