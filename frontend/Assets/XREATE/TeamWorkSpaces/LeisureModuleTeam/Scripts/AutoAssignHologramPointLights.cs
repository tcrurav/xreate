using UnityEngine;
using System.Linq;
using TeamWorkSpaces.LeisureModule;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AutoAssignHologramPointLights : MonoBehaviour
{
    public Transform hologramLightRoot; // Asigna la ra�z "HologramLightRoot" aqu� en el inspector.
    public string intermediateChildName = "HoloPointLight_3"; // Nombre del hijo intermedio.
    public string targetChildName = "PointLight"; // Nombre del objeto hijo final.

    public QuestionManager questionManager; // Referencia al script QuestionManager.

#if UNITY_EDITOR
    [ContextMenu("Auto-Assign Lights")]
    public void AutoAssignLights()
    {
        if (hologramLightRoot == null)
        {
            Debug.LogError("HologramLightRoot no est� asignado. Por favor, as�galo en el inspector.");
            return;
        }

        if (questionManager == null)
        {
            Debug.LogError("QuestionManager no est� asignado. Por favor, as�galo en el inspector.");
            return;
        }

        Debug.Log("Iniciando la auto-asignaci�n de luces...");

        var focusPositions = new System.Collections.Generic.List<Transform>();

        // Recorre todos los sets hijos de HologramLightRoot
        foreach (Transform lightSet in hologramLightRoot)
        {
            Debug.Log($"Procesando: {lightSet.name}");

            var controller = lightSet.GetComponent<HologramLightSetController>();
            if (controller == null)
            {
                Debug.LogWarning($"No se encontr� el script HologramLightSetController en {lightSet.name}");
                continue;
            }

            // Busca el hijo intermedio "HoloPointLight_3" dentro del set
            var holoPointLight = lightSet.Find(intermediateChildName);
            if (holoPointLight == null)
            {
                Debug.LogWarning($"No se encontr� '{intermediateChildName}' en {lightSet.name}");
                continue;
            }

            // Busca el hijo final "PointLight" dentro de "HoloPointLight_3"
            var pointLight = holoPointLight.Find(targetChildName);
            if (pointLight != null)
            {
                controller.holoLight1 = pointLight.GetComponent<Light>(); // Asigna la luz al controlador
                focusPositions.Add(pointLight); // A�ade la posici�n al array de Focus Positions
                Debug.Log($"Se asign� '{pointLight.name}' para {lightSet.name}");
            }
            else
            {
                Debug.LogWarning($"No se encontr� '{targetChildName}' dentro de '{holoPointLight.name}' en {lightSet.name}");
            }
        }

        // Actualiza Focus Positions en QuestionManager
        questionManager.focusPositions = focusPositions.ToArray();
        Debug.Log($"Focus Positions actualizadas: {focusPositions.Count} posiciones asignadas.");
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(AutoAssignHologramPointLights))]
public class AutoAssignHologramPointLightsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoAssignHologramPointLights script = (AutoAssignHologramPointLights)target;
        if (GUILayout.Button("Auto-Assign Lights"))
        {
            script.AutoAssignLights();
        }
    }
}
#endif
