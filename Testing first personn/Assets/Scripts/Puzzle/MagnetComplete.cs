using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetComplete : MonoBehaviour
{
    public GameObject mag1;
    public GameObject mag2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mag1.GetComponent<MagnetPuzzle>().finishedRoute == true && mag2.GetComponent<MagnetPuzzle>().finishedRoute == true){
            Debug.Log("completion");
        }
    }
}
