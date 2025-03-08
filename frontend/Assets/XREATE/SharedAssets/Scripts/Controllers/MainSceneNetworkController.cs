using UnityEngine;
using UnityEngine.UI;

public class MainSceneNetworkController : MonoBehaviour
{
    public TeamMapManager TeamMapManager;
    public GameObject WristMenu;

    // MainScene
    public GameObject StartButton;
    public GameObject ForceAttentionToggle;
    public GameObject ButtonArrowRight;
    public GameObject ButtonArrowLeft;

    // RoomModuleB
    public GameObject RoomModuleBTeam1PlayButton;
    public GameObject RoomModuleBTeam1CheckboxButton;
    public GameObject RoomModuleBTeam2PlayButton;
    public GameObject RoomModuleBTeam2CheckboxButton;

    // RoomModuleA
    public GameObject RoomModuleATeam1PlayButton;
    public GameObject RoomModuleATeam1CheckboxButton;
    public GameObject RoomModuleATeam2PlayButton;
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

        // TODO - All this is mainly hardcoded. It should be changed with time
        if (MainManager.IsTeacherPermissionsActivated())
        {
            RoomModuleBTeam1CheckboxButton.GetComponent<Button>().interactable = false;      // Only teachers can Activate the button, but after Room task is finished
            RoomModuleBTeam2CheckboxButton.GetComponent<Button>().interactable = false;      // Only teachers can Activate the button, but after Room task is finished

            RoomModuleATeam1CheckboxButton.GetComponent<Button>().interactable = false;      // Only teachers can Activate the button, but after Room task is finished
            RoomModuleATeam2CheckboxButton.GetComponent<Button>().interactable = false;      // Only teachers can Activate the button, but after Room task is finished 

            //LeisureModuleTeam1CheckboxButton.GetComponent<Button>().interactable = false;    // Only teachers can Activate the button, but after Room task is finished
            //LeisureModuleTeam2CheckboxButton.GetComponent<Button>().interactable = false;    // Only teachers can Activate the button, but after Room task is finished

            if (MainManager.GetUser().role != "TEACHER")
            {
                StartButton.GetComponent<Button>().interactable = false;                // Only teachers can Start the Training Lab
                ForceAttentionToggle.GetComponent<Toggle>().interactable = false;       // Only teachers can Force Attention
                ButtonArrowRight.GetComponent<Button>().interactable = false;           // Only teachers can use slide button right
                ButtonArrowLeft.GetComponent<Button>().interactable = false;            // Only teachers can use slide button left

                // Room Module B
                RoomModuleBTeam1PlayButton.SetActive(false);                    // Only teachers can Start the Task
                RoomModuleBTeam2PlayButton.SetActive(false);                    // Only teachers can Start the Task

                // Room Module A
                RoomModuleATeam1PlayButton.SetActive(false);                    // Only teachers can Start the Task
                RoomModuleATeam2PlayButton.SetActive(false);                    // Only teachers can Start the Task
                
                // LeisureModule
                //LeisureModuleTeam1PlayButton.SetActive(false);                // Only teachers can Start the Task
                //LeisureModuleTeam2PlayButton.SetActive(false);                // Only teachers can Start the Task
                

                // Corridor to Room Module B
                CorridorToModuleBTeam1CheckboxButton.SetActive(false);          // Only teachers can Activate the button to go to next room 
                CorridorToModuleBTeam2CheckboxButton.SetActive(false);          // Only teachers can Activate the button to go to next room

                // Corridor to Room Module A
                CorridorToModuleATeam1CheckboxButton.SetActive(false);          // Only teachers can Activate the button to go to next room 
                CorridorToModuleATeam2CheckboxButton.SetActive(false);          // Only teachers can Activate the button to go to next room
            }

            if(MainManager.GetUser().role == "TEACHER")
            {
                if(MainManager.GetUser().username == MainManager.GetTeam1Teacher())
                {
                    // Room Module B
                    RoomModuleBTeam1PlayButton.SetActive(true);                // Only the teacher in charge of this Team can start this task
                    RoomModuleBTeam2PlayButton.SetActive(false);               // Only the teacher in charge of this Team can start this task

                    // Room Module A
                    RoomModuleATeam1PlayButton.SetActive(true);              // Only the teacher in charge of this Team can start this task
                    RoomModuleATeam2PlayButton.SetActive(false);              // Only the teacher in charge of this Team can start this task

                    // Leisure Module
                    //LeisureModuleTeam1PlayButton.SetActive(true);            // Only the teacher in charge of this Team can start this task
                    //LeisureModuleTeam2PlayButton.SetActive(false);            // Only the teacher in charge of this Team can start this task
                }

                if (MainManager.GetUser().username == MainManager.GetTeam2Teacher())
                {
                    // Room Module B
                    RoomModuleBTeam1PlayButton.SetActive(false);                // Only the teacher in charge of this Team can start this task
                    RoomModuleBTeam2PlayButton.SetActive(true);               // Only the teacher in charge of this Team can start this task

                    // Room Module A
                    RoomModuleATeam1PlayButton.SetActive(false);              // Only the teacher in charge of this Team can start this task
                    RoomModuleATeam2PlayButton.SetActive(true);              // Only the teacher in charge of this Team can start this task

                    // Leisure Module
                    //LeisureModuleTeam1PlayButton.SetActive(false);            // Only the teacher in charge of this Team can start this task
                    //LeisureModuleTeam2PlayButton.SetActive(true);            // Only the teacher in charge of this Team can start this task
                }
            }
        }

        StartCoroutine(CurrentActivityManager.Refresh());

        // TODO - It should be refactor for any number of teams
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.Main);     //Team 1 is in MainScene
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(1, (int)Scene.Main);     //Team 2 is in MainScene
    }
}
