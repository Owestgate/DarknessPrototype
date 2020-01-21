using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseDoorBehindAlt : MonoBehaviour
{
    public Animator slidingAnim;
    private AudioSource audSource;
    public AudioClip slidingSound;
    private bool onlyPlayItOnce;
    public GameObject roomLights;
    public GameObject colorLights;

    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>();
        onlyPlayItOnce = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character" && onlyPlayItOnce == false)
        {
            slidingAnim.Play("SlidingDoorClosed");
            audSource.PlayOneShot(slidingSound);
            onlyPlayItOnce = true;
            roomLights.GetComponent<RoomLights>().ForceOffAlt(2);
            colorLights.GetComponent<LightRoomColor>().ForceOn(4);
        }

    }



}
