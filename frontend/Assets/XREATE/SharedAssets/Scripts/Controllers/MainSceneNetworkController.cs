using UnityEngine;

public class MainSceneNetworkController : MonoBehaviour
{
    public TeamMapManager TeamMapManager;

    private void OnEnable()
    {
        StartCoroutine(CurrentActivityManager.Refresh());

        // TODO - It should be refactor for any number of teams
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(0, (int)Scene.Main); //Team 1 is in MainScene
        TeamMapManager.ChangeCurrentTeamSceneServerRpc(1, (int)Scene.Main); //Team 2 is in MainScene
    }
}
