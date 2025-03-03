using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    public HologramLightManager lightManager;

    // Toggle para encender/apagar cada nivel
    public void ToggleLevel1(bool isOn) => lightManager.ToggleLevel(1, isOn);
    public void ToggleLevel2(bool isOn) => lightManager.ToggleLevel(2, isOn);
    public void ToggleLevel3(bool isOn) => lightManager.ToggleLevel(3, isOn);

    // Encender/Apagar todas las luces
    public void ToggleAllLights(bool isOn) => lightManager.ToggleAllLights(isOn);

    // Secuencial para cada nivel
    public void ToggleSequentialLevel1(bool isOn) => lightManager.ToggleSequentialLevel(1, isOn);
    public void ToggleSequentialLevel2(bool isOn) => lightManager.ToggleSequentialLevel(2, isOn);
    public void ToggleSequentialLevel3(bool isOn) => lightManager.ToggleSequentialLevel(3, isOn);

    // Parpadeo para cada nivel
    public void ToggleBlinkLevel1(bool isOn, float interval) => lightManager.ToggleBlinkLevel(1, isOn, interval);
    public void ToggleBlinkLevel2(bool isOn, float interval) => lightManager.ToggleBlinkLevel(2, isOn, interval);
    public void ToggleBlinkLevel3(bool isOn, float interval) => lightManager.ToggleBlinkLevel(3, isOn, interval);

    // Activar/Desactivar por completo el Point Light
    public void EnablePointLightLevel1() => lightManager.EnablePointLight(1);
    public void DisablePointLightLevel1() => lightManager.DisablePointLight(1);

    public void EnablePointLightLevel2() => lightManager.EnablePointLight(2);
    public void DisablePointLightLevel2() => lightManager.DisablePointLight(2);

    public void EnablePointLightLevel3() => lightManager.EnablePointLight(3);
    public void DisablePointLightLevel3() => lightManager.DisablePointLight(3);

    // Detener cualquier parpadeo activo
    public void StopBlinking() => lightManager.StopBlinking();


    public void ToggleChargingEffectLevel3(bool isOn)
    {
        lightManager.ToggleChargingEffect(3, isOn, Color.green, Color.red, 0.2f, 18f, 5f);
    }

}
