using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Animator slidingAnim;
    public GameObject chaser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        chaser.SetActive(true);
        chaser.transform.position = new Vector3(-260,0,85);
        slidingAnim.Play("SlidingDoorClosed");
    }
}
