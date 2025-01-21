using UnityEngine;
using UnityEditor;

public class DistributeObjectsOnCircularWall : EditorWindow
{
    private GameObject objectToDistribute;  // Objeto a duplicar
    private Transform wallCenter;          // Centro de la pared circular
    private int numberOfObjects = 10;      // Número de objetos a distribuir
    private float wallRadius = 5f;         // Radio de la pared circular
    private float startAngle = 0f;         // Ángulo inicial (en grados)
    private float angleSpan = 360f;        // Ángulo total de la distribución (puede ser menor a 360°)
    private Vector3 rotationOffset = Vector3.zero;  // Ajuste adicional de rotación

    [MenuItem("Tools/Distribute On Circular Wall")]
    public static void ShowWindow()
    {
        GetWindow<DistributeObjectsOnCircularWall>("Distribute On Circular Wall");
    }

    private void OnGUI()
    {
        GUILayout.Label("Distribute Objects on Circular Wall", EditorStyles.boldLabel);

        // Configuración del objeto y centro
        objectToDistribute = (GameObject)EditorGUILayout.ObjectField("Object to Distribute", objectToDistribute, typeof(GameObject), true);
        wallCenter = (Transform)EditorGUILayout.ObjectField("Wall Center", wallCenter, typeof(Transform), true);

        // Parámetros de distribución
        numberOfObjects = EditorGUILayout.IntField("Number of Objects", numberOfObjects);
        wallRadius = EditorGUILayout.FloatField("Wall Radius", wallRadius);
        startAngle = EditorGUILayout.FloatField("Start Angle", startAngle);
        angleSpan = EditorGUILayout.FloatField("Angle Span", angleSpan);

        // Opciones de rotación
        rotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", rotationOffset);

        // Botón para ejecutar la distribución
        if (GUILayout.Button("Distribute"))
        {
            DistributeObjects();
        }
    }

    private void DistributeObjects()
    {
        if (objectToDistribute == null)
        {
            Debug.LogError("No se ha asignado un objeto para distribuir.");
            return;
        }

        if (wallCenter == null)
        {
            Debug.LogError("No se ha asignado el centro de la pared circular.");
            return;
        }

        // Calcula el paso angular entre objetos
        float angleStep = angleSpan / (numberOfObjects - 1);

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calcula el ángulo en radianes para el objeto actual
            float angle = Mathf.Deg2Rad * (startAngle + angleStep * i);

            // Calcula la posición del objeto en la circunferencia
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * wallRadius,
                objectToDistribute.transform.position.y,  // Mantén la altura del objeto original
                Mathf.Sin(angle) * wallRadius
            );

            // Ajusta la posición relativa al centro de la pared
            position += wallCenter.position;

            // Instancia el objeto en la posición calculada
            GameObject newObject = Instantiate(objectToDistribute, position, Quaternion.identity);

            // Configura la rotación del objeto para que apunte al centro de la pared
            newObject.transform.LookAt(wallCenter.position);

            // Aplica un offset adicional a la rotación
            newObject.transform.Rotate(rotationOffset);

            // Registra el objeto en el sistema de Undo para poder deshacer
            Undo.RegisterCreatedObjectUndo(newObject, "Distribute Objects on Circular Wall");
        }
    }
}
