using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AutoAssignLightSets : MonoBehaviour
{
    [Tooltip("Selecciona la ra�z donde est�n todos los HologramLightSet_XX")]
    public Transform hologramLightRoot;

    [Tooltip("Selecciona el componente Hologram Light Manager")]
    public HologramLightManager hologramLightManager;

#if UNITY_EDITOR
    [ContextMenu("Auto-Assign Light Sets")]
    public void AssignLightSetsAutomatically() // Renombrado para evitar conflicto
    {
        if (hologramLightRoot == null)
        {
            Debug.LogError("HologramLightRoot no est� asignado. Por favor, as�galo en el inspector.");
            return;
        }

        if (hologramLightManager == null)
        {
            Debug.LogError("HologramLightManager no est� asignado. Por favor, as�galo en el inspector.");
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

        // Ajusta el tama�o del array en HologramLightManager
        SerializedObject serializedObject = new SerializedObject(hologramLightManager);
        SerializedProperty lightSetsProperty = serializedObject.FindProperty("lightSets");

        serializedObject.Update();
        lightSetsProperty.arraySize = lightSets.Length;

        for (int i = 0; i < lightSets.Length; i++)
        {
            lightSetsProperty.GetArrayElementAtIndex(i).objectReferenceValue = lightSets[i];
            Debug.Log($"Asignado {lightSets[i].name} al �ndice {i}");
        }

        serializedObject.ApplyModifiedProperties();

        Debug.Log("Asignaci�n autom�tica completada.");
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
            script.AssignLightSetsAutomatically(); // Usar el nuevo nombre del m�todo
        }
    }
}
#endif
