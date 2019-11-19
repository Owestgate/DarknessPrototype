using UnityEngine;

public class CameraObjectController : MonoBehaviour
{
    public AudioSource ReadySound;
    public AudioSource FlashSound;
    public AudioSource BeepSound;
    public Animator Anim;
    public float CameraFlashRate = 0.1f;
    private float nextFlashTime;
    public Renderer orangeLight;

    public float holdDownTime = 1;
    private float currentHoldDownTime;

    private void Start()
    {
        orangeLight.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time > nextFlashTime)
            {
                orangeLight.enabled = true;
                ReadySound.Play();
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (orangeLight.enabled)
            {
                currentHoldDownTime += Time.deltaTime;

                if (currentHoldDownTime >= holdDownTime)
                {
                    nextFlashTime = Time.time + CameraFlashRate;
                    Anim.Play("CameraFlash", 0, 0);
                    FlashSound.Play();
                    orangeLight.enabled = false;
                }
            }      
        }

        if (Input.GetMouseButtonUp(1))
        {
            currentHoldDownTime = 0;
            orangeLight.enabled = false;
        }
    }

    public void PlayBeepSound()
    {
        BeepSound.Play();
    }
}
