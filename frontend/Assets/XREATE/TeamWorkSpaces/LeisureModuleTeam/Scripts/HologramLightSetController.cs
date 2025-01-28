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
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null)
            {
                lights[i].color = defaultColor;
                lights[i].intensity = defaultIntensity;
            }
        }

        foreach (var particle in particles)
        {
            if (particle != null)
                particle.Stop();
        }
    }

    // Cambiar propiedades de un foco específico
    public void SetLightProperties(int index, Color color, float intensity)
    {
        if (index < 0 || index >= lights.Length) return;
        if (lights[index] != null)
        {
            lights[index].color = color;
            lights[index].intensity = intensity;
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
