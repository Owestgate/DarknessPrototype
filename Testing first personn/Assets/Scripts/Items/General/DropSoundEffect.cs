using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSoundEffect : MonoBehaviour
{
    public AudioClip impactSound;
    private AudioSource impactSource;

    void OnCollisionEnter(Collision hit){
        
        if(this.gameObject.GetComponent<Rigidbody>().velocity.y <= -1.0f){
            impactSource.PlayOneShot(impactSound);
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
