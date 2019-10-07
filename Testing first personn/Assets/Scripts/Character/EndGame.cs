﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    
    public GameObject cameraObj;

    public GameObject enemy;
    public GameObject player;

    public GameObject endGameUI;


    
    void OnTriggerEnter(Collider plyr){
        if(plyr.gameObject.tag == "Character"){

            cameraObj.GetComponent<BloomOptimized>().enabled = true;            
            cameraObj.SendMessage("EndGame");
                       
            player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<MenuPopup>().cursorLock = false;

            enemy.SetActive(false);           

            endGameUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }   
    }

    
    
}
