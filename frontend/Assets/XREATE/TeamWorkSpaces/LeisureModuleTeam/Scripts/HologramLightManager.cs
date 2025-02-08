using System.Collections;
using UnityEngine;

[System.Serializable]
public class LightEffectPreset
{
    public string name;               // Nombre del preset para identificarlo
    public Color color1 = Color.red;  // Primer color
    public Color color2 = Color.green; // Segundo color
    public float intensity1 = 1f;     // Intensidad del primer color
    public float intensity2 = 1f;     // Intensidad del segundo color
    public float interval = 1f;       // Tiempo de cambio entre estados
}

public class HologramLightManager : MonoBehaviour
{
    [Header("Light Sets")]
    [SerializeField] private HologramLightSetController[] lightSets;

    [Header("Effect Presets")]
    [SerializeField] private LightEffectPreset[] effectPresets; // Configuraciones predefinidas

    private Coroutine currentEffectCoroutine; // Para detener efectos actuales

    /// <summary>
    /// Cambia las propiedades de TODOS los focos (de todos los conjuntos).
    /// </summary>
    public void SetAllLights(Color color, float intensity)
    {
        foreach (var set in lightSets)
        {
            for (int i = 0; i < 3; i++) // 3 focos por conjunto
            {
                set.SetLightProperties(i, color, intensity);
            }
        }
    }



    /// <summary>
    /// Cambia las propiedades de un tipo de foco específico (todos los HoloLight_1, HoloLight_2, HoloLight_3).
    /// </summary>
    public void SetSpecificLightType(int lightIndex, Color color, float intensity)
    {
        foreach (var set in lightSets)
        {
            set.SetLightProperties(lightIndex, color, intensity);
        }
    }

    /// <summary>
    /// Cambia las propiedades de un foco individual dentro de un conjunto.
    /// </summary>
    public void SetIndividualLight(int setIndex, int lightIndex, Color color, float intensity)
    {
        if (setIndex < 0 || setIndex >= lightSets.Length) return;
        lightSets[setIndex].SetLightProperties(lightIndex, color, intensity);
    }

    /// <summary>
    /// Activa/desactiva las partículas de TODOS los focos.
    /// </summary>
    public void ToggleAllParticles(bool enable)
    {
        foreach (var set in lightSets)
        {
            for (int i = 0; i < 3; i++)
            {
                set.ToggleParticles(i, enable);
            }
        }
    }

    /// <summary>
    /// Activa/desactiva las partículas de un tipo de foco específico.
    /// </summary>
    public void ToggleSpecificParticles(int lightIndex, bool enable)
    {
        foreach (var set in lightSets)
        {
            set.ToggleParticles(lightIndex, enable);
        }
    }

    /// <summary>
    /// Activa/desactiva las partículas de un foco individual dentro de un conjunto.
    /// </summary>
    public void ToggleIndividualParticles(int setIndex, int lightIndex, bool enable)
    {
        if (setIndex < 0 || setIndex >= lightSets.Length) return;
        lightSets[setIndex].ToggleParticles(lightIndex, enable);
    }

    /// <summary>
    /// Inicia una secuencia de luces en TODOS los conjuntos.
    /// </summary>
    public void StartAllLightSequences()
    {
        foreach (var set in lightSets)
        {
            set.StartLightSequence();
        }
    }

    /// <summary>
    /// Inicia una secuencia de luces en un conjunto específico.
    /// </summary>
    public void StartSequenceForSet(int setIndex)
    {
        if (setIndex < 0 || setIndex >= lightSets.Length) return;
        lightSets[setIndex].StartLightSequence();
    }

    /// <summary>
    /// Reinicia todas las luces al estado predeterminado.
    /// </summary>
    public void ResetAllLights()
    {
        foreach (var set in lightSets)
        {
            set.ResetLights();
        }
    }

    /// <summary>
    /// Reinicia las luces de un conjunto específico.
    /// </summary>
    public void ResetLightsForSet(int setIndex)
    {
        if (setIndex < 0 || setIndex >= lightSets.Length) return;
        lightSets[setIndex].ResetLights();
    }

    /// <summary>
    /// Inicia un efecto de parpadeo usando un preset.
    /// </summary>
    public void StartEffectWithPreset(int presetIndex)
    {
        if (presetIndex < 0 || presetIndex >= effectPresets.Length) return;

        LightEffectPreset preset = effectPresets[presetIndex];
        StartBlinkingAllLights(preset.color1, preset.color2, preset.intensity1, preset.intensity2, preset.interval);
    }

