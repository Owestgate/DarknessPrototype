﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string Scene;
    public GameObject chkPoint1;
    public GameObject chkPoint2;
    public GameObject enemyPoint1;
    public GameObject enemyPoint2;
    public GameObject player;
    public GameObject chaser;
    public GameObject entryBoxCP1;
    public GameObject entryBoxCP2;

    // Reset the spawn location by going to the main menu, or by starting on menu
    void Start()
    {
        

        if (PlayerPrefs.GetInt ("pcheckpoint") == 1){
            player.transform.position = chkPoint1.transform.position;
            player.transform.Rotate(new Vector3(0,90,0));
            chaser.transform.position = enemyPoint1.transform.position;
            entryBoxCP1.GetComponent<CloseDoorBehind>().onlyPlayItOnce = true;
            entryBoxCP2.GetComponent<CloseDoorBehind>().onlyPlayItOnce = false;

        }
        if (PlayerPrefs.GetInt ("pcheckpoint") == 2){
            player.transform.position = chkPoint2.transform.position;
            chaser.transform.position = enemyPoint2.transform.position;
            entryBoxCP1.GetComponent<CloseDoorBehind>().onlyPlayItOnce = true;
            entryBoxCP2.GetComponent<CloseDoorBehind>().onlyPlayItOnce = true;
        }
        if (PlayerPrefs.GetInt ("pcheckpoint") == 0){                    // this respawn players at the place they are on original game load
            player.transform.position = player.transform.position;
            chaser.transform.position = chaser.transform.position;
            entryBoxCP1.GetComponent<CloseDoorBehind>().onlyPlayItOnce = false;
            entryBoxCP2.GetComponent<CloseDoorBehind>().onlyPlayItOnce = false;
        }
    }

    void OnTriggerEnter (Collider plyr){
        if (PlayerPrefs.GetInt ("difficulty") != 2)
        {
            if (plyr.gameObject.tag == "Character" && this.gameObject.name == "CheckPoint1")
            {
                PlayerPrefs.SetInt("pcheckpoint", 1);
            }
            if (plyr.gameObject.tag == "Character" && this.gameObject.name == "CheckPoint2")
            {
                PlayerPrefs.SetInt("pcheckpoint", 2);
            }
        }
    }

    /* private void Update()
    {
        //Dev Checkpoint tools, don't forget to delete.
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            PlayerPrefs.SetInt("pcheckpoint", 0);
            Debug.Log("Checkpoint reset to beginning");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerPrefs.SetInt("pcheckpoint", 1);
            Debug.Log("Checkpoint set to checkpoint1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerPrefs.SetInt("pcheckpoint", 2);
            Debug.Log("Checkpoint set to checkpoint2");
        }
    }
    */
}
