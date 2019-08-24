using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public int padId;
    public int objectsOnPad = 0;
    public bool correctBlockOnPad;
    public GameObject correcter;

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<PressurePadPickupable>() != null){

            objectsOnPad++;

            if(other.gameObject.GetComponent<PressurePadPickupable>().ReturnBoxId() == padId){
                //Increases the no of correct placements
                PressurePadManager.instance.IncreaseCorrectPlacements();
                correctBlockOnPad = true;
            }

           PressurePadManager.instance.IncreasePlacement();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.GetComponent<PressurePadPickupable>() != null){

            objectsOnPad--;
            //decrease num of placemetns
            PressurePadManager.instance.DecreasePlacement();
            correctBlockOnPad = false;
            
            if(other.gameObject.GetComponent<PressurePadPickupable>().ReturnBoxId() == padId){
                //decrease the num of correct placements
                PressurePadManager.instance.DecreaseCorrectPlacements();
            }

        }
    }
}
