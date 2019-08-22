using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragObject : MonoBehaviour
{
    public Transform onHandPosition;
    public GameObject playerMain;
    public GameObject playerHHand;
    public bool inHand;
    
    public Collider rangePickUp; //change later, here so it can only be picked up within certain distance - make sphere trigger collider on object
    public bool playerInPickupRange;

    //public Collider capsuleCollider;

    void Start (){
        inHand = false;
        playerInPickupRange = false;
    }

    void Update(){
        if(inHand == true){
            this.transform.position = onHandPosition.position;
            //Locking rotation/position 
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
           
        }
        if (inHand == false){
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        }
    }
    
    //Picks up object
    void OnMouseDown(){
        if (playerInPickupRange == true){
        GetComponent<Rigidbody>().useGravity = false;
       // this.transform.position = onHandPosition.position;
        this.transform.parent = playerMain.transform;
        this.transform.parent = playerHHand.transform;
        inHand = true;
        }
    }
    
    //Drops object
    void OnMouseUp (){
        
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<CapsuleCollider>().enabled = true;
        inHand = false;
        
    }

    //Ignores collision with player while holding
    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.tag == "Character")
        {
           Physics.IgnoreCollision(playerMain.GetComponent<CharacterController>(), this.GetComponent<CapsuleCollider>());
           Physics.IgnoreCollision(playerMain.GetComponent<CapsuleCollider>(), this.GetComponent<CapsuleCollider>());
           Debug.Log("ignire player col");
        }
    }

    //Player can only pick up item when within the trigger collider area
    void OnTriggerEnter (Collider rangeIndicator){
        if (rangeIndicator.gameObject.tag == "Character"){
            playerInPickupRange = true;
        }

    }
    //Resets so player cant pick up item again
    void OnTriggerExit (Collider rangeIndicator){
        if (rangeIndicator.gameObject.tag == "Character"){
            playerInPickupRange = false;
        }

    }
}