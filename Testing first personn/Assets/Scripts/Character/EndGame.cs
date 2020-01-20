using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    
    public GameObject cameraObj;

	public UnityEvent finishGame;
    public GameObject enemy;
    public GameObject player;

    public GameObject endGameUI;
    public GameObject abilityUI;

    public GameObject roomController;

    //Tracker vars
    public Text t_Text;
    public Text d_Text;

    void OnTriggerEnter(Collider plyr){
		if(plyr.tag == "Finish")
		{
			finishGame.Invoke();
		}

        if(plyr.gameObject.tag == "Character"){

            cameraObj.GetComponent<BloomOptimized>().enabled = true;            
            cameraObj.SendMessage("EndGame");
                       
            player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<MenuPopup>().cursorLock = false;            // overrides the menu script cursor lock

            enemy.SetActive(false);

            if(PlayerPrefs.GetFloat("survdist") != 0)
                d_Text.text = "Distance: " + PlayerPrefs.GetFloat("survdist").ToString() + " metres";
            else
                d_Text.text = "Error";

            if (PlayerPrefs.GetFloat("survtime") != 0)
                t_Text.text = "Time: " + System.TimeSpan.FromSeconds(PlayerPrefs.GetFloat("survtime")).ToString();
            else
                t_Text.text = "Error";

            endGameUI.SetActive(true);
            abilityUI.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            StartCoroutine(WaitTime());    // half measure, fix later
        }   
    }

    IEnumerator WaitTime(){               // half measure, fix later
        yield return new WaitForSeconds(14);
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<MenuPopup>().cursorLock = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
