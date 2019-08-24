﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LightRoomTest : MonoBehaviour
{
    public Light roomLights;
    public GameObject renameLaterroom; // rename
    public bool switchingOn;
    public float lightTimer;
    public GameObject player;

    public float walkSpeedOnStart;
    public float runSpeedOnStart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightsOnOff());
        switchingOn = true;
        runSpeedOnStart = player.GetComponent<FirstPersonController>().m_RunSpeed;
        walkSpeedOnStart = player.GetComponent<FirstPersonController>().m_WalkSpeed;
    }

    IEnumerator LightsOnOff(){
        while (true){
        
        yield return new WaitForSeconds(lightTimer);
        switchingOn = ! switchingOn;    // bool trigger
        
        }
           
    }

    
    void Update()
    {
        //Turning lights on/ off through game object active status
        if (switchingOn == true){
            renameLaterroom.SetActive(true);
        }
        if (switchingOn == false){
            renameLaterroom.SetActive(false);
        }
    }
    
    // Kills Player if they are moving inside the room with lights on - change to take damage over time everntually
    void OnTriggerStay (Collider other){
        if (other.gameObject.tag == "Character" && switchingOn == true){
            //other.gameObject.GetComponent<FirstPersonController>().enabled = false;
            Debug.Log("Player in Lights");
            player.GetComponent<FirstPersonController>().m_RunSpeed = 0;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;
        } 
        if(other.gameObject.tag == "Character" && switchingOn == false) {
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedOnStart;
            
        }
    }
}
