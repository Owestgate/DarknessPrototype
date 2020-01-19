using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResumeChase : MonoBehaviour
{

    public GameObject chaser;
    public GameObject chaserResumePos;
    public GameObject roomLights;
    public GameObject pauser;
    public bool triggerOnce;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character" && triggerOnce == false)
        {
            chaser.GetComponent<NavMeshAgent>().enabled = false; // allows teleport
            triggerOnce = true;
            chaser.SetActive(true);
            chaser.transform.position = chaserResumePos.transform.position; // teleport
            roomLights.GetComponent<RoomLights>().GraceTime(1.5f);
            pauser.GetComponent<PauseEnemy>().stopping = false; //Prevents the pause enemy script from incorrectly disabling the chaser
            chaser.GetComponent<NavMeshAgent>().enabled = true; // allows teleport 
        }

    }
}
