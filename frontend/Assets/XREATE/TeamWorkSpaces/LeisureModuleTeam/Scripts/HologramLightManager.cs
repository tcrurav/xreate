using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class LightSettings
{
    public bool isActive = true;                // Activar o desactivar el nivel completo
    public Color colorOn = Color.white;         // Color cuando la luz está encendida
    public Color colorOff = Color.black;        // Color cuando la luz está apagada
    public float intensity = 1f;                // Intensidad de la luz
    public float sequentialDelay = 0.1f;        // Tiempo de espera para la activación secuencial
    public bool enableBlink = false;            // Habilitar/deshabilitar parpadeo
    public Color blinkColor1 = Color.yellow;       // Primer color para el parpadeo
    public Color blinkColor2 = Color.white;      // Segundo color para el parpadeo
    public float blinkInterval = 0.5f;          // Intervalo de parpadeo
}

public class HologramLightManager : MonoBehaviour
{
    [SerializeField] private HologramLightSetController[] lightSets;

    [SerializeField] private HologramLightSetController hologramLightSetController;


    [Header("Light Settings")]
    public LightSettings level1Settings;
    public LightSettings level3Settings;

    private Coroutine blinkCoroutine;

    private void SetLight(int level, Color color, float intensity, bool enable = true)
    {
        if (!GetSettingsForLevel(level).isActive)
        {
            Debug.Log($"Level {level} is deactivated.");
            return;
        }

        Debug.Log($"SetLight called - Level: {level}, Color: {color}, Intensity: {intensity}, Enable: {enable}");

        foreach (var set in lightSets)
        {
            Debug.Log($"Applying settings to LightSet: {set.name}, Light Index: {level - 1}");
            set.SetLightProperties(level - 1, color, intensity);
            set.TogglePointLight(level - 1, enable);
        }
    }

    public void ToggleLevel(int level, bool isOn)
    {
        Debug.Log($"ToggleLevel called - Level: {level}, IsOn: {isOn}");
        LightSettings settings = GetSettingsForLevel(level);
        SetLight(level, isOn ? settings.colorOn : settings.colorOff, isOn ? settings.intensity : 0f, isOn);

        if (isOn && settings.enableBlink)
        {
            ToggleBlinkLevel(level, true, settings.blinkInterval);
        }
    }

    public void ToggleAllLights(bool isOn)
    {
        Debug.Log($"ToggleAllLights called - IsOn: {isOn}");
        ToggleLevel(1, isOn);
        ToggleLevel(3, isOn);
    }

    public void ToggleSequentialLevel(int level, bool isOn)
    {
        Debug.Log($"ToggleSequentialLevel called - Level: {level}, IsOn: {isOn}");
        LightSettings settings = GetSettingsForLevel(level);
        if (isOn)
            StartCoroutine(SequentialTurnOnCoroutine(level, settings.sequentialDelay, settings.colorOn, settings.intensity));
        else
            SetLight(level, settings.colorOff, 0f, false);
    }

    private IEnumerator SequentialTurnOnCoroutine(int level, float delay, Color color, float intensity)
    {
        Debug.Log($"SequentialTurnOnCoroutine started - Level: {level}, Delay: {delay}, Color: {color}, Intensity: {intensity}");

        foreach (var set in lightSets)
        {
            Debug.Log($"Sequentially turning on LightSet: {set.name}, Light Index: {level - 1}");
            set.SetLightProperties(level - 1, color, intensity);
            set.TogglePointLight(level - 1, true);
            yield return new WaitForSeconds(delay);
        }
    }

    public void ToggleBlinkLevel(int level, bool isOn, float interval)
    {
        Debug.Log($"ToggleBlinkLevel called - Level: {level}, IsOn: {isOn}, Interval: {interval}");
        LightSettings settings = GetSettingsForLevel(level);
        if (isOn)
        {
            StopBlinking();
            blinkCoroutine = StartCoroutine(BlinkLights(level, settings.blinkColor1, settings.blinkColor2, interval, settings.intensity));
        }
        else
        {
            StopBlinking();
            SetLight(level, settings.colorOff, 0f, false);
        }
    }

    private IEnumerator BlinkLights(int level, Color color1, Color color2, float interval, float intensity)
    {
        Debug.Log($"BlinkLights started - Level: {level}, Color1: {color1}, Color2: {color2}, Interval: {interval}, Intensity: {intensity}");

        bool toggle = false;

        while (true)
        {
            Debug.Log($"Blinking - Level: {level}, CurrentColor: {(toggle ? color1 : color2)}");
            SetLight(level, toggle ? color1 : color2, intensity);
            toggle = !toggle;
            yield return new WaitForSeconds(interval);
        }
    }

    public void StopBlinking()
    {
        Debug.Log("StopBlinking called");

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    public void DisablePointLight(int level)
    {
        Debug.Log($"DisablePointLight called - Level: {level}");

        foreach (var set in lightSets)
        {
            set.TogglePointLight(level - 1, false);
        }
    }

    public void EffectLightBeforeLoadWords()
    {
        
        StartCoroutine(RunDemoSequence_LoadWords());
        StartCoroutine(DelayedRunDemoSequence());

    }

    private IEnumerator DelayedRunDemoSequence()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de llamar a la secuencia
        StartCoroutine(RunDemoSequence_LoadWords());
    }

    public void EnablePointLight(int level)
    {
        Debug.Log($"EnablePointLight called - Level: {level}");

        foreach (var set in lightSets)
        {
            set.TogglePointLight(level - 1, true);
        }
    }

