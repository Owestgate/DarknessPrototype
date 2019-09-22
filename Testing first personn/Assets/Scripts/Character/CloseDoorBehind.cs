using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseDoorBehind : MonoBehaviour
{
    public Animator slidingAnim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "Character"){
            slidingAnim.Play("SlidingDoorClosed");
        }

    }

    
}
