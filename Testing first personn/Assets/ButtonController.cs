using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Animator doorOpenAnimator;
    public AudioSource FailureSound;
    public AudioSource DoorOpen;
    public GameObject errorLight;

    public GameObject lightRoom;
    private int pattern;

    private int codePosition;
    private bool codeFailed;

    private string[] currentCode;

    private string[] colorSequence1 = new string[3];
    private string[] colorSequence2 = new string[5];
    private string[] colorSequence3 = new string[7];

    // Start is called before the first frame update
    void Start()
    {
        colorSequence1[0] = "yellow";
        colorSequence1[1] = "orange";
        colorSequence1[2] = "red";

        colorSequence2[0] = "red";
        colorSequence2[1] = "orange";
        colorSequence2[2] = "red";
        colorSequence2[3] = "yellow";
        colorSequence2[4] = "red";

        colorSequence3[0] = "orange";
        colorSequence3[1] = "red";
        colorSequence3[2] = "yellow";
        colorSequence3[3] = "orange";
        colorSequence3[4] = "red";
        colorSequence3[5] = "orange";
        colorSequence3[6] = "yellow";

        pattern = 1;
        codePosition = 0;
        codeFailed = false;

        currentCode = colorSequence1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(string color)
    {
        if (color != currentCode[codePosition])
        {
            codeFailed = true;
        }
        codePosition += 1;
        if (codePosition == currentCode.Length)
        {
            if (codeFailed)
            {
                codePosition = 0;
                codeFailed = false;
                FailureSound.Play();
                Debug.Log("wrong");
                StartCoroutine(FlashLight());
            }
            else
            {
                codePosition = 0;
                lightRoom.GetComponent<LightRoomColor>().AdvancePattern();
                pattern += 1;
                if (pattern == 2)
                {
                    currentCode = colorSequence2;
                } else
                {
                    currentCode = colorSequence3;
                }
                if (pattern > 3)
                {
                    doorOpenAnimator.Play("SlidingDoorOpen");
                    DoorOpen.Play();
                    codePosition = 100;
                }
            }
        }
    }
    IEnumerator FlashLight()
    {
        errorLight.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        errorLight.SetActive(false);
    }
}