    /// <summary>
    /// Inicia un efecto de parpadeo global alternando entre dos colores.
    /// </summary>
    public void StartBlinkingAllLights(Color color1, Color color2, float intensity1, float intensity2, float interval)
    {
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine); // Detiene el efecto anterior si existe
        }

        currentEffectCoroutine = StartCoroutine(BlinkAllLights(color1, color2, intensity1, intensity2, interval));
    }

    /// <summary>
    /// Coroutine para hacer parpadear todas las luces con alternancia de colores e intensidad.
    /// </summary>
    private IEnumerator BlinkAllLights(Color color1, Color color2, float intensity1, float intensity2, float interval)
    {
        bool useFirstColor = true;

        while (true)
        {
            foreach (var set in lightSets)
            {
                for (int i = 0; i < 3; i++) // Afecta a los 3 focos de cada conjunto
                {
                    if (useFirstColor)
                        set.SetLightProperties(i, color1, intensity1);
                    else
                        set.SetLightProperties(i, color2, intensity2);
                }
            }

            useFirstColor = !useFirstColor; // Cambia el estado de color/intensidad
            yield return new WaitForSeconds(interval); // Aquí se usa el intervalo correctamente
        }
    }

    /// <summary>
    /// Detiene todos los efectos en ejecución.
    /// </summary>
    public void StopAllEffects()
    {
        if (currentEffectCoroutine != null)
        {
            StopCoroutine(currentEffectCoroutine);
            currentEffectCoroutine = null;
        }

        ResetAllLights(); // Restaura las luces al estado inicial
    }



    /// <summary>
    /// Enciende los focos uno por uno, en secuencia del 1 al 25.
    /// </summary>
    public void StartSequentialLightSequence(float delayBetweenLights, Color color, float intensity)
    {
        StopAllCoroutines(); // Detenemos cualquier otra coroutine en ejecución
        StartCoroutine(SequentialLightSequence(delayBetweenLights, color, intensity));
    }

    /// <summary>
    /// Coroutine para encender los focos uno por uno.
    /// </summary>
    private IEnumerator SequentialLightSequence(float delayBetweenLights, Color color, float intensity)
    {
        // Asegúrate de que todos los focos estén apagados al inicio
        SetAllLights(Color.black, 0f);

        // Recorre cada conjunto de focos
        for (int setIndex = 0; setIndex < lightSets.Length; setIndex++)
        {
            // Enciende los focos del conjunto actual
            for (int lightIndex = 0; lightIndex < 3; lightIndex++)
            {
                lightSets[setIndex].SetLightProperties(lightIndex, color, intensity);
            }

            // Espera el tiempo especificado antes de pasar al siguiente conjunto
            yield return new WaitForSeconds(delayBetweenLights);

            // Opcional: Apagar el conjunto anterior después de encender el siguiente
            lightSets[setIndex].ResetLights();
        }
    }

    /// <summary>
    /// Efecto fluido y secuencial tipo LED: primero enciende los focos A, luego B, y luego C.
    /// </summary>
    public void StartLEDStripEffect(float delayBetweenLights, float delayBetweenGroups, Color color, float intensity)
    {
        StopAllCoroutines(); // Detenemos cualquier otra coroutine activa
        StartCoroutine(LEDStripEffect(delayBetweenLights, delayBetweenGroups, color, intensity));
    }

    /// <summary>
    /// Coroutine para el efecto LED secuencial.
    /// </summary>
    private IEnumerator LEDStripEffect(float delayBetweenLights, float delayBetweenGroups, Color color, float intensity)
    {
        // Asegúrate de que todos los focos estén apagados al inicio
        SetAllLights(Color.black, 0f);

        // Iterar por cada "foco A" (primer foco de cada conjunto)
        for (int setIndex = 0; setIndex < lightSets.Length; setIndex++)
        {
            lightSets[setIndex].SetLightProperties(0, color, intensity); // Enciende el foco A
            yield return new WaitForSeconds(delayBetweenLights); // Espera entre focos
        }

        yield return new WaitForSeconds(delayBetweenGroups); // Espera entre el grupo de A y B

        // Iterar por cada "foco B" (segundo foco de cada conjunto)
        //for (int setIndex = 0; setIndex < lightSets.Length; setIndex++)
        //{
        //    Debug.Log($"Seeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeet: {setIndex} de {lightSets.Length}");
        //    lightSets[setIndex].SetLightProperties(1, color, intensity); // Enciende el foco B
        //    yield return new WaitForSeconds(delayBetweenLights); // Espera entre focos
        //}

        //yield return new WaitForSeconds(delayBetweenGroups); // Espera entre el grupo de B y C

        //// Iterar por cada "foco C" (tercer foco de cada conjunto)
        //for (int setIndex = 0; setIndex < lightSets.Length; setIndex++)
        //{
        //    lightSets[setIndex].SetLightProperties(2, color, intensity); // Enciende el foco C
        //    yield return new WaitForSeconds(delayBetweenLights); // Espera entre focos
        //}
    }



    public void StartEffectFromButton()
    {
        StartLEDStripEffect(0.05f, 0.1f, Color.green, 1f); 
    }




    private void Start()
    {
        // Configurar todas las luces como transparentes al inicio
        SetAllLights(Color.clear, 0f);

        // Iniciar la corrutina para añadir un retraso antes de cambiar a la secuencia
        //StartCoroutine(DelayedStartLEDStripEffect());
    }

    /// <summary>
    /// Corrutina para retrasar la ejecución del efecto LED Strip.
    /// </summary>
    private IEnumerator DelayedStartLEDStripEffect()
    {
        // Esperar 3 segundos antes de iniciar el efecto
        yield return new WaitForSeconds(3f);

        // Cambiar el color a verde y empezar la secuencia
        // StartLEDStripEffect(0.1f, 0.2f, Color.green, 1f);
        StartLEDStripEffect(0.05f, 0.1f, Color.green, 1f); // Velocidades más rápidas
    }


}
