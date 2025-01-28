using System.Collections;
using UnityEngine;

public class SuperObjectBehavior : MonoBehaviour
{
    // -------------------- Enums y Variables --------------------
    
    public enum AnimationType { None, FadeIn, FadeOut, Scale }

    [Header("Configuraciones Generales")]
    [Tooltip("Distancia máxima antes de que el objeto regrese a su posición inicial.")]
    public float maxDistance = 5f; 
    
    [Tooltip("Si está habilitado, el objeto regresará automáticamente después de exceder la distancia o el tiempo de inactividad.")]
    public bool enableReturn = true; 
    
    [Tooltip("Tiempo de inactividad antes de que el objeto regrese.")]
    public float idleTimeBeforeReturn = 5f; 
    
    [Tooltip("Si quieres que el objeto se mueva a un 'targetPoint' en lugar de a su posición inicial, actívalo y asigna 'targetPoint'.")]
    public bool useTargetPoint = false; 
    public Transform targetPoint;  // Punto de destino opcional

    [Header("Aparición y Desaparición")]
    [Tooltip("¿Debe aparecer con un efecto (fade-in, scale, etc.)?")]
    public bool enableAppearance = true; 
    
    [Tooltip("Duración de la animación de aparición.")]
    public float appearanceTime = 1f; 
    
    [Tooltip("¿Debe desaparecer con un efecto (fade-out, scale, etc.)?")]
    public bool enableDisappearance = true; 
    
    [Tooltip("Duración de la animación de desaparición.")]
    public float disappearanceTime = 1f; 
    
    [Tooltip("Tipo de efecto de aparición.")]
    public AnimationType appearanceEffect = AnimationType.FadeIn; 
    
    [Tooltip("Tipo de efecto de desaparición.")]
    public AnimationType disappearanceEffect = AnimationType.FadeOut;

    [Header("Resalte e Interacción")]
    [Tooltip("Material de resaltado cuando el objeto está seleccionado.")]
    public Material highlightMaterial; 
    
    [Tooltip("Material que se aplica si el objeto llega correctamente a su destino.")]
    public Material correctPlacementMaterial; 
    
    [Tooltip("Partículas que se activan al colocar el objeto correctamente.")]
    public ParticleSystem placementParticles; 
    
    [Tooltip("Efecto de partículas al colisionar con alguna superficie, si lo deseas.")]
    public ParticleSystem collisionParticles;

    private Material originalMaterial; 
    private Renderer objectRenderer; 
    
    [Header("Indicadores Visuales")]
    [Tooltip("¿Mostrar una línea que conecte el objeto con su punto de destino (si está asignado)?")]
    public bool showGuideLine = true; 
    public LineRenderer guideLine; // Línea que conecta el objeto con su destino

    [Header("Indicaciones Adicionales")]
    [Tooltip("Texto flotante o UI que aparezca al acercarse al objeto.")]
    public GameObject floatingTextPrefab; 
    [Tooltip("Posición opcional donde se instanciará el texto flotante.")]
    public Transform floatingTextSpawnPoint; 
    [Tooltip("Duración (segundos) antes de que se destruya el texto flotante.")]
    public float floatingTextDuration = 2f;

    // Control interno
    private Vector3 initialPosition; 
    private Rigidbody rb; 
    private bool isSelected = false; 
    private float idleTimer = 0f;

    // -------------------- Métodos Principales --------------------

    void Start()
    {
        // Guardar la posición inicial.
        initialPosition = transform.position;
        
        // Obtener componentes necesarios.
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
            originalMaterial = objectRenderer.material;

        // Configurar la física "pesada" (conservando tu comportamiento).
        if (rb != null)
        {
            rb.mass = 100f;
            rb.linearDamping = 5f;
            rb.angularDamping = 10f;
            rb.useGravity = false; // Evita que caiga
        }

        // Configurar la aparición inicial (si está habilitada).
        if (enableAppearance)
        {
            StartCoroutine(HandleAppearance());
        }

        // Configurar la línea de guía (si se desea).
        if (showGuideLine && guideLine != null && targetPoint != null)
        {
            guideLine.enabled = true;
            guideLine.SetPosition(0, transform.position);
            guideLine.SetPosition(1, targetPoint.position);
        }
    }

    void Update()
    {
        // Actualizar la línea de guía en tiempo real (opcional).
        if (showGuideLine && guideLine != null && targetPoint != null)
        {
            guideLine.SetPosition(0, transform.position);
            guideLine.SetPosition(1, targetPoint.position);
        }

        // Resalte visual al interactuar.
        HandleHighlight();

        // Manejo de distancia máxima y tiempo de inactividad.
        HandleReturnLogic();
    }

    // -------------------- Interacciones del Mouse --------------------
    // Estos métodos usan OnMouseEnter/OnMouseExit, válidos en un contexto 3D con Collider y Raycast de la cámara.

    void OnMouseEnter()
    {
        isSelected = true;
        idleTimer = 0f;
        // Instanciar texto flotante o UI si se desea.
        ShowFloatingText();
    }

    void OnMouseExit()
    {
        isSelected = false;
    }

    // -------------------- Funciones Principales --------------------

    /// <summary>
    /// Mueve el objeto manualmente a una nueva posición, reseteando el temporizador de inactividad.
    /// </summary>
    public void MoveManually(Vector3 newPosition)
    {
        transform.position = newPosition;
        idleTimer = 0f;
    }

