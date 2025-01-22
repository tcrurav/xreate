using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AutoAssignHologramLights : MonoBehaviour
{
    public Transform hologramLightRoot; // Asigna la raíz "HologramLightRoot" aquí en el inspector.

#if UNITY_EDITOR
    [ContextMenu("Auto-Assign Lights")]
    public void AutoAssignLights()
    {
        if (hologramLightRoot == null)
        {
            Debug.LogError("HologramLightRoot no está asignado. Por favor, asígalo en el inspector.");
            return;
        }

        // Recorre todos los sets hijos de HologramLightRoot
        foreach (Transform lightSet in hologramLightRoot)
        {
            var controller = lightSet.GetComponent<HologramLightSetController>();
            if (controller == null)
            {
                Debug.LogWarning($"No se encontró el script HologramLightSetController en {lightSet.name}");
                continue;
            }

            // Busca las luces correspondientes en los hijos
            var pointLights = lightSet.GetComponentsInChildren<Light>();
            if (pointLights.Length >= 3)
            {
                controller.holoLight1 = pointLights[0];
                controller.holoLight2 = pointLights[1];
                controller.holoLight3 = pointLights[2];
                Debug.Log($"Luces asignadas correctamente para {lightSet.name}");
            }
            else
            {
                Debug.LogWarning($"No se encontraron suficientes luces en {lightSet.name}");
            }
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(AutoAssignHologramLights))]
public class AutoAssignHologramLightsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoAssignHologramLights script = (AutoAssignHologramLights)target;
        if (GUILayout.Button("Auto-Assign Lights"))
        {
            script.AutoAssignLights();
        }
    }
}
#endif




