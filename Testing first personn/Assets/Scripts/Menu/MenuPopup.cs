﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this here so Button can be used
using UnityStandardAssets.Characters.FirstPerson; // so movement can be disabled on pause


public class MenuPopup : MonoBehaviour
{

    public GameObject menuPopup; // Menu Canvas Object - menu hidden on start
    public bool menuOpen; //check if menu already open

    public bool cursorLock;
    //public Script firstPerson;
    public bool pauseDelayed;

    public GameObject paintUIController;

    private FirstPersonController fpsController;

    
    
    void Start()
    {
 
       Cursor.lockState = CursorLockMode.Locked;
       cursorLock = true;    
       //StartCoroutine(DelayPause());
       pauseDelayed = false; /////
    }

    private void Awake()
    {
       fpsController = gameObject.GetComponent<FirstPersonController>();
       StartCoroutine(DelayPause());
    }

    IEnumerator DelayPause(){
        yield return new WaitForSeconds(8.5f);
        pauseDelayed = true;
    }

    //Unpauses game from the return button on pause menu panel
    public void ReturnButton(){
        if(menuOpen == true){
            menuPopup.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1; // Unpauses game time
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.GetComponent<FirstPersonController>().enabled = true; // Enables character to move again
            cursorLock = true;
        }


    }

   

    void Update()
    {
                
        
        //OPENS MENU - cant open in intro or death or painting
        if(Input.GetKeyDown(KeyCode.Escape) && menuOpen == false && pauseDelayed == true && this.gameObject.GetComponent<KillScreen>().cantPause == false && paintUIController.GetComponent<PaintingUI>().cantPauseNow == false){
            menuPopup.SetActive(true);
            menuOpen = true;
            Time.timeScale = 0; // Pauses Game time
            cursorLock = false;
            gameObject.GetComponent<FirstPersonController>().enabled = false; // disables character movement while paused
        }

        

        if (cursorLock == true){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (cursorLock == false){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(pauseDelayed == false){
            //Cursor.lockState = CursorLockMode.Locked;
        }
    

        
    }
}
