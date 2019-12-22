using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SaturationByLightState : MonoBehaviour
{
    public static SaturationByLightState Instance { get; private set; }
    public PostProcessVolume postProcessVolume;

    public float saturationOnAmount = 35;
    public float saturationOffAmount = -100;

    private ColorGrading colorGrading;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (RoomLights.Instance)
        {
            RoomLights.Instance.OnLightSwitchStateOn.AddListener(OnLightSwitchOn);
            RoomLights.Instance.OnLightSwitchStateOff.AddListener(OnLightSwitchOff);
        }
    }

    private void OnDestroy()
    {
        if (RoomLights.Instance)
        {
            RoomLights.Instance.OnLightSwitchStateOn.RemoveListener(OnLightSwitchOn);
            RoomLights.Instance.OnLightSwitchStateOff.RemoveListener(OnLightSwitchOff);
        }
    }

    public void OnLightSwitchOn()
    {
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading.saturation.value = saturationOnAmount;
        }
    }

    void OnLightSwitchOff()
    {
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            colorGrading.saturation.value = saturationOffAmount;
        }
    }
}
