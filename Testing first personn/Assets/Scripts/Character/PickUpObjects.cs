﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{

    public Transform onHandPos;
    public GameObject playerMainController;
    public GameObject playerMainHand;
    public GameObject holdingItem;

    public bool thingInHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.red);
        CheckForPickup();
        if(thingInHand == false){
            holdingItem = null;
        }
    }

    private void CheckForPickup(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f)){
        
            if(Input.GetKeyDown(KeyCode.Mouse0) && hit.transform.gameObject.GetComponent<PickUpableItem>()){
                holdingItem = hit.transform.gameObject;

                holdingItem.transform.parent = playerMainController.transform;
                holdingItem.transform.parent = playerMainHand.transform;
                holdingItem.transform.position = playerMainHand.transform.position;
                thingInHand = true;
            }

            if(Input.GetKeyUp(KeyCode.Mouse0) /*&& hit.transform.gameObject.GetComponent<PickUpableItem>()*/){
                holdingItem.transform.parent = null;
                //hit.transform.gameObject.transform.position = hit.transform.gameObject.transform.position;
                thingInHand = false;
                Debug.Log("drop");
            }

            //Tells Spinning puzzle its being hit
            if (Input.GetKey(KeyCode.Mouse0) && hit.transform.gameObject.GetComponent<SpinPuzzle>()){
                hit.transform.SendMessage ("SpinningObject");
                
            }

            if( hit.transform.gameObject != holdingItem){
              //  holdingItem.transform.parent = null;
            }

        }
    }
}
