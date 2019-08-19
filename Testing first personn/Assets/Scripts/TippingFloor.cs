using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TippingFloor : MonoBehaviour
{

    public Animator floorAnim;

    // Start is called before the first frame update
    void Start()
    {
        //floorAnim.Play("TippingPlatform", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision playerCol){
        if (playerCol.gameObject.tag == "Character"){
        floorAnim.Play("TippingPlatform", 0, 0);
        Debug.Log("FF");
        }
    }
}
