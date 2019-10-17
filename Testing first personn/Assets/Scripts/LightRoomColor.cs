using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;
using UnityEngine.Events;

public class LightRoomColor : MonoBehaviour
{
    public GameObject subLights;
    public bool switchingOn;
    public float lightTimeOnBlank;
    public float lightTimeOff;
    public float lightTimeOnColor;
    public GameObject player;
    public GameObject chaser;

    private float lighttime;
    private int stage;
    public int pattern;
    private float splitTime;
    private int colorStage;

    private Color colorRed = new Color(0.8f, 0, 0, 1);
    private Color colorOrange = new Color(1, 0.6f, 0.2f, 1);
    private Color colorYellow = new Color(0.6f, 0.6f, 0, 1);
    private Color colorWhite = new Color(1, 1, 1, 1);

    private Color[] colorSequence1 = new Color[3];
    private Color[] colorSequence2 = new Color[5];
    private Color[] colorSequence3 = new Color[7];
    private int colorCount = 0;

    public AudioSource lightOnSound;

    public UnityEvent OnLightSwitchStateOn;
    public UnityEvent OnLightSwitchStateOff;

    // Start is called before the first frame update
    void Start()
    {
        colorSequence1[0] = colorYellow;
        colorSequence1[1] = colorOrange;
        colorSequence1[2] = colorRed;

        colorSequence2[0] = colorRed;
        colorSequence2[1] = colorOrange;
        colorSequence2[2] = colorRed;
        colorSequence2[3] = colorYellow;
        colorSequence2[4] = colorRed;

        colorSequence3[0] = colorOrange;
        colorSequence3[1] = colorRed;
        colorSequence3[2] = colorYellow;
        colorSequence3[3] = colorOrange;
        colorSequence3[4] = colorRed;
        colorSequence3[5] = colorOrange;
        colorSequence3[6] = colorYellow;

        stage = 0;
        pattern = 1;
        splitTime = lightTimeOnColor / colorSequence1.Length;

        StartCoroutine(LightCycle());
        switchingOn = true;
    }

    public IEnumerator LightCycle()
    {
        while (true) {
            if (player.GetComponent<FirstPersonController>().inColorPuzzle)
            {
                lighttime += Time.deltaTime;
                if (lighttime >= lightTimeOnBlank && stage == 0 && switchingOn)
                {
                    switchingOn = !switchingOn;
                    lighttime = 0;
                    stage = 1;
                    OnLightSwitchStateOff.Invoke();
                    lightOnSound.Play();
                    foreach (Transform child in subLights.transform)
                    {
                        switch (pattern) {
                            case 1:
                                child.GetComponent<Light>().color = colorSequence1[0];
                                break;
                            case 2:
                                child.GetComponent<Light>().color = colorSequence2[0];
                                break;
                            case 3:
                                child.GetComponent<Light>().color = colorSequence3[0];
                                break;
                        }
                    }
                }
                if (lighttime >= lightTimeOff && !switchingOn)
                {
                    switchingOn = !switchingOn;
                    lighttime = 0;
                    OnLightSwitchStateOn.Invoke();
                    lightOnSound.Play();
                }
                if (stage != 0 && switchingOn)
                {
                    if (lighttime >= lightTimeOnColor)
                    {
                        switchingOn = !switchingOn;
                        lighttime = 0;
                        stage = 0;
                        OnLightSwitchStateOff.Invoke();
                        lightOnSound.Play();
                        foreach (Transform child in subLights.transform)
                        {
                            child.GetComponent<Light>().color = colorWhite;
                        }
                    }
                    if (lighttime != 0)
                    {
                        colorStage = (int)Math.Floor(lighttime / splitTime);
                        foreach (Transform child in subLights.transform)
                        {
                            switch (pattern)
                            {
                                case 1:
                                    child.GetComponent<Light>().color = colorSequence1[colorStage];
                                    break;
                                case 2:
                                    child.GetComponent<Light>().color = colorSequence2[colorStage];
                                    break;
                                case 3:
                                    child.GetComponent<Light>().color = colorSequence3[colorStage];
                                    break;
                            }
                        }
                    }
                }
            }
            yield return null;
        }
    }

    void Update()
    {
        //Turning lights on/ off through game object active status
        if (switchingOn == true){
            subLights.SetActive(true);
        }
        if (switchingOn == false){
            subLights.SetActive(false);
        }
    }
    
    void OnTriggerStay (Collider other){
        if (other.gameObject.tag == "Character" && switchingOn == false){
            //other.gameObject.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<FirstPersonController>().lightsOn = false;
            chaser.GetComponent<EnemyAI>().lightsOn = false; 
        } 
        if(other.gameObject.tag == "Character" && switchingOn == true) {
            player.GetComponent<FirstPersonController>().lightsOn = true;
            chaser.GetComponent<EnemyAI>().lightsOn = true;
        }
    }

    void DoNothing()
    {

    }
    public void ForceOn(float grace) //Lights will immediately turn on, and stay on for at least the specified number of seconds
    {
        if (stage == 0)
        {
            lightTimeOnBlank += grace;
        } else
        {
            lightTimeOnColor += grace;
        }
        if (!switchingOn)
        {
            lightTimeOff = 0;
        }
    }

    public void AdvancePattern()
    {
        pattern += 1;
        if (pattern == 2)
        {
            splitTime = lightTimeOnColor / colorSequence2.Length;
        } else
        {
            splitTime = lightTimeOnColor / colorSequence3.Length;
        }
    }
}
