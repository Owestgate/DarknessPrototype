using System.Collections;
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

    private float walkSpeedOnStart;
    private float runSpeedOnStart;

    public float walkSpeedInDarkness;
    public float runSpeedInDarkness;

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
        if (other.gameObject.tag == "Character" && switchingOn == false){
            //other.gameObject.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedInDarkness;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedInDarkness;
        } 
        if(other.gameObject.tag == "Character" && switchingOn == true) {
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedOnStart;
            
        }
    }
}
