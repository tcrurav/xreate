using UnityEngine;
using UnityEditor;

public class DistributeObjectsInCircle : EditorWindow
{
    private GameObject objectToDistribute;  // Objeto a duplicar
    private Transform centerPoint;         // Punto central opcional
    private bool useCurrentPosition = true; // Usar la posición actual como punto central
    private int numberOfObjects = 10;      // Número de objetos
    private float radius = 5f;             // Radio del círculo
    private float diameter = 0f;           // Diámetro (opcional)
    private float startAngle = 0f;         // Ángulo inicial en grados
    private bool rotateTowardsCenter = true;  // Rotar objetos hacia el centro
    private bool useOriginalRotation = true;  // Usar la rotación original del objeto
    private Vector3 rotationOffset = Vector3.zero;  // Rotación adicional por objeto
    private bool distributeInSpiral = false;  // Distribuir en espiral
    private float heightStep = 0f;        // Incremento de altura para espiral

    [MenuItem("Tools/Distribute In Circle")]
    public static void ShowWindow()
    {
        GetWindow<DistributeObjectsInCircle>("Distribute Objects In Circle");
    }

    private void OnGUI()
    {
        GUILayout.Label("Distribute Objects in Circle", EditorStyles.boldLabel);

        // Configuración de objeto y centro
        objectToDistribute = (GameObject)EditorGUILayout.ObjectField("Object to Distribute", objectToDistribute, typeof(GameObject), true);
        centerPoint = (Transform)EditorGUILayout.ObjectField("Center Point (Optional)", centerPoint, typeof(Transform), true);
        useCurrentPosition = EditorGUILayout.Toggle("Use Current Position", useCurrentPosition);

        // Parámetros de distribución
        numberOfObjects = EditorGUILayout.IntField("Number of Objects", numberOfObjects);
        radius = EditorGUILayout.FloatField("Radius", radius);
        diameter = EditorGUILayout.FloatField("Diameter (Overrides Radius)", diameter);

        // Opciones de distribución
        startAngle = EditorGUILayout.FloatField("Start Angle", startAngle);
        rotateTowardsCenter = EditorGUILayout.Toggle("Rotate Towards Center", rotateTowardsCenter);
        useOriginalRotation = EditorGUILayout.Toggle("Use Original Rotation", useOriginalRotation);
        rotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", rotationOffset);

        // Opciones de espiral
        distributeInSpiral = EditorGUILayout.Toggle("Distribute in Spiral", distributeInSpiral);
        if (distributeInSpiral)
        {
            heightStep = EditorGUILayout.FloatField("Height Step (Spiral)", heightStep);
        }

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

        // Determinar el radio desde el diámetro si está definido
        float effectiveRadius = diameter > 0 ? diameter / 2f : radius;

        // Determinar el punto central
        Vector3 centerPosition = useCurrentPosition ? objectToDistribute.transform.position : (centerPoint ? centerPoint.position : Vector3.zero);

        // Paso angular entre cada objeto
        float angleStep = 360f / numberOfObjects;

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calcula el ángulo en radianes para el objeto actual
            float angle = Mathf.Deg2Rad * (startAngle + angleStep * i);

            // Calcula la posición en el círculo
            Vector3 position = new Vector3(
                Mathf.Cos(angle) * effectiveRadius,
                distributeInSpiral ? heightStep * i : 0f,  // Ajusta la altura si es espiral
                Mathf.Sin(angle) * effectiveRadius
            );

            // Ajusta la posición relativa al centro
            position += centerPosition;

            // Instancia el objeto en la posición calculada
            GameObject newObject = Instantiate(objectToDistribute, position, Quaternion.identity);

            // Configura la rotación
            if (rotateTowardsCenter && !useOriginalRotation)
            {
                // Rotar hacia el centro más un offset adicional
                newObject.transform.LookAt(centerPosition);
                newObject.transform.Rotate(rotationOffset);
            }
            else if (useOriginalRotation)
            {
                // Usar la rotación original del objeto
                newObject.transform.rotation = objectToDistribute.transform.rotation;
                newObject.transform.Rotate(rotationOffset);  // Aplicar el offset adicional
            }
            else
            {
                // Aplica únicamente el offset de rotación
                newObject.transform.rotation = Quaternion.Euler(rotationOffset);
            }

            // Registra el objeto en el sistema de Undo para poder deshacer
            Undo.RegisterCreatedObjectUndo(newObject, "Distribute Objects");
        }
    }
}
