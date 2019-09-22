using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public string[] correctCode = new string[3];
    public Animator doorOpenAnimator;
    public AudioSource FailureSound;

    private int codePosition;
    private bool codeFailed;

    // Start is called before the first frame update
    void Start()
    {
        codePosition = 0;
        codeFailed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(string color)
    {
        if (color != correctCode[codePosition])
        {
            codeFailed = true;
        }
        codePosition += 1;
        if (codePosition == correctCode.Length)
        {
            if (codeFailed)
            {
                codePosition = 0;
                codeFailed = false;
                FailureSound.Play();
            }
            else
            {
                codePosition = 100;
                doorOpenAnimator.Play("SlidingDoorOpen");
            }
        }
    }
}
