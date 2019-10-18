using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShrink : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.Collections.Hashtable hash =
                   new System.Collections.Hashtable();
         hash.Add("amount", new Vector3(0.05f, 0.05f, 0f));
         hash.Add("time", speed);
         iTween.PunchScale(gameObject, hash);
    }
}
