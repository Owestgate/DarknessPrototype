﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using EZCameraShake;

public class KillScreen : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject playerObj;
    public GameObject roomLights;
    public GameObject colorLights;
    //Name of the screen to transition to
    public string killScreen;
    //If currentDist is less than this, you die.
    public float killDist;
    //Distance between player and chaser.
    private float currentDist;

    //Jumpscare testing 
    public GameObject jumpScarePosition;
    public preserveNoise jumpScareAudioObject; // game object with audio source set to play on awake
    public GameObject playerCamera;    
    public GameObject jumpScareLookAt;
    public GameObject postProcessing;

    private ChromaticAberration chroma;
    private Vignette vig;

    public GameObject jumpScarePos2;
    public bool jumpScare2 = false;

    private float silenceTime;
    private float silenceTimer = 4;
    private bool scaring = false;

    public UnityEvent OnDie;
    public bool cantPause = false;
    public GameObject abilityUI;

    void Start()
    {
        postProcessing.GetComponent<PostProcessVolume>().profile.TryGetSettings(out chroma);
        postProcessing.GetComponent<PostProcessVolume>().profile.TryGetSettings(out vig);
        jumpScareAudioObject = preserveNoise.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the current distance, compares, then kills
        currentDist = Vector3.Distance(playerObj.transform.position, enemyObj.transform.position);
        if (currentDist < killDist && !roomLights.GetComponent<RoomLights>().switchingOn && !scaring)
        {
            silenceTime = silenceTimer;
            roomLights.GetComponent<RoomLights>().ForceOff(silenceTimer);
            if (!colorLights.GetComponent<LightRoomColor>().switchingOn)
            {
                colorLights.GetComponent<LightRoomColor>().ForceOff(silenceTimer);
            }
            scaring = true;
        }

        if (scaring)
        {
            silenceTime -= Time.deltaTime;
            if (silenceTime <= 0)
            {
                silenceTime = 0;
                StartCoroutine(Lookat());
                FirstPersonController fpsController = playerObj.GetComponent<FirstPersonController>();
                //playerObj.transform.position = jumpScarePosition.transform.position; // teleports player into position infront of enemy -- testint new one
                playerObj.transform.position = jumpScarePos2.transform.position;    // Testing new model/position
                fpsController.enabled = false; // Turns off player controller so it locks player looking at enemy
                //playerCamera.transform.LookAt(jumpScareLookAt.transform); // looks at enemy (seperate object attached to enemy that is positioned better)
                if (!jumpScareAudioObject.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    jumpScareAudioObject.gameObject.GetComponent<AudioSource>().Play(); 
                }
                CameraShaker.Instance.ShakeOnce(6f, 3f, .1f, .2f);
                jumpScare2 = true;

                Invoke("LoadScreen", 2.0f); // auto loads menu after delay
                OnDie.Invoke();
                SaturationByLightState.Instance.OnLightSwitchOn();
                GrainByLightState.Instance.OnLightSwitchOn();
                RoomLights.Instance.bypass = true;
                RoomLights.Instance.SetSublightsState(true);
                RoomLights.Instance.StopCoroutine(RoomLights.Instance.LightsCoroutine);
                RoomLights.Instance.enabled = false;
                colorLights.GetComponent<LightRoomColor>().lightTimeOnColor = 100;
                colorLights.GetComponent<LightRoomColor>().lightTimeOnBlank = 100;
                enemyObj.GetComponent<NavMeshAgent>().enabled = false;

                cantPause = true;
            }
        }
    }

    IEnumerator Lookat(){             // fix for not looking
        yield return new WaitForSeconds(0.1f);
        playerCamera.transform.LookAt(jumpScareLookAt.transform);
        chroma.intensity.value += Time.deltaTime;
        vig.intensity.value += Time.deltaTime;
        abilityUI.SetActive(false);
    }
    
    void LoadScreen()
    {
        Invoke("LoadSceneDelayed", 3);
    }

    void LoadSceneDelayed()
    {
        SceneManager.LoadScene(killScreen);
    }
}
