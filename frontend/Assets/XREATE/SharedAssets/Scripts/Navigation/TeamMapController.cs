using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;

public class TeamMapController : MonoBehaviour
{
    // TODO - Transform in a Serialized Wrapper
    public GameObject[] mainSlideShowMapTeamImages;        //map 0
    public GameObject[] tunnelToRoomModuleBMapTeamImages;  //map 1
    public GameObject[] tunnelToRoomModuleAMapTeamImages;  //map 2

    // teamImages[m][t], where 'm' is the map, and 't' is the team in the map 'm'
    private GameObject[][] teamImages;

    private TeamMapManager teamMapManager;
    private Vector3[] teamPositionsInMap;

    private void Start()
    {
        Debug.Log("TeamMapController - Start");

        teamImages = new GameObject[3][];
        teamImages[0] = new GameObject[2];
        teamImages[1] = new GameObject[2]; 
        teamImages[2] = new GameObject[2];

        for(int i = 0; i < mainSlideShowMapTeamImages.Length; i++) teamImages[0][i] = mainSlideShowMapTeamImages[i];
        for (int i = 0; i < tunnelToRoomModuleBMapTeamImages.Length; i++) teamImages[1][i] = tunnelToRoomModuleBMapTeamImages[i];
        for (int i = 0; i < tunnelToRoomModuleAMapTeamImages.Length; i++) teamImages[2][i] = tunnelToRoomModuleAMapTeamImages[i];

        //teamImages[0] = mainSlideShowMapTeamImages;
        //teamImages[1] = tunnelToRoomModuleBMapTeamImages;
        //teamImages[2] = tunnelToRoomModuleAMapTeamImages;

        teamMapManager = GetComponent<TeamMapManager>();
        teamPositionsInMap = new Vector3[Enum.GetValues(typeof(Scene)).Length];

        teamPositionsInMap[(int)Scene.Login] = new Vector3(304.9534f, -1627.642f, 0.00226346f);        // This is not used in Map. MainScene Value
        teamPositionsInMap[(int)Scene.Menu] = new Vector3(304.9534f, -1627.642f, 0.00226346f);         // This is not used in Map. MainScene Value
        teamPositionsInMap[(int)Scene.Auditorium] = new Vector3(304.9534f, -1627.642f, 0.00226346f);   // This is not used in Map. MainScene Value
        teamPositionsInMap[(int)Scene.Main] = new Vector3(304.9534f, -1627.642f, 0.00226346f);
        teamPositionsInMap[(int)Scene.RoomModuleB] = new Vector3(304.9534f, -668.07f, 0.00226346f);
        teamPositionsInMap[(int)Scene.RoomModuleA] = new Vector3(898.4f, -668.07f, 0.00226346f);
        teamPositionsInMap[(int)Scene.LeisureModule] = new Vector3(350.5f, -399.2f, 0.00226346f);
        teamPositionsInMap[(int)Scene.TunnelConnectorC] = new Vector3(379.7f, -634.4f, 0.00226346f);
        teamPositionsInMap[(int)Scene.TunnelConnectorD] = new Vector3(736.9f, -634.4f, 0.00226346f);
        teamPositionsInMap[(int)Scene.TunnelConnectorF] = new Vector3(465.5f, -534.8f, 0.00226346f);    // This is not used in Map
    }

    //public void ChangeTeamScene(int teamId, Scene newScene)
    //{
    //    DebugManager.Log($"TeamMapController - ChangeTeamScene - teamId: {teamId}, newScene: {(int)newScene}");

    //    // index is (teamId - 1)
    //    teamMapManager.ChangeCurrentTeamSceneServerRpc(teamId - 1, (int)newScene);
    //}

    //public void OnChangeTeamScene(int index, int newSceneValue)
    //{
    //    // index is (teamId - 1)
    //    for (int m = 0; m < teamImages.Length; m++)
    //    {
    //        teamImages[m][index].transform.position = teamPositionsInMap[newSceneValue];
    //    }
    //}

    public void RefreshTeamPositionsInAllMaps()
    {
        // 'm' is map, and 't' is team
        for (int m = 0; m < teamImages.Length; m++)
        {
            RefreshTeamPositionsInMap(m);
        }
    }

    public void RefreshTeamPositionsInMap(int mapId)
    {
       if (teamMapManager == null)
        {
            Debug.Log("RefreshTeamPositionsInMap - teamMapManager is null");
            return;
        }
        // 't' is team
        for (int t = 0; t < teamImages[mapId].Length; t++)
        {
            try
            {
                int auxScene = teamMapManager.currentTeamScene[t];
                Vector3 position = teamPositionsInMap[auxScene];
                if (t > 0 && teamMapManager.currentTeamScene[0] == auxScene)
                {
                    // if both teams are in same scene, then shift position so that they both don't overlap
                    position += new Vector3(200f, 200f, 0f);
                }
                teamImages[mapId][t].GetComponent<RectTransform>().localPosition = position;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

}
