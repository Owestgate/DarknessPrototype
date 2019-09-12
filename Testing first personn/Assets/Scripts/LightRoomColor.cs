using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class LightRoomColor : MonoBehaviour
{
    public GameObject subLights;
    public bool switchingOn;
    public float lightTimeOn;
    public float lightTimeOff;
    public GameObject player;
    public GameObject chaser;

    private float walkSpeedOnStart;
    private float runSpeedOnStart;

    public float walkSpeedInDarkness;
    public float runSpeedInDarkness;

    private Color colorRed = new Color(1, 0, 0, 1);
    private Color colorGreen = new Color(0, 1, 0, 1);
    private Color colorBlue = new Color(0, 0, 1, 1);

    private Color[] colorSequence = new Color[3];
    private int colorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        colorSequence[0] = colorBlue;
        colorSequence[1] = colorGreen;
        colorSequence[2] = colorRed;

        StartCoroutine(LightsOff());
        switchingOn = true;
        runSpeedOnStart = player.GetComponent<FirstPersonController>().m_RunSpeed;
        walkSpeedOnStart = player.GetComponent<FirstPersonController>().m_WalkSpeed;
    }

    IEnumerator LightsOn(){
        while (true){
            yield return new WaitForSeconds(lightTimeOff);
            switchingOn = ! switchingOn;    // bool trigger
            yield return StartCoroutine(LightsOff());
        }
           
    }
    IEnumerator LightsOff()
    {
        while (true)
        {
            foreach (Transform child in subLights.transform)
            {
                child.GetComponent<Light>().color = colorSequence[colorCount];
            }
            colorCount++;
            if (colorCount >= colorSequence.Length)
            {
                colorCount = 0;
            }
            yield return new WaitForSeconds(lightTimeOn);
            switchingOn = !switchingOn;    // bool trigger\
            yield return StartCoroutine(LightsOn());
        }

    }

    void Update()
    {
        //Turning lights on/ off through game object active status
        if (switchingOn == true){
            subLights.SetActive(true);
        }
        if (switchingOn == false){
            subLights.SetActive(false);
        }
    }
    
    void OnTriggerStay (Collider other){
        if (other.gameObject.tag == "Character" && switchingOn == false){
            //other.gameObject.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedInDarkness;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedInDarkness;
            player.GetComponent<FirstPersonController>().m_JumpAllowed = false;
            chaser.GetComponent<EnemyAI>().lightsOn = false; //Lights now simply inform the chaser that the lights are off. Chaser does the work itself now.
        } 
        if(other.gameObject.tag == "Character" && switchingOn == true) {
            player.GetComponent<FirstPersonController>().m_RunSpeed = runSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = walkSpeedOnStart;
            player.GetComponent<FirstPersonController>().m_JumpAllowed = true;
            chaser.GetComponent<EnemyAI>().lightsOn = true;
        }
    }
}
