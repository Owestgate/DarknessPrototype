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
        lightTimeOffWait = new WaitForSeconds(lightTimeOff);

        lightTimeOn = 3;
        lightTimeOnWait = new WaitForSeconds(lightTimeOn);

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
                yield return lightTimeOnWait;
                if (gracePeriodActive)
                {
                    yield return new WaitForSeconds(graceTimer);
                    gracePeriodActive = false;
                }
                switchingOn = false;
                OnLightSwitchStateOff.Invoke();
                yield return lightTimeOffWait;
                switchingOn = true;
                OnLightSwitchStateOn.Invoke();
            }
            else
            {
                yield return null;
            }
        }
    }

    void LightSwitchStateOn()
    {
        Debug.Log("Lights on");
        lightTimeOff = Random.Range(lightTimeOffRange.x, lightTimeOffRange.y);
        lightTimeOffWait = new WaitForSeconds(lightTimeOff);
        SetSublightsState(true);
        lightOnSound.Play();
        UpdatePlayerMovementAttributes();
    }

    void UpdatePlayerMovementAttributes()
    {
        if (fpsController.inBypass)
        {
            fpsController.lightsOn = true;
        } else
        {
            fpsController.lightsOn = switchingOn ? true : false;
        } //Player handles movement speed changes
        /*fpsController.m_RunSpeed = switchingOn ? runSpeedOnStart : runSpeedInDarkness;
        fpsController.m_WalkSpeed = switchingOn ? walkSpeedOnStart : walkSpeedInDarkness;
        fpsController.m_JumpAllowed = switchingOn ? true : false;*/
        chaser.lightsOn = switchingOn ? true : false; //Lights now simply inform the chaser that the lights are off. Chaser does the work itself now.
    }

    void LightSwitchStateOff()
    {
        Debug.Log("Lights off");
        lightTimeOn = Random.Range(lightTimeOnRange.x, lightTimeOnRange.y);
        lightTimeOnWait = new WaitForSeconds(lightTimeOn);
        SetSublightsState(false);
        lightOffSound.Play();
        UpdatePlayerMovementAttributes();
    }

    public void SetSublightsState(bool on)
    {
        for (int i = 0; i < subLights.Length; i++)
        {
            subLights[i].SetActive(on);
        }
    }
}
