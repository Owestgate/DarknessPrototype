using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingUI : MonoBehaviour
{
    public GameObject paintingUI;
    bool onlyShowOnce = true;
    public bool cantPauseNow = false;


    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Character" && onlyShowOnce == true){
            paintingUI.SetActive(true);
            StartCoroutine(UIOnScreen());
            onlyShowOnce = false;
            cantPauseNow = true;

        }
    }

    IEnumerator UIOnScreen(){
        yield return new WaitForSeconds(5);
        paintingUI.SetActive(false);
        cantPauseNow = false;
    }
}
