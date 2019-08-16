using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTest : MonoBehaviour
{
    public Light lightParent; // connect parent light to this
    public MeshRenderer renderer;

    // Used for thing in main menu to appear and disappear in the light

    
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

   
    void Update()
    {
        if(lightParent.enabled == true){
            //Debug.Log("disappear");
            renderer.enabled = false;
        }
        if(lightParent.enabled == false){
            //Debug.Log("Visable");
            renderer.enabled = true;
        }
    }
}
