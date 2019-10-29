using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public Animator doorOpenAnimator;
    private AudioSource failAudSource;
    public AudioClip FailureSound;
    private AudioSource doorAudSource;
    public AudioClip DoorOpen;
    public AudioClip SuccessSound;
    private AudioSource successAudSource;
    public GameObject GeneratorNoise1;
    public GameObject GeneratorNoise2;
    public GameObject GeneratorNoise3;
    public GameObject errorLight;
    public GameObject greenLight1;
    public GameObject greenLight2;
    public GameObject greenLight3;

    public GameObject lightRoom;
    private int pattern;

    private int codePosition;
    private bool codeFailed;

    private string[] currentCode;

    private string[] colorSequence1 = new string[3];
    private string[] colorSequence2 = new string[4];
    private string[] colorSequence3 = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        colorSequence1[0] = "yellow";
        colorSequence1[1] = "magenta";
        colorSequence1[2] = "cyan";

        colorSequence2[0] = "cyan";
        colorSequence2[1] = "magenta";
        colorSequence2[2] = "yellow";
        colorSequence2[3] = "cyan";

        colorSequence3[0] = "magenta";
        colorSequence3[1] = "cyan";
        colorSequence3[2] = "magenta";
        colorSequence3[3] = "yellow";
        colorSequence3[4] = "magenta";

        pattern = 1;
        codePosition = 0;
        codeFailed = false;

        currentCode = colorSequence1;

        failAudSource = GetComponent<AudioSource>();
        doorAudSource = GetComponent<AudioSource>();
        successAudSource = GetComponent<AudioSource>();
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
                failAudSource.PlayOneShot(FailureSound);
                StartCoroutine(FlashLight());
            }
            else
            {
                codePosition = 0;
                lightRoom.GetComponent<LightRoomColor>().AdvancePattern();
                pattern += 1;
                successAudSource.PlayOneShot(SuccessSound);
                if (pattern == 2)
                {
                    currentCode = colorSequence2;
                    greenLight1.SetActive(true);
                    GeneratorNoise1.GetComponent<AudioSource>().Play();
                } else
                {
                    currentCode = colorSequence3;
                    greenLight2.SetActive(true);
                    GeneratorNoise2.GetComponent<AudioSource>().Play();
                }
                if (pattern > 3)
                {
                    doorOpenAnimator.Play("SlidingDoorOpen");
                    doorAudSource.PlayOneShot(DoorOpen);
                    codePosition = 100;
                    greenLight3.SetActive(true);
                    GeneratorNoise3.GetComponent<AudioSource>().Play();
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
