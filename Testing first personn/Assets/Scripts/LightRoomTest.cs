﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class LightRoomTest : MonoBehaviour
{
    public GameObject subLights;
    public bool switchingOn;
    public float lightTimeOn;
    public float lightTimeOff;
    public GameObject player;
    public GameObject chaser;

    private float walkSpeedOnStart;
    private float runSpeedOnStart;

    public float walkSpeedInDarkness;
    public float runSpeedInDarkness;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightsOff());
        switchingOn = true;
        runSpeedOnStart = player.GetComponent<FirstPersonController>().m_RunSpeed;
        walkSpeedOnStart = player.GetComponent<FirstPersonController>().m_WalkSpeed;
    }

    IEnumerator LightsOn(){
        while (true){
        
        yield return new WaitForSeconds(lightTimeOff);
        switchingOn = ! switchingOn;    // bool trigger
        yield return StartCoroutine(LightsOff());
        
        }
           
    }
    IEnumerator LightsOff()
    {
        while (true)
        {

            yield return new WaitForSeconds(lightTimeOn);
            switchingOn = !switchingOn;    // bool trigger\
            yield return StartCoroutine(LightsOn());

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
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedInDarkness;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedInDarkness;
            player.GetComponent<FirstPersonController>().m_JumpAllowed = false;
            chaser.GetComponent<EnemyAI>().lightsOn = false; //Lights now simply inform the chaser that the lights are off. Chaser does the work itself now.
        } 
        if(other.gameObject.tag == "Character" && switchingOn == true) {
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_JumpAllowed = true;
            chaser.GetComponent<EnemyAI>().lightsOn = true;
        }
    }
}
