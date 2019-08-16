using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareBehaviour : MonoBehaviour
{
    public Light flareLight;
    public float minFlicker;
    public float maxFlicker;
    public Animator animator;

    
    void Start()
    {
        flareLight = GetComponent<Light>();   
    }

    // Adds a random amount of gain or reduction in intensity to flicker the flare, only happens during animation
    void Update()
    {
        // Checks to see if Animation is playing
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("flareAnim")){ 
        flareLight.intensity = flareLight.intensity + (Random.Range(minFlicker, maxFlicker));  
        } 
    }
}
