using UnityEngine;
using UnityEditor;

public class RenameTool : EditorWindow
{
    private string searchTerm = "(Clone)";
    private string replaceTerm = "";

    [MenuItem("Tools/Rename Tool")]
    private static void ShowWindow()
    {
        GetWindow<RenameTool>("Rename Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Buscar y reemplazar en los nombres (recursivo)", EditorStyles.boldLabel);

        searchTerm = EditorGUILayout.TextField("Buscar", searchTerm);
        replaceTerm = EditorGUILayout.TextField("Reemplazar con", replaceTerm);

        // Botón para renombrar todo (objetos seleccionados + sus hijos)
        if (GUILayout.Button("Renombrar objetos seleccionados (+hijos)"))
        {
            foreach (var obj in Selection.gameObjects)
            {
                RenameGameObjectRecursively(obj, searchTerm, replaceTerm);
            }
        }
    }

    /// <summary>
    /// Renombra un GameObject y todos sus hijos de forma recursiva.
    /// </summary>
    private void RenameGameObjectRecursively(GameObject obj, string search, string replace)
    {
        // Reemplaza el nombre en el objeto actual
        string newName = obj.name.Replace(search, replace);
        Undo.RecordObject(obj, "Rename Object"); // Para que sea deshacible (CTRL+Z)
        obj.name = newName;

        // Llama recursivamente a todos los hijos
        foreach (Transform child in obj.transform)
        {
            RenameGameObjectRecursively(child.gameObject, search, replace);
        }
    }
}
