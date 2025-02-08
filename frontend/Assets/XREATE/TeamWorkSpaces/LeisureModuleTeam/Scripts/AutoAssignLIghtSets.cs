using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AutoAssignLightSets : MonoBehaviour
{
    [Tooltip("Selecciona la raíz donde están todos los HologramLightSet_XX")]
    public Transform hologramLightRoot;

    [Tooltip("Selecciona el componente Hologram Light Manager")]
    public HologramLightManager hologramLightManager;

#if UNITY_EDITOR
    [ContextMenu("Auto-Assign Light Sets")]
    public void AssignLightSetsAutomatically() // Renombrado para evitar conflicto
    {
        if (hologramLightRoot == null)
        {
            Debug.LogError("HologramLightRoot no está asignado. Por favor, asígalo en el inspector.");
            return;
        }

        if (hologramLightManager == null)
        {
            Debug.LogError("HologramLightManager no está asignado. Por favor, asígalo en el inspector.");
            return;
        }

        // Encuentra todos los sets hijos
        var lightSets = hologramLightRoot.GetComponentsInChildren<HologramLightSetController>();

        Debug.Log("Numerrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
        Debug.Log(lightSets.Length);

        if (lightSets.Length == 0)
        {
            Debug.LogWarning("No se encontraron hijos con el script HologramLightSetController en HologramLightRoot.");
            return;
        }

        // Ajusta el tamaño del array en HologramLightManager
        SerializedObject serializedObject = new SerializedObject(hologramLightManager);
        SerializedProperty lightSetsProperty = serializedObject.FindProperty("lightSets");

        serializedObject.Update();
        lightSetsProperty.arraySize = lightSets.Length;

        for (int i = 0; i < lightSets.Length; i++)
        {
            lightSetsProperty.GetArrayElementAtIndex(i).objectReferenceValue = lightSets[i];
            Debug.Log($"Asignado {lightSets[i].name} al índice {i}");
        }

        serializedObject.ApplyModifiedProperties();

        Debug.Log("Asignación automática completada.");
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(AutoAssignLightSets))]
public class AutoAssignLightSetsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoAssignLightSets script = (AutoAssignLightSets)target;
        if (GUILayout.Button("Auto-Assign Light Sets"))
        {
            script.AssignLightSetsAutomatically(); // Usar el nuevo nombre del método
        }
    }
}
#endif
