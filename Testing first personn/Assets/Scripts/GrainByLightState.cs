using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

public class GrainByLightState : MonoBehaviour
{
    public static GrainByLightState Instance { get; private set; }
    public PostProcessVolume postProcessVolume;

    public float grainAmountOff;
    public float grainAmountOn;

    public float legacyNoiseOff;
    public float legacyNoiseOn;

    private Grain grain;
    public NoiseAndScratches legacyNoise;
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
