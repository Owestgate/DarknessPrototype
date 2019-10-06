using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeChase : MonoBehaviour
{

    public GameObject chaser;
    public GameObject chaserResumePos;
    public GameObject roomLights;
    public GameObject pauser;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            chaser.SetActive(true);
            chaser.transform.position = chaserResumePos.transform.position;
            roomLights.GetComponent<RoomLights>().gracePeriodActive = true;
            pauser.GetComponent<PauseEnemy>().stopping = false; //Prevents the pause enemy script from incorrectly disabling the chaser
        }

    }
}
