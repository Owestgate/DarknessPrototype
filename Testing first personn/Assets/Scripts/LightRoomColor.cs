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
    public float lightTimeOnColorH;
    public GameObject player;
    public EnemyAI chaser;

    public float lighttime;
    public int pattern;
    private int patternLength;
    private float splitTime;
    private int colorStage;

    private Color colorCyan = new Color(0, 1, 1, 1);
    private Color colorMagenta = new Color(1, 0, 1, 1);
    private Color colorYellow = new Color(1, 1, 0, 1);
    private Color colorWhite = new Color(1, 1, 1, 1);

    private Color[] colorSequence1 = new Color[3];
    private Color[] colorSequence2 = new Color[4];
    private Color[] colorSequence3 = new Color[5];

    private Color[] colorSequence1h = new Color[3];
    private Color[] colorSequence2h = new Color[5];
    private Color[] colorSequence3h = new Color[7];
    private int colorCount = 0;

    public AudioSource lightOnSound;

    public UnityEvent OnLightSwitchStateOn;
    public UnityEvent OnLightSwitchStateOff;

    // Start is called before the first frame update
    void Start()
    {
        colorSequence1[0] = colorYellow;
        colorSequence1[1] = colorMagenta;
        colorSequence1[2] = colorCyan;

        colorSequence2[0] = colorCyan;
        colorSequence2[1] = colorMagenta;
        colorSequence2[2] = colorYellow;
        colorSequence2[3] = colorCyan;

        colorSequence3[0] = colorMagenta;
        colorSequence3[1] = colorCyan;
        colorSequence3[2] = colorMagenta;
        colorSequence3[3] = colorYellow;
        colorSequence3[4] = colorMagenta;

        colorSequence1h[0] = colorMagenta;
        colorSequence1h[1] = colorYellow;
        colorSequence1h[2] = colorCyan;

        colorSequence2h[0] = colorCyan;
        colorSequence2h[1] = colorYellow;
        colorSequence2h[2] = colorCyan;
        colorSequence2h[3] = colorMagenta;
        colorSequence2h[4] = colorYellow;

        colorSequence3h[0] = colorMagenta;
        colorSequence3h[1] = colorCyan;
        colorSequence3h[2] = colorYellow;
        colorSequence3h[3] = colorCyan;
        colorSequence3h[4] = colorYellow;
        colorSequence3h[5] = colorMagenta;
        colorSequence3h[6] = colorYellow;

        pattern = 1;

        if (PlayerPrefs.GetInt("difficulty") == 0)
        {
            splitTime = lightTimeOnColor / colorSequence1.Length;
            patternLength = colorSequence1.Length;
        } else
        {
            lightTimeOnColor = lightTimeOnColorH;
            splitTime = lightTimeOnColor / colorSequence1h.Length;
            patternLength = colorSequence1h.Length;
        }
        Debug.Log("timer: " + lightTimeOnColor);

        StartCoroutine(LightCycle());
        switchingOn = true;
    }

    public IEnumerator LightCycle()
    {
        while (true) {
            if (player.GetComponent<FirstPersonController>().inColorPuzzle)
            {
                lighttime += Time.deltaTime;
                if (lighttime >= lightTimeOff && !switchingOn)
                {
                    switchingOn = !switchingOn;
                    lighttime = 0;
                    OnLightSwitchStateOn.Invoke();
                    lightOnSound.Play();
                }
                if (switchingOn)
                {
                    if (lighttime >= lightTimeOnColor + lightTimeOnBlank*2)
                    {
                        switchingOn = !switchingOn;
                        lighttime = 0;
                        OnLightSwitchStateOff.Invoke();
                        lightOnSound.Play();
                        foreach (Transform child in subLights.transform)
                        {
                            child.GetComponent<Light>().color = colorWhite;
                        }
                    }
                    if (lighttime > lightTimeOnBlank && lighttime < lightTimeOnColor + lightTimeOnBlank)
                    {
                        colorStage = (int)Math.Floor((lighttime - lightTimeOnBlank) / splitTime);
                        if (0 <= colorStage && colorStage < patternLength)
                        {
                            foreach (Transform child in subLights.transform)
                            {
                                if (PlayerPrefs.GetInt("difficulty") == 0)
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
                                } else
                                {
                                    switch (pattern)
                                    {
                                        case 1:
                                            child.GetComponent<Light>().color = colorSequence1h[colorStage];
                                            break;
                                        case 2:
                                            child.GetComponent<Light>().color = colorSequence2h[colorStage];
                                            break;
                                        case 3:
                                            child.GetComponent<Light>().color = colorSequence3h[colorStage];
                                            break;
                                    }
                                }
                            }
                        }
                    } else
                    {
                        foreach (Transform child in subLights.transform)
                        {
                            child.GetComponent<Light>().color = colorWhite;
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
            chaser.lightsOn = false; 
        } 
        if(other.gameObject.tag == "Character" && switchingOn == true) {
            player.GetComponent<FirstPersonController>().lightsOn = true;
            chaser.lightsOn = true;
        }
    }

    void DoNothing()
    {

    }
    public void ForceOn(float grace) //Lights will immediately turn on, and stay on for at least the specified number of seconds
    {
        lighttime -= grace;
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
            if (PlayerPrefs.GetInt("difficulty") == 0)
            {
                splitTime = lightTimeOnColor / colorSequence2.Length;
                patternLength = 4;
            } else
            {
                splitTime = lightTimeOnColor / colorSequence2h.Length;
                patternLength = 5;
            }
        } else
        {
            if (PlayerPrefs.GetInt("difficulty") == 0)
            {
                splitTime = lightTimeOnColor / colorSequence3.Length;
                patternLength = 5;
            } else
            {
                splitTime = lightTimeOnColor / colorSequence3h.Length;
                patternLength = 7;
            }
        }
    }

    public void ForceOff(float delay)
    {
        lighttime = 0;
        lightTimeOff = delay;
    }

}
