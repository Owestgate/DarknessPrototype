using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BypassTheBypass : MonoBehaviour
{
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();

        coll.enabled = false;
        StartCoroutine(WaitForFix());
    }

    IEnumerator WaitForFix(){
        yield return new WaitForSeconds(1);
        coll.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
