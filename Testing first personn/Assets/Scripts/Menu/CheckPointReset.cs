using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointReset : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("pcheckpoint", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