    private LightSettings GetSettingsForLevel(int level)
    {
        Debug.Log($"GetSettingsForLevel called - Level: {level}");

        return level switch
        {
            1 => level1Settings,
            3 => level3Settings,
            _ => new LightSettings(),
        };
    }

    private void Start()
    {
        Debug.Log("HologramLightManager started");
        // Asegúrate de que las luces estén completamente apagadas al inicio
        ToggleLevel(1, false); // Apaga Level 1
        ToggleLevel(3, false); // Apaga Level 3

        // Desactiva físicamente los Point Lights para asegurarse
        DisablePointLight(1);
        DisablePointLight(3);

        // Inicia la demo después de apagar todo
        //StartCoroutine(RunDemoSequence());
    }

    private IEnumerator RunDemoSequence()
    {
        Debug.Log("⚡ Iniciando Demo de Luces...");

        // 🔹 Obtener todos los `HologramLightSetController` en la escena
        HologramLightSetController[] allLightSets = FindObjectsByType<HologramLightSetController>(FindObjectsSortMode.None);


        // 🔹 Iniciar el efecto de carga en todos los objetos con luces
        foreach (var lightSet in allLightSets)
        {
            lightSet.StartChargingEffect(Color.green, Color.red, 1f, 8f, 3f);
        }
        yield return new WaitForSeconds(3f); // Dejar el efecto de carga por 3 segundos

        // 🔹 Detener el efecto de carga en todos los objetos
        foreach (var lightSet in allLightSets)
        {
            lightSet.StopChargingEffect();
        }
        yield return new WaitForSeconds(1f);

        Debug.Log("🔆 Efecto de carga finalizado");

        // 🔹 Encender y apagar Level 3
        //ToggleLevel(3, true);
        //yield return new WaitForSeconds(2f);
        //ToggleLevel(3, false);
        //yield return new WaitForSeconds(1f);

        //Debug.Log("🔆 Nivel 3 Encendido y Apagado");

        // 🔹 Secuencia de encendido en Level 3
        ToggleSequentialLevel(3, true);
        yield return new WaitForSeconds(3f);
        ToggleSequentialLevel(3, false);
        yield return new WaitForSeconds(1f);

        Debug.Log("🚀 Secuencia de Encendido en Level 3");

        // 🔹 Secuencia invertida (25 a 1)
        StartCoroutine(SequentialTurnOnCoroutine(3, 0.1f, Color.blue, 1f, true));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SequentialTurnOnCoroutine(3, 0.1f, Color.black, 0f, true));

        // 🔹 Parpadeo final para simular carga completa
        ToggleBlinkLevel(3, true, 0.3f);
        yield return new WaitForSeconds(2f);
        ToggleBlinkLevel(3, false, 0.3f);

        Debug.Log("🎉 Demo de Luces Completada.");
    }

    private IEnumerator RunDemoSequence_LoadWords()
    {
        Debug.Log("⚡ Iniciando Efecto de Luces...");

        // 🔹 Parpadeo final para simular carga completa
        ToggleBlinkLevel(3, true, 0.3f);
        yield return new WaitForSeconds(6f);
        ToggleBlinkLevel(3, false, 0.3f);

        Debug.Log("🎉 Luces Completada parpadeo.");

        ToggleLevel(3, true);
        yield return new WaitForSeconds(2f);
        
        Debug.Log("🎉 Dejamos Luces encendidas.");
    }

    private IEnumerator RunDemoSequence_LoadWords1()
    {
        Debug.Log("⚡ Iniciando Efecto de Luces...");

        // Activar el efecto de carga con ControlPanelManager
        ControlPanelManager controlPanel = FindFirstObjectByType<ControlPanelManager>(); 

        if (controlPanel == null)
        {
            Debug.LogError("⚠ No se encontró ControlPanelManager en la escena.");
            yield break;
        }

        controlPanel.ToggleChargingEffectLevel3(true); // Iniciar carga
        yield return new WaitForSeconds(4f); // Dejar el efecto de carga por 4 segundos

        controlPanel.ToggleChargingEffectLevel3(false); // Detener carga
        yield return new WaitForSeconds(1f);

        Debug.Log("🔆 Efecto de carga finalizado");
    }

    private IEnumerator SequentialTurnOnCoroutine(int level, float delay, Color color, float intensity, bool reverse = false)
    {
        Debug.Log($"SequentialTurnOnCoroutine started - Level: {level}, Delay: {delay}, Color: {color}, Intensity: {intensity}, Reverse: {reverse}");

        // 🔹 Recorre las luces en orden inverso si reverse == true
        var lightSetList = reverse ? lightSets.Reverse() : lightSets;

        foreach (var set in lightSetList)
        {
            Debug.Log($"Sequentially turning on LightSet: {set.name}, Light Index: {level - 1}");
            set.SetLightProperties(level - 1, color, intensity);
            set.TogglePointLight(level - 1, true);
            yield return new WaitForSeconds(delay);
        }
    }

    public void ToggleChargingEffect(int level, bool isOn, Color color1, Color color2, float minIntensity, float maxIntensity, float duration)
    {
        Debug.Log($"ToggleChargingEffect called - Level: {level}, IsOn: {isOn}");

        foreach (var set in lightSets)
        {
            if (isOn)
            {
                set.StartChargingEffect(color1, color2, minIntensity, maxIntensity, duration);
            }
            else
            {
                set.StopChargingEffect();
            }
        }
    }




}
