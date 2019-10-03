using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoadScreen : MonoBehaviour
{
    public GameObject playBTN;
    public GameObject spinningThing;

    void Awake()
    {
        StartCoroutine(FakeLoadingTime());
    }

    IEnumerator FakeLoadingTime(){
        Debug.Log("FGGGGGGGGG");
        yield return new WaitForSeconds(4.0f);
        Debug.Log("s");
        spinningThing.SetActive(false);
        playBTN.SetActive(enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
