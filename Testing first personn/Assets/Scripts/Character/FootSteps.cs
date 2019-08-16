using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    public CharacterControlTest controls;
    public AudioSource footstepSound;
    public AudioClip footstepClip;
    
    public float stepRate = 1.0f;
    public float stepCD;

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<CharacterControlTest>();
        footstepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Sneaking/Sprinting or Walking and adjusts footstep rate accordingly
        if (controls.isSneaking == true && controls.isSprinting == false){
            stepRate = 1.4f; //Sneaking
        }
        if(controls.isSneaking == false && controls.isSprinting == true){
            stepRate = 0.5f; //Sprinting
        }
        if(controls.isSneaking == false && controls.isSprinting == false){
            stepRate = 1.0f; //Walking
        }

        //Footstep Scripting
        stepCD -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCD < 0f && controls.cooldown == false){
          footstepSound.pitch = 1f + Random.Range (-0.2f, 0.2f);
          footstepSound.PlayOneShot (footstepClip, 0.9f);
          stepCD = stepRate;
        }

        //Stops audio if jumping - real buggy fix at later point
        if(controls.cooldown == true){
            footstepSound.Stop();
        }
    }
}
