using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

public class GrainByLightState : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    public float grainAmountOff;
    public float grainAmountOn;

    public float legacyNoiseOff;
    public float legacyNoiseOn;

    private Grain grain;
    public NoiseAndScratches legacyNoise;

    private void Start()
    {
        RoomLights.Instance.OnLightSwitchStateOn.AddListener(OnLightSwitchOn);
        RoomLights.Instance.OnLightSwitchStateOff.AddListener(OnLightSwitchOff);
    }

    private void OnDestroy()
    {
        RoomLights.Instance.OnLightSwitchStateOn.RemoveListener(OnLightSwitchOn);
        RoomLights.Instance.OnLightSwitchStateOff.RemoveListener(OnLightSwitchOff);
    }

    void OnLightSwitchOn()
    {
        if (postProcessVolume.profile.TryGetSettings(out grain))
        {
            grain.intensity.value = grainAmountOff;
        }

        legacyNoise.grainIntensityMin = legacyNoiseOff;
        legacyNoise.grainIntensityMax = legacyNoiseOff;
    }

    void OnLightSwitchOff()
    {
        if (postProcessVolume.profile.TryGetSettings(out grain))
        {
            grain.intensity.value = grainAmountOn;
        }

        legacyNoise.grainIntensityMin = legacyNoiseOn;
        legacyNoise.grainIntensityMax = legacyNoiseOn;
    }
}
