using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorFlareUI : MonoBehaviour
{
    public GameObject canvasUI;
    public GameObject player;

    public GameObject screenCentre;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        canvasUI.SetActive(false);
        screenCentre.SetActive(false);
        once = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<FlareManager3>().canUse == true && once == false){
            canvasUI.SetActive(true);
            screenCentre.SetActive(true);
            once = true;
        }
    }
}
