using UnityEngine;

public class EnableScenesController : MonoBehaviour
{
    public string[] sceneContainers;
    void OnEnable()
    {
        // TODO - RoomModuleC(& TunnelConnectorE), RoomModuleB(& TunnelConnectorC), LeisureRoom(& TunnelConnectorD), should be bellow
        // At the moment only RoomModuleB. 

        for (var i = 0; i < sceneContainers.Length; i++)
        {
            MainNavigationManager.EnableSceneContainer(sceneContainers[i]);
        }
    }
}
