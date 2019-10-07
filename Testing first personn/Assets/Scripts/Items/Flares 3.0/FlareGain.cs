using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlareGain : MonoBehaviour
{
    private float ttime = 0;
    public GameObject flareUI;
    public GameObject flareManager;
    private float flareCD;
    
    // Start is called before the first frame update
    void Start()
    {
        flareCD = 20.0f; //I think ????
    }

    // Update is called once per frame
    void Update()
    {
        if(flareManager.GetComponent<FlareManager3>().canUse == false){
            flareUI.GetComponent<Image>().fillAmount = 0;
            ttime += Time.deltaTime / flareCD;
            flareUI.GetComponent<Image>().fillAmount = Mathf.Lerp(0.0f, 1.0f, ttime);
            
        }
        if(flareManager.GetComponent<FlareManager3>().canUse == true){
            flareUI.GetComponent<Image>().fillAmount = 1;
            ttime = 0;
        }
        
    }

}
