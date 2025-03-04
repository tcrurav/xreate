using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    public TeamMapManager TeamMapManager;
    public GameObject WristMenu;

    // MainScene
    public GameObject StartButton;
    public GameObject ForceAttentionToggle;
    //public GameObject ButtonArrowRight;
    //public GameObject ButtonArrowLeft;

    // RoomModuleB
    public GameObject RoomModuleBTeam1PlayButton;
    public GameObject RoomModuleBTeam1CheckboxButton;
    public GameObject RoomModuleBTeam2PlayButton;
    public GameObject RoomModuleBTeam2CheckboxButton;

    // RoomModuleA
    //public GameObject RoomModuleATeam1PlayButton;
    public GameObject RoomModuleATeam1CheckboxButton;
    //public GameObject RoomModuleATeam2PlayButton;
    public GameObject RoomModuleATeam2CheckboxButton;

    // Leisure Module
    //public GameObject LeisureModuleTeam1PlayButton;
    public GameObject LeisureModuleTeam1CheckboxButton;
    //public GameObject LeisureModuleTeam2PlayButton;
    public GameObject LeisureModuleTeam2CheckboxButton;

    // Corridor to Room Module B
    public GameObject CorridorToModuleBTeam1CheckboxButton;
    public GameObject CorridorToModuleBTeam2CheckboxButton;

    // Corridor to Room Module A
    public GameObject CorridorToModuleATeam1CheckboxButton;
    public GameObject CorridorToModuleATeam2CheckboxButton;

    private void OnEnable()
    {
        WristMenu.SetActive(true);


        if (MainManager.IsTeacherPermissionsActivated())
        {
            if (MainManager.GetUser().role != "TEACHER")
            {
                StartButton.SetActive(false);                               // Only teachers can Start the Training Lab
                ForceAttentionToggle.SetActive(false);                      // Only teachers can Force Attention
                //ButtonArrowRight.SetActive(false);                        // Only teachers can use slide button right
                //ButtonArrowLeft.SetActive(false);                         // Only teachers can use slide button left

                // Room Module B
                RoomModuleBTeam1PlayButton.SetActive(false);                // Only teachers can Start the Task
                RoomModuleBTeam1CheckboxButton.SetActive(false);            // Only teachers can Activate the button to go to next room 
                RoomModuleBTeam2PlayButton.SetActive(false);                // Only teachers can Start the Task
                RoomModuleBTeam2CheckboxButton.SetActive(false);            // Only teachers can Activate the button to go to next room 

                // Room Module A
                //RoomModuleATeam1PlayButton.SetActive(false);                // Only teachers can Start the Task
                RoomModuleATeam1CheckboxButton.SetActive(false);            // Only teachers can Activate the button to go to next room 
                //RoomModuleATeam2PlayButton.SetActive(false);                // Only teachers can Start the Task
                RoomModuleATeam2CheckboxButton.SetActive(false);            // Only teachers can Activate the button to go to next room 

                // LeisureModule
                //LeisureModuleTeam1PlayButton.SetActive(false);              // Only teachers can Start the Task
                LeisureModuleTeam1CheckboxButton.SetActive(false);               // Only teachers can Activate the button to go to next room 
                //LeisureModuleTeam2PlayButton.SetActive(false);              // Only teachers can Start the Task
                LeisureModuleTeam2CheckboxButton.SetActive(false);          // Only teachers can Activate the button to go to next room

                // Corridor to Room Module B
                CorridorToModuleBTeam1CheckboxButton.SetActive(false);      // Only teachers can Activate the button to go to next room 
                CorridorToModuleBTeam2CheckboxButton.SetActive(false);      // Only teachers can Activate the button to go to next room

                // Corridor to Room Module A
                CorridorToModuleATeam1CheckboxButton.SetActive(false);      // Only teachers can Activate the button to go to next room 
                CorridorToModuleATeam2CheckboxButton.SetActive(false);      // Only teachers can Activate the button to go to next room
            }
        }

        StartCoroutine(CurrentActivityManager.Refresh());

        // TODO - It should be refactor for any number of teams
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.Main); //Team 1 is in MainScene
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(1, (int)Scene.Main); //Team 2 is in MainScene
    }
}
