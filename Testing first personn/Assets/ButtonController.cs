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

    private string[] colorSequence1h = new string[3];
    private string[] colorSequence2h = new string[5];
    private string[] colorSequence3h = new string[7];

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

        colorSequence1h[0] = "magenta";
        colorSequence1h[1] = "yellow";
        colorSequence1h[2] = "cyan";

        colorSequence2h[0] = "cyan";
        colorSequence2h[1] = "yellow";
        colorSequence2h[2] = "cyan";
        colorSequence2h[3] = "magenta";
        colorSequence2h[4] = "yellow";

        colorSequence3h[0] = "magenta";
        colorSequence3h[1] = "cyan";
        colorSequence3h[2] = "yellow";
        colorSequence3h[3] = "cyan";
        colorSequence3h[4] = "yellow";
        colorSequence3h[5] = "magenta";
        colorSequence3h[6] = "yellow";

        pattern = 1;
        codePosition = 0;
        codeFailed = false;

        if (PlayerPrefs.GetInt("difficulty") == 0)
        {
            currentCode = colorSequence1;
        } else
        {
            currentCode = colorSequence1h;
        }

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
                    if (PlayerPrefs.GetInt("difficulty") == 0)
                    {
                        currentCode = colorSequence2;
                    } else
                    {
                        currentCode = colorSequence2h;
                    }
                    greenLight1.SetActive(true);
                    GeneratorNoise1.GetComponent<AudioSource>().Play();
                } else
                {
                    if (PlayerPrefs.GetInt("difficulty") == 0)
                    {
                        currentCode = colorSequence3;
                    } else
                    {
                        currentCode = colorSequence3h;
                    }
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
