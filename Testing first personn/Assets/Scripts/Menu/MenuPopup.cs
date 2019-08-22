using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this here so Button can be used
using UnityStandardAssets.Characters.FirstPerson; // so movement can be disabled on pause


public class MenuPopup : MonoBehaviour
{

    public GameObject menuPopup; // Menu Canvas Object - menu hidden on start
    public bool menuOpen; //check if menu already open
    public AudioListener listener; //Attack camera to this in inspector
    public bool muted; //check if muted

    public bool cursorLock;
    //public Script firstPerson;
    
   // public Button muteButton; //trying to change color through script, not working
   // public ColorBlock colorChange;
    
    
    void Start()
    {
       muted = false;
       Cursor.lockState = CursorLockMode.Locked;
       cursorLock = true;
       
       
    }

    //Unpauses game from the return button on pause menu panel
    public void ReturnButton(){
        if(menuOpen == true){
            menuPopup.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1; // Unpauses game time (recheck)
            Cursor.lockState = CursorLockMode.Locked;
            this.gameObject.GetComponent<FirstPersonController>().enabled = true; // Enables character to move again
        }

    }

    // Mutes game - applied to mute button in pause menu, checks if game is already muted
    public void MuteGame(){
        if(muted == false){
           //colorChange.pressedColor = Color.green; //trying to change color through script, not working
           //colorChange.selectedColor = Color.green;    //
           //colorChange.normalColor = Color.green;      //
           //colorChange.disabledColor = Color.green;    //
           //colorChange.highlightedColor = Color.green; //
           //colorChange.colorMultiplier = 1.0f;         //
           //muteButton.colors = colorChange; //trying to change color through script, not working
            AudioListener.volume = 0f;
            muted = true;
        }
    
        else if(muted == true){
        AudioListener.volume = 1f; // resets volume
        muted = false;
        }        
    }

    void Update()
    {
        // Opens and closes in game menu
        /*          Commented so you have to press return to unpause - problems with game not locking cursor
        if(Input.GetKeyDown(KeyCode.Escape) && menuOpen == true){
            menuPopup.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1; // Unpauses game time (recheck)
            //cursorLock = true;
            
            
        }*/
        //OPENS MENU - potentially change to a menu animation instead of a set active eg. menu animates onto screen in 1 frame
        if(Input.GetKeyDown(KeyCode.Escape) && menuOpen == false){
            menuPopup.SetActive(true);
            menuOpen = true;
            Time.timeScale = 0; // Pauses Game time (recheck)
            cursorLock = false;
            this.gameObject.GetComponent<FirstPersonController>().enabled = false; // disables character movement while paused, might need changing
        }

        if (cursorLock == true){
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (cursorLock == false){
            Cursor.lockState = CursorLockMode.None;
        }
    

        
    }
}
