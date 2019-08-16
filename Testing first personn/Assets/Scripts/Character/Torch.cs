using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{

    public Light torchLight;
    public Light torchLight2;
    public GameObject torchObject;
    public bool torchOn;
    public MouseDragObject somethingInHand;

    // Start is called before the first frame update
    void Start()
    {
       torchOn = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.E)) {
        torchLight.enabled = !torchLight.enabled;
        torchLight2.enabled = !torchLight2.enabled;
        }
        
        // Hide Torch when holding object
        if (somethingInHand.inHand == true){
            torchObject.SetActive(false);
        }
        if (somethingInHand.inHand == false){
           // torchObject.SetActive(true); //Commented to HIDE TORCH ON START
        }

    }
}
