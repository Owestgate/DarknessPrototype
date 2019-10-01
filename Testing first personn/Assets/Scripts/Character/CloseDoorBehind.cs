using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseDoorBehind : MonoBehaviour
{
    public Animator slidingAnim;
    private AudioSource audSource;
    public AudioClip slidingSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }


    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "Character"){
            slidingAnim.Play("SlidingDoorClosed");
            audSource.PlayOneShot(slidingSound);           
        }

    }


    
}
