using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoadScreen : MonoBehaviour
{
    public GameObject playBTN;
    public GameObject spinningThing;
    public GameObject readyScreenBTN;

    void Awake()
    {
        StartCoroutine(FakeLoadingTime());
    }

    IEnumerator FakeLoadingTime(){
        
        yield return new WaitForSeconds(4.0f);

        spinningThing.SetActive(false);
        playBTN.SetActive(enabled);
        readyScreenBTN.SetActive(enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
