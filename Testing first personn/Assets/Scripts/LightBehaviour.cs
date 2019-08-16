using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{

    public float lightSpeed = 2f;
    public float maxRotation = 45f;

    public float lightTime;
    public bool lightOn;
    
    public Light testLight;     // Place light Child here in Inspector
    public float lightOffTime;  // amount of time light is off

    
    void Start()
    {
        
        StartCoroutine(Flashing());
    }

    
    void Update()
    {
        transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * lightSpeed), 0f, 0f); //Swinging light
 
    }

    IEnumerator Flashing(){   //Turning light on/off
        while (true){
            yield return new WaitForSeconds(lightOffTime);
            testLight.enabled = ! testLight.enabled;
        }
    }
}
