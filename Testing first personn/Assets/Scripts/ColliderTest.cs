using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public GameObject player;
    public GameObject text;
    public bool displayText;

    // Start is called before the first frame update
    void Start()
    {
        displayText = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character"){
        Destroy(other.gameObject);
        displayText = true;
        }
    }

    void OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Character"){
            displayText = false;
        }
    }
}
