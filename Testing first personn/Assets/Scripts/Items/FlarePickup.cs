using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlarePickup : MonoBehaviour
{
    
    //public GameObject flarePrefab;
    //public GameObject characterFlareHand;
    public GameObject flarehold;

    void Update()
    {
        
    }
    
    // Destroys the Flare item on the ground, makes flare in hand appear - fix later to add the flare from a prefab into hand
    void OnCollisionEnter (Collision flareItem){
        if (flareItem.gameObject.name == "FlareItem"){
            Destroy(flareItem.gameObject, 0f);
            flarehold.SetActive(true);

        }
    }
}
