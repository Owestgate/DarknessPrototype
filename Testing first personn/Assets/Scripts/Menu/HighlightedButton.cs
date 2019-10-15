using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject progressLost;
    public bool onButton;

    void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData){
        onButton = true;
    }
    public void OnPointerExit(PointerEventData eventData){
        onButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(onButton == true){
            progressLost.SetActive(true);
        }
        if(onButton == false){
            progressLost.SetActive(false);
        }
    }
}
