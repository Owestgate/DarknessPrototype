using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareBehaviour : MonoBehaviour
{
    public Light flareLight;
    public float minFlicker;
    public float maxFlicker;
    public Animator flareAnimator;
    public MouseDragObject isHolding; // Connect parent to here

    
    void Start()
    {
        flareLight = GetComponent<Light>();   
        
    }

    // Adds a random amount of gain or reduction in intensity to flicker the flare, only happens during animation
    void Update()
    {
        // Checks to see if Animation is playing - then randomly flickers light
        if (this.flareAnimator.GetCurrentAnimatorStateInfo(0).IsName("flareAnim")){ 
        flareLight.intensity = flareLight.intensity + (Random.Range(minFlicker, maxFlicker));  
        } 
        
        //Lights flare if player is holding item
        if (Input.GetKeyDown(KeyCode.G) && isHolding.inHand == true){
            flareAnimator.Play("flareAnim");
            Destroy(transform.parent.gameObject, 6.0f);
        }


    }
}
