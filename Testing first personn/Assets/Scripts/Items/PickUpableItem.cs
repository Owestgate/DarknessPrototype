using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableItem : MonoBehaviour
{
    public bool itemBeingHeld;
    public GameObject handCameraObject; // where the pick up script is located
    //public GameObject handPositionObject; // hand position game object
    public Collider thisObjectsCollider;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handCameraObject.GetComponent<PickUpObjects>().thingInHand == true && this.gameObject.transform.parent != null){
            Debug.Log("first if");
            //if(this.gameObject.transform.parent.transform.parent == handCameraObject){
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            itemBeingHeld = true;
            Debug.Log("2nd if");
            //thisObjectsCollider.enabled = false;
            //}
        } 
        if (handCameraObject.GetComponent<PickUpObjects>().thingInHand == false){
            itemBeingHeld = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //thisObjectsCollider.enabled = true;
        }

        if(itemBeingHeld == false){
            this.gameObject.transform.parent = null;
        }

        
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag != "Character" && other.gameObject.tag != "MainCamera" && itemBeingHeld == true)
        {
           Debug.Log("ignore this sumbitch");
           Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), thisObjectsCollider);
        }
        if (other.gameObject.tag == "Character"){
            Physics.IgnoreCollision(other.gameObject.GetComponent<CharacterController>(), thisObjectsCollider);
            Debug.Log("fucking player colliser");
        }
    }
}
