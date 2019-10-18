﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSoundEffect : MonoBehaviour
{
    public AudioClip impactSound;
    private AudioSource impactSource;
    public float vell;

    void OnCollisionEnter(Collision hit){
        
        if(this.gameObject.GetComponent<Rigidbody>().velocity.y <= vell){
            impactSource.PlayOneShot(impactSound);
            Debug.Log("drop");
        }
    }
    void OnCollisionStay(Collision hit){
        
        if(this.gameObject.GetComponent<Rigidbody>().velocity.y <= -6.0f){
            impactSource.PlayOneShot(impactSound);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        impactSource = GetComponent<AudioSource>();
    }

}
