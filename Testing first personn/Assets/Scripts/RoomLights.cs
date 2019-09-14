using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class RoomLights : MonoBehaviour
{
    public static RoomLights Instance { get; private set; }

    public bool switchingOn;
    public bool bypass;
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

    private float walkSpeedOnStart;
    private float runSpeedOnStart;

    public float walkSpeedInDarkness;
    public float runSpeedInDarkness;

    // Add listeners for checking if certain lights should be enabled/disabled.
    public UnityEvent OnLightSwitchStateOn;
    public UnityEvent OnLightSwitchStateOff;

    private Coroutine LightsCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OnLightSwitchStateOn.AddListener(LightSwitchStateOn);
        OnLightSwitchStateOff.AddListener(LightSwitchStateOff);
        LightsCoroutine = StartCoroutine(LightsStateTimer());
        switchingOn = true;
        runSpeedOnStart = fpsController.m_RunSpeed;
        walkSpeedOnStart = fpsController.m_WalkSpeed;
    }

    private void OnDestroy()
    {
        OnLightSwitchStateOn.RemoveListener(LightSwitchStateOn);
        OnLightSwitchStateOff.RemoveListener(LightSwitchStateOff);
    }

    IEnumerator LightsStateTimer()
    {
        while (true)
        {
            if (!bypass)
            {
                yield return lightTimeOffWait;
                switchingOn = true;
                OnLightSwitchStateOn.Invoke();
                yield return lightTimeOnWait;
                switchingOn = false;
                OnLightSwitchStateOff.Invoke();
            }
            else
            {
                yield return null;
            }
        }
    }

    void LightSwitchStateOn()
    {
        lightTimeOff = Random.Range(lightTimeOffRange.x, lightTimeOffRange.y);
        lightTimeOffWait = new WaitForSeconds(lightTimeOff);
        SetSublightsState(true);
        lightOnSound.Play();
        UpdatePlayerMovementAttributes();
    }

    void UpdatePlayerMovementAttributes()
    {
        fpsController.m_RunSpeed = switchingOn ? runSpeedOnStart : runSpeedInDarkness;
        fpsController.m_WalkSpeed = switchingOn ? walkSpeedOnStart : walkSpeedInDarkness;
        fpsController.m_JumpAllowed = switchingOn ? true : false;
        chaser.lightsOn = switchingOn ? true : false; //Lights now simply inform the chaser that the lights are off. Chaser does the work itself now.
    }

    void LightSwitchStateOff()
    {
        lightTimeOn = Random.Range(lightTimeOnRange.x, lightTimeOnRange.y);
        lightTimeOnWait = new WaitForSeconds(lightTimeOn);
        SetSublightsState(false);
        lightOffSound.Play();
        UpdatePlayerMovementAttributes();
    }

    void SetSublightsState(bool on)
    {
        for (int i = 0; i < subLights.Length; i++)
        {
            subLights[i].SetActive(on);
        }
    }
}
