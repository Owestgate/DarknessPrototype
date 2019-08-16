using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightFlicker;
    public float waitTimeMin;
    public float waitTimeMax;
    // Start is called before the first frame update
    void Start()
    {
        lightFlicker = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing(){   //Turning light on/off
        while (true){
            yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));
            lightFlicker.enabled = ! lightFlicker.enabled;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
