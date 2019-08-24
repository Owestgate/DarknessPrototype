using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPuzzle : MonoBehaviour
{
    public Transform endPos;
    public float speed;
    public bool magnetInRange;
    public bool finishedRoute;
    
    void Start()
    {
        //startPos.transform.localScale = new Vector3() startPoss;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position == endPos.transform.position){
            finishedRoute = true;
        }

    }

    public void OnTriggerStay(Collider magnet){
        if(magnet.gameObject.name == "Magnet"){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);
        }
    }
}
