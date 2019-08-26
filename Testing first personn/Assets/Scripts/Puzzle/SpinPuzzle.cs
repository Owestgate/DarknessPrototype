using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPuzzle : MonoBehaviour
{
    public float rotationAmount; // multiple of 15
    public bool spinnerInPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
       spinnerInPosition = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.x > (rotationAmount - 15) && transform.rotation.eulerAngles.x < (rotationAmount + 15)){
             Debug.Log(gameObject.name + " In position");           
            spinnerInPosition = true;
        }
        
    }
    public void SpinningObject(){
        

        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
