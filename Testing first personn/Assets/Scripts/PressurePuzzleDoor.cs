using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePuzzleDoor : MonoBehaviour
{
    public GameObject padManager;
    public Animator thisDoorsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(padManager.GetComponent<PressurePadManager>().currentCorrectPlacements == 3){
            thisDoorsAnimator.Play("PressurePuzzleDoorDown");
        }
    }
}
