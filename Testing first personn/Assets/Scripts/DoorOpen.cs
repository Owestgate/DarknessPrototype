using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public GameObject key;
    public Animator animOpenDoor;

    // Start is called before the first frame update
    void Start()
    {
        animOpenDoor = GetComponent<Animator>();
        //animOpenDoor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider keyCollision){
        if (keyCollision.gameObject.name == "Key"){
            //animOpenDoor.enabled = true;
            animOpenDoor.Play("DoorOpen");
            Debug.Log("Door should open");
        }


    }
}
