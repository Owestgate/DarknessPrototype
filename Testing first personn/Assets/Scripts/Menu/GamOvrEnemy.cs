using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamOvrEnemy : MonoBehaviour
{
    public float randomNumber = 100;
    public GameObject lightOnOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lightOnOff.GetComponent<LightFlicker>().lightOn == true){
            randomNumber = Random.Range(0, 100);
        }

        if(lightOnOff.GetComponent<LightFlicker>().lightOn == false && randomNumber <= 20){
            GetComponent<MeshRenderer>().enabled = false;
        }
        if(lightOnOff.GetComponent<LightFlicker>().lightOn == false && randomNumber >= 21){
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
