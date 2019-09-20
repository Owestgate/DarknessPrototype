using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class KillScreen : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject playerObj;
    //Name of the screen to transition to
    public string killScreen;
    //If currentDist is less than this, you die.
    public float killDist;
    //Distance between player and chaser.
    private float currentDist;

    //Jumpscare testing 
    public GameObject jumpScarePosition;
    public GameObject jumpScareAudioObject; // game object with audio source set to play on awake
    public GameObject playerCamera;    
    public GameObject jumpScareLookAt;

    // Update is called once per frame
    void Update()
    {
        //Gets the current distance, compares, then kills
        currentDist = Vector3.Distance(playerObj.transform.position, enemyObj.transform.position);
        if (currentDist < killDist)
        {
            //Jump scare animation goes here.
            //testing
            playerObj.transform.position = jumpScarePosition.transform.position; // teleports player into position infront of enemy
            playerObj.GetComponent<FirstPersonController>().enabled = false; // Turns off player controller so it locks player looking at enemy
            playerCamera.transform.LookAt(jumpScareLookAt.transform); // looks at enemy (seperate object attached to enemy that is positioned better)
            jumpScareAudioObject.SetActive(true); // sets active gameobject with audio set to play on awake

            Invoke("LoadScreen", 2.0f); // auto loads menu after delay


            //Unlocks cursor from FPSController
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

       
    }
    
    void LoadScreen (){
        SceneManager.LoadScene(killScreen);
    }
}
