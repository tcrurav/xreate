using System.Collections;
using UnityEngine;

public class HologramLightSetController : MonoBehaviour
{
    [Header("Light References")]
    [SerializeField] public Light holoLight1;
    [SerializeField] public Light holoLight2;
    [SerializeField] public Light holoLight3;

    [Header("Particle System References")]
    [SerializeField] private ParticleSystem particles1;
    [SerializeField] private ParticleSystem particles2;
    [SerializeField] private ParticleSystem particles3;

    [Header("Light Properties")]
    public Color defaultColor = Color.white;
    public float defaultIntensity = 1f;
    public float hologramEffectIntensity = 3f;
    public Color hologramEffectColor = Color.cyan;

    [Header("Sequence Settings")]
    public float sequenceSpeed = 0.5f; // Velocidad del efecto
    public bool enableBounceEffect = true;

    private Light[] lights;
    private ParticleSystem[] particles;

    private void Awake()
    {
        // Agrupar las luces y partículas para facilidad de uso
        lights = new Light[] { holoLight1, holoLight2, holoLight3 };
        particles = new ParticleSystem[] { particles1, particles2, particles3 };

        ResetLights();
    }

    // Método para reiniciar los focos al estado inicial
    public void ResetLights()
    {
        if (holoLight3 != null)
        {
            holoLight3.color = defaultColor;
            holoLight3.intensity = defaultIntensity;
        }
    }



    // Cambiar propiedades de un foco específico
    public void SetLightProperties(int index, Color color, float intensity)
    {
        if (holoLight3 != null)
        {
            holoLight3.color = color;
            holoLight3.intensity = intensity;
        }
    }



    // Activar/Desactivar partículas de un foco específico
    public void ToggleParticles(int index, bool enable)
    {
        if (index < 0 || index >= particles.Length) return;
        if (particles[index] != null)
        {
            if (enable) particles[index].Play();
            else particles[index].Stop();
        }
    }

    // Efecto de secuencia lineal con rebote
    public void StartLightSequence()
    {
        StopAllCoroutines(); // Para evitar que se acumulen coroutines
        StartCoroutine(LightSequence());
    }
    // Método para activar o desactivar el Point Light
    public void TogglePointLight(int lightIndex, bool enable)
    {
        if (holoLight3 != null)
        {
            holoLight3.enabled = enable;
        }
    }

    private Coroutine chargingCoroutine; // Para controlar la animación de carga

    public void StartChargingEffect(Color color1, Color color2, float minIntensity, float maxIntensity, float speed)
    {
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
        }
        chargingCoroutine = StartCoroutine(ChargingEffect(color1, color2, minIntensity, maxIntensity, speed));
    }

    public void StopChargingEffect()
    {
        if (chargingCoroutine != null)
        {
            StopCoroutine(chargingCoroutine);
            chargingCoroutine = null;
        }

        // Restablecer la luz a su estado original
        if (holoLight3 != null)
        {
            holoLight3.color = defaultColor;
            holoLight3.intensity = defaultIntensity;
        }
    }

    private IEnumerator ChargingEffect(Color color1, Color color2, float minIntensity, float maxIntensity, float speed)
    {
        Debug.Log("⚡ Iniciando efecto de carga intermitente...");

        float t = 0f;
        bool increasing = true;
        bool toggleColor = false;

        while (true)
        {
            if (holoLight3 != null)
            {
                // Alternar entre color1 y color2 cada cierto tiempo
                holoLight3.color = toggleColor ? color1 : color2;

                // Efecto de aumento y disminución de intensidad
                if (increasing)
                {
                    t += Time.deltaTime * speed;
                    if (t >= 1f)
                    {
                        t = 1f;
                        increasing = false;
                        toggleColor = !toggleColor; // Cambia el color al llegar al máximo
                    }
                }
                else
                {
                    t -= Time.deltaTime * speed;
                    if (t <= 0f)
                    {
                        t = 0f;
                        increasing = true;
                        toggleColor = !toggleColor; // Cambia el color al llegar al mínimo
                    }
                }

                // Ajustar intensidad suavemente
                holoLight3.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
            }

            yield return null;
        }
    }


    private System.Collections.IEnumerator LightSequence()
    {
        int direction = 1; // 1 para avanzar, -1 para retroceder
        int currentIndex = 0;

        while (true)
        {
            // Resaltar el foco actual
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null)
                {
                    lights[i].intensity = (i == currentIndex) ? hologramEffectIntensity : defaultIntensity;
                    lights[i].color = (i == currentIndex) ? hologramEffectColor : defaultColor;
                }
            }

            yield return new WaitForSeconds(sequenceSpeed);

            // Cambiar al siguiente foco
            currentIndex += direction;

            // Rebotar al final de la secuencia
            if (currentIndex == lights.Length || currentIndex == -1)
            {
                if (enableBounceEffect)
                    direction *= -1; // Cambiar dirección
                else
                    currentIndex = 0; // Reiniciar la secuencia
            }
        }
    }
}