    /// <summary>
    /// Lógica de retorno al punto inicial o al 'targetPoint' si está activo.
    /// </summary>
    private void HandleReturnLogic()
    {
        float distanceFromInitial = Vector3.Distance(transform.position, initialPosition);

        // Si excede la distancia máxima, regresa inmediatamente.
        if (distanceFromInitial > maxDistance && enableReturn)
        {
            if (useTargetPoint && targetPoint != null)
                StartCoroutine(MoveToTarget(targetPoint.position));
            else
                StartCoroutine(MoveToTarget(initialPosition));
        }

        // Si no se está seleccionado, aumentar el contador de inactividad.
        if (!isSelected && enableReturn)
        {
            idleTimer += Time.deltaTime;

            // Si pasa el tiempo de inactividad, moverse automáticamente.
            if (idleTimer >= idleTimeBeforeReturn)
            {
                if (useTargetPoint && targetPoint != null)
                    StartCoroutine(MoveToTarget(targetPoint.position));
                else
                    StartCoroutine(MoveToTarget(initialPosition));
            }
        }
    }

    /// <summary>
    /// Aplica el material de resaltado cuando está seleccionado; caso contrario, material original.
    /// </summary>
    private void HandleHighlight()
    {
        if (objectRenderer == null) return;

        if (isSelected && highlightMaterial != null)
        {
            objectRenderer.material = highlightMaterial;
        }
        else
        {
            objectRenderer.material = originalMaterial;
        }
    }

    // -------------------- Corutinas de Movimiento --------------------

    /// <summary>
    /// Mueve el objeto suavemente hasta la posición objetivo usando un Lerp.
    /// </summary>
    private IEnumerator MoveToTarget(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float journey = 0f;

        while (journey < 1f)
        {
            journey += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target, journey);
            yield return null;
        }

        // Una vez llegado al destino, reproducir partículas y aplicar material "correctPlacement" si lo deseas.
        if (placementParticles != null)
        {
            placementParticles.Play();
        }

        if (correctPlacementMaterial != null && objectRenderer != null)
        {
            objectRenderer.material = correctPlacementMaterial;
        }

        // Si después de moverse quieres que desaparezca, puedes llamar a la corutina de HandleDisappearance:
        if (enableDisappearance)
        {
            StartCoroutine(HandleDisappearance());
        }
    }

    // -------------------- Corutinas de Aparición y Desaparición --------------------

    /// <summary>
    /// Maneja la animación de aparición (fade-in, scale, etc.).
    /// </summary>
    private IEnumerator HandleAppearance()
    {
        // Por si en el Inspector no pones un efecto en particular.
        if (appearanceEffect == AnimationType.None) yield break;

        // Esperar al siguiente frame para evitar conflictos con Start().
        yield return null;

        // Fade In
        if (appearanceEffect == AnimationType.FadeIn && objectRenderer != null)
        {
            float elapsedTime = 0f;
            SetObjectAlpha(0f); // Dejar el objeto invisible al principio

            while (elapsedTime < appearanceTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / appearanceTime);
                SetObjectAlpha(alpha);
                yield return null;
            }
        }
        // Scale
        else if (appearanceEffect == AnimationType.Scale)
        {
            float elapsedTime = 0f;
            Vector3 initialScale = Vector3.zero;
            Vector3 finalScale = transform.localScale;
            transform.localScale = initialScale;

            while (elapsedTime < appearanceTime)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / appearanceTime);
                yield return null;
            }
        }
    }

    /// <summary>
    /// Maneja la animación de desaparición (fade-out, scale, etc.).
    /// </summary>
    private IEnumerator HandleDisappearance()
    {
        if (!enableDisappearance) yield break; // Si no está habilitado, no hacer nada.
        if (disappearanceEffect == AnimationType.None) yield break;

        // Fade Out
        if (disappearanceEffect == AnimationType.FadeOut && objectRenderer != null)
        {
            float elapsedTime = 0f;
            float currentAlpha = objectRenderer.material.color.a;

            while (elapsedTime < disappearanceTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(currentAlpha, 0f, elapsedTime / disappearanceTime);
                SetObjectAlpha(alpha);
                yield return null;
            }

            // Finalmente, desactivar el objeto.
            gameObject.SetActive(false);
        }
        // Scale
        else if (disappearanceEffect == AnimationType.Scale)
        {
            float elapsedTime = 0f;
            Vector3 startScale = transform.localScale;

            while (elapsedTime < disappearanceTime)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsedTime / disappearanceTime);
                yield return null;
            }

            // Finalmente, desactivar el objeto.
            gameObject.SetActive(false);
        }
    }

    // -------------------- Manejo de Colisiones y Efectos --------------------

    /// <summary>
    /// Si quieres que el objeto reproduzca un efecto de partículas al colisionar con algo.
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collisionParticles != null)
        {
            collisionParticles.Play();
        }
        // Lógica extra: sonido, bounce, etc.
    }

    // -------------------- Utilidades --------------------

    /// <summary>
    /// Ajusta el valor alpha de los materiales del objeto. Necesita un shader que soporte transparencia.
    /// </summary>
    private void SetObjectAlpha(float alphaValue)
    {
        if (objectRenderer == null) return;

        Color c = objectRenderer.material.color;
        c.a = alphaValue;
        objectRenderer.material.color = c;
    }

    /// <summary>
    /// Instancia un texto flotante (o UI) al entrar en el objeto, para guiar al usuario.
    /// </summary>
    private void ShowFloatingText()
    {
        if (floatingTextPrefab == null) return;

        Vector3 spawnPos = (floatingTextSpawnPoint != null)
            ? floatingTextSpawnPoint.position
            : transform.position + Vector3.up * 1.5f;

        GameObject textObj = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);
        Destroy(textObj, floatingTextDuration); // Se elimina tras X segundos
    }
}
