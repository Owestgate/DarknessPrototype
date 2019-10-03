using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLetterbox : MonoBehaviour
{
    private Animator letterAnim;
    public float letterBoxLeaveTime;
    public GameObject lightController;

    public bool waitCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        waitCheck = false;
        
        StartCoroutine(ShortDelay());
        letterAnim = GetComponent<Animator>();
        
    }

    IEnumerator ShortDelay(){
        yield return new WaitForSeconds(0.1f);
        letterBoxLeaveTime = lightController.GetComponent<RoomLights>().lightTimeOn + lightController.GetComponent<RoomLights>().lightTimeOff;
        waitCheck = true;
        StartCoroutine(LetterBoxDelay());
    }

    IEnumerator LetterBoxDelay(){
        if(waitCheck = true){
            yield return new WaitForSeconds(letterBoxLeaveTime);
            if(gameObject.name == "LetterBoxTop"){
                letterAnim.Play("LetterBoxTopLeave");
        }
            if(gameObject.name == "LetterBoxBottom"){
                letterAnim.Play("LetterBoxBotLeave");
            }
        }
    }
}
