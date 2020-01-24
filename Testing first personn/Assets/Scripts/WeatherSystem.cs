using UnityEngine;
using UnityStandardAssets.Utility;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Events;

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem instance { get; private set; }

    public SunShafts sunShaftsScript;

    [Header("Lightning")]
    public Animator LightFlash;
    public ParticleSystem Lightning;

    public Vector2 LightningTime = new Vector2(30, 60);
    public float NewLightningTime;

    public AudioSource Thunder;
    public AudioClip[] ThunderSounds;
    public Vector2 ThunderPitchRange = new Vector2(0.5f, 1.25f);

    [Header("Rain and clouds")]
    public ParticleSystem Rain;
    public AudioSource RainLoop;

    [Header("Sun")]
    public Light Sun;
    public AutoMoveAndRotate SunAutoRotateScript;

    [Header("Wind")]
    public WindZone GlobalWind;
	public UnityEvent OnLightning;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetLightningTime();
    }

    void Update()
    {
        CheckLightning();
    }

    void CheckLightning()
    {
       
        if (NewLightningTime > 0)
        {
            NewLightningTime -= Time.deltaTime;
            float targetRainVol = Mathf.Clamp(0.125f, 0, 1);
            return;
        }

        else

        {
            DoLightning();
            ResetLightningTime();
        }
       
    }

    public void DoLightning()
    {
        LightFlash.SetTrigger("Flash");
        Lightning.Play();

        if (Thunder.isPlaying == false)
        {
            Thunder.clip = ThunderSounds[Random.Range(0, ThunderSounds.Length)];
            Thunder.pitch = Random.Range(ThunderPitchRange.x, ThunderPitchRange.y);
            Thunder.Play();
        }

		OnLightning.Invoke();
    }
    public void ResetLightningTime()
    {
        NewLightningTime = Random.Range(LightningTime.x, LightningTime.y);
    }
}