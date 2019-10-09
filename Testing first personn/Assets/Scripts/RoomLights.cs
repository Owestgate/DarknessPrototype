using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class RoomLights : MonoBehaviour
{
    public static RoomLights Instance { get; private set; }

    public bool switchingOn;
    public bool bypass;
    public bool gracePeriodActive;
    public float graceTimer;
    public FirstPersonController fpsController;
    public EnemyAI chaser;
    public GameObject[] subLights;

    // Light ON state.
    public float lightTimeOn;
    public Vector2 lightTimeOnRange;
    private WaitForSeconds lightTimeOnWait;
    public AudioSource lightOnSound;

    // Light OFF state.
    public float lightTimeOff;
    public Vector2 lightTimeOffRange;
    private WaitForSeconds lightTimeOffWait;
    public AudioSource lightOffSound;
    private float lighttime;

    //private float walkSpeedOnStart;
    //private float runSpeedOnStart;
    
    //public float walkSpeedInDarkness;
    //public float runSpeedInDarkness;

    // Add listeners for checking if certain lights should be enabled/disabled.
    public UnityEvent OnLightSwitchStateOn;
    public UnityEvent OnLightSwitchStateOff;

    public Coroutine LightsCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OnLightSwitchStateOn.AddListener(LightSwitchStateOn);
        OnLightSwitchStateOff.AddListener(LightSwitchStateOff);

        lightTimeOff = 5;

        lightTimeOn = 3;

        lighttime = 0;

        gracePeriodActive = false;

        LightsCoroutine = StartCoroutine(LightsStateTimer());
        switchingOn = true;
        /*runSpeedOnStart = fpsController.m_RunSpeed;
        walkSpeedOnStart = fpsController.m_WalkSpeed;*/
    }

    private void OnDestroy()
    {
        OnLightSwitchStateOn.RemoveListener(LightSwitchStateOn);
        OnLightSwitchStateOff.RemoveListener(LightSwitchStateOff);
    }

    public IEnumerator LightsStateTimer()
    {
        while (true)
        {
            if (!fpsController.inBypass)
            {
                lighttime += Time.deltaTime;
                // Debug.Log("lighttime: " + lighttime);
                // Debug.Log("lightTimeOn: " + lightTimeOn);
                if (lighttime >= lightTimeOn && switchingOn)
                {
                    OnLightSwitchStateOff.Invoke();
                }
                if (lighttime >= lightTimeOff && !switchingOn)
                {
                    OnLightSwitchStateOn.Invoke();
                }
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    void LightSwitchStateOn()
    {
        switchingOn = true;
        Debug.Log("Lights on");
        lightTimeOff = Random.Range(lightTimeOffRange.x, lightTimeOffRange.y);
        SetSublightsState(true);
        lightOnSound.Play();
        UpdatePlayerMovementAttributes();
        lighttime = 0;

    }

    void UpdatePlayerMovementAttributes()
    {
        if (fpsController.inBypass)
        {
            fpsController.lightsOn = true;
        } else
        {
            fpsController.lightsOn = switchingOn;
        } //Player handles movement speed changes
        /*fpsController.m_RunSpeed = switchingOn ? runSpeedOnStart : runSpeedInDarkness;
        fpsController.m_WalkSpeed = switchingOn ? walkSpeedOnStart : walkSpeedInDarkness;
        fpsController.m_JumpAllowed = switchingOn ? true : false;*/
        chaser.lightsOn = switchingOn; //Lights now simply inform the chaser that the lights are off. Chaser does the work itself now.
    }

    void LightSwitchStateOff()
    {
        Debug.Log("Lights off");
        switchingOn = false;
        lightTimeOn = Random.Range(lightTimeOnRange.x, lightTimeOnRange.y);
        SetSublightsState(false);
        lightOffSound.Play();
        UpdatePlayerMovementAttributes();
        lighttime = 0;
    }

    public void SetSublightsState(bool on)
    {
        for (int i = 0; i < subLights.Length; i++)
        {
            subLights[i].SetActive(on);
        }
    }

    public void GraceTime(int grace) //Extends the time the lights are on next by the specified number of seconds
    {
        lightTimeOn += grace;
    }

    public void ForceOn(int grace) //Lights will immediately turn on, and stay on for at least the specified number of seconds
    {
        lightTimeOn += grace;
        lightTimeOff = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GraceTime(5);
            Debug.Log("5 seconds added");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ForceOn(5);
            Debug.Log("Lights locked on");
        }
    }
}
