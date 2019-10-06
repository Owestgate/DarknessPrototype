using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string Scene;
    public GameObject chkPoint1;
    public GameObject chkPoint2;
    public GameObject player;

    // Reset the spawn location by going to the main menu, or by starting on menu
    void Start()
    {
        

        if (PlayerPrefs.GetInt ("pcheckpoint") == 1){
            player.transform.position = chkPoint1.transform.position;
        }
        if (PlayerPrefs.GetInt ("pcheckpoint") == 2){
            player.transform.position = chkPoint2.transform.position;
        }
        if (PlayerPrefs.GetInt ("pcheckpoint") == 0){                    // this respawn players at the place they are on original game load
            player.transform.position = player.transform.position;
        }
    }

    void OnTriggerEnter (Collider plyr){
        if(plyr.gameObject.tag == "Character" && this.gameObject.name == "CheckPoint1"){
            PlayerPrefs.SetInt("pcheckpoint", 1);
        }
        if(plyr.gameObject.tag == "Character" && this.gameObject.name == "CheckPoint2"){
            PlayerPrefs.SetInt("pcheckpoint", 2);
        }
    }
}
