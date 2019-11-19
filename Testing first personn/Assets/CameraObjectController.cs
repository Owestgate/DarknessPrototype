using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectController : MonoBehaviour
{
    public AudioSource FlashSound;
    public Animator Anim;
    public float CameraFlashRate = 0.1f;
    private float nextFlashTime;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time > nextFlashTime)
            {
                nextFlashTime = Time.time + CameraFlashRate;
                Anim.Play("CameraFlash", 0, 0);
                FlashSound.Play();
            }
        }
    }
}
