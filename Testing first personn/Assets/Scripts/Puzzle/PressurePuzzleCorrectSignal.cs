using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePuzzleCorrectSignal : MonoBehaviour
{
    public GameObject correspondingPressurePad;
    public GameObject signal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(correspondingPressurePad.GetComponent<PressurePad>().correctBlockOnPad == true){
            signal.SetActive(true);
        }
    }
}
