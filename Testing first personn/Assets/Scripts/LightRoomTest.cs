using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoomTest : MonoBehaviour
{
    public Light roomLights;
    public GameObject renameLaterroom; // rename
    public bool switchingOn;
    public float lightTimer;
    public GameObject player;
    public CharacterControlTest characterControlScript;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightsOnOff());
        switchingOn = true;
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
        if (other.gameObject.tag == "Character" && switchingOn == true && characterControlScript.moving == true){
        //Destroy(other.gameObject);
        Debug.Log("Player in Lights");
        }
    }
}
