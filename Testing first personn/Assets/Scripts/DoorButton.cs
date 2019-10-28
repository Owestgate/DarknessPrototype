using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Animator slidingAnim;
    public GameObject chaser;

    public GameObject door1;   //Left
    public GameObject door2;   //Right

    public bool onlyOnce;

    public AudioSource leverSource;
    public AudioClip leverClip;
    public AudioClip doorClip;

    // Start is called before the first frame update
    void Start()
    {
        onlyOnce = false;
    }

    IEnumerator DoorDelay(){
        yield return new WaitForSeconds(0.5f);
        door1.GetComponent<Animator>().Play("LeftDoor");
        door2.GetComponent<Animator>().Play("RightDoor");
    }

    public void Press()
    {
        if(onlyOnce == false){
            gameObject.GetComponent<Animator>().Play("Lever");
            onlyOnce = true;
            leverSource.PlayOneShot(leverClip);
            StartCoroutine(DoorDelay());
            door1.GetComponent<AudioSource>().PlayOneShot(doorClip);
            door2.GetComponent<AudioSource>().PlayOneShot(doorClip);
        //chaser.SetActive(true);
        //chaser.transform.position = new Vector3(-260,0,85);
       

        }
    }
}
