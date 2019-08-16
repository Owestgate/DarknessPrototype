using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this here so Button can be used

public class MenuPopup : MonoBehaviour
{

    public GameObject menuPopup; // Menu Canvas Object - menu hidden on start
    public bool menuOpen; //check if menu already open
    public AudioListener listener; //Attack camera to this in inspector
    public bool muted; //check if muted

   // public Button muteButton; //trying to change color through script, not working
   // public ColorBlock colorChange;
    
    // Start is called before the first frame update
    void Start()
    {
       muted = false;
    }

    //Unpauses game from the return button on pause menu panel
    public void ReturnButton(){
        if(menuOpen == true){
            menuPopup.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1; // Unpauses game time (recheck)
            Cursor.lockState = CursorLockMode.Locked;
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
        if(Input.GetKeyDown(KeyCode.P) && menuOpen == true){
            menuPopup.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1; // Unpauses game time (recheck)
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(Input.GetKeyDown(KeyCode.P) && menuOpen == false){
            menuPopup.SetActive(true);
            menuOpen = true;
            Time.timeScale = 0; // Pauses Game time (recheck)
            Cursor.lockState = CursorLockMode.None;
        }
    

        
    }
}
