using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteAmountByDistance : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Transform chaser;

    public Vector2 vignetteAmountRange = new Vector2(0.185f, 0.4f);

    public float distance;
    public float distanceProportion;
    public Vector2 distanceRange = new Vector2(1, 20);

    private Vignette vignette;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, chaser.position);
        distanceProportion = Mathf.InverseLerp(distanceRange.x, distanceRange.y, distance);
        
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = Remap(1 - distanceProportion, 0, 1, 
                vignetteAmountRange.x,
                vignetteAmountRange.y);

            vignette.intensity.value = Mathf.Clamp(
                vignette.intensity.value,
                vignetteAmountRange.x,
                vignetteAmountRange.y);
        }
    }

    private static float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
