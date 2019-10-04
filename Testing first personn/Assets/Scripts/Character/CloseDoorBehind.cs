using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseDoorBehind : MonoBehaviour
{
    public Animator slidingAnim;
    private AudioSource audSource;
    public AudioClip slidingSound;
    private bool onlyPlayItOnce;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>();
        onlyPlayItOnce = false;
    }


    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "Character" && onlyPlayItOnce == false){
            slidingAnim.Play("SlidingDoorClosed");
            audSource.PlayOneShot(slidingSound);   
            onlyPlayItOnce = true;        
        }

    }


    
}
