using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopEnemyBehindDoor : MonoBehaviour
{
    public GameObject enemyObject;
    public float waitTime;
    public bool inDoorZone;
    public GameObject player;

    public Animator animSlidingDoor;

    private float navSpeedAtStart; 

    // used for the banging sound effect
    public AudioClip doorBangSound;
    private AudioSource doorBangSource;
    public float doorBanginterval;
    public float doorPitch;
    
    // used when the door is 'bashed' and spawns particle effects
    public GameObject bashParticleSystem;
    public Transform doorPosition;

    // used when sounds muted during lights on
    public GameObject lightController;
    private float volumeAtStart;

    //used for the sound when enemy opens door
    public AudioSource slidingOpenSound;
    public AudioClip slidingDoorClip;

    // Start is called before the first frame update
    void Start()
    {
        navSpeedAtStart = enemyObject.GetComponent<NavMeshAgent>().speed;
        doorBangSource = GetComponent<AudioSource>();
        volumeAtStart = doorBangSource.volume;
        //Difficulty 
        if (PlayerPrefs.GetInt("difficulty") == 2){
            waitTime = 28;
        }
        if (PlayerPrefs.GetInt("difficulty") == 1){
            waitTime = 32;
        } 
        if (PlayerPrefs.GetInt("difficulty") == 0){
            waitTime = 35;
        } // --
    }

    // Update is called once per frame
    IEnumerator BangingOnTheDoor ()
    {
        while(inDoorZone == true)
        {
            if(enemyObject.GetComponent<EnemyAI>().lightsOn == false){
                doorBangSource.PlayOneShot(doorBangSound);
                Debug.Log("inte1111");

                Instantiate(bashParticleSystem, doorPosition.position, doorPosition.rotation);
                yield return new WaitForSeconds(doorBanginterval);
                doorBanginterval = Random.Range(0.9f, 2.0f);         // Randomises interval sound
                doorPitch = Random.Range (0.85f, 1.15f);               // Randomises pitch so its a bit different each time
                Debug.Log("inte2222");
            }   
            yield return new WaitForSeconds(0.1f); // this is here so there is no while-loop that crashes the game     
        }

        

    }

    void OnTriggerEnter (Collider other){

        if(other.gameObject == enemyObject){
            enemyObject.GetComponent<NavMeshAgent>().speed = 0f;
            enemyObject.GetComponent<EnemyAI>().navSpeed = 0f;
            inDoorZone = true;
            StartCoroutine(WaitHere());
            StartCoroutine(BangingOnTheDoor());
        }
    }

    IEnumerator WaitHere(){
        
        yield return new WaitForSeconds(waitTime);
        enemyObject.GetComponent<EnemyAI>().navSpeed = navSpeedAtStart;
        enemyObject.GetComponent<NavMeshAgent>().speed = navSpeedAtStart;
        inDoorZone = false;
        animSlidingDoor.Play("SlidingDoorOpen");
        slidingOpenSound.PlayOneShot(slidingDoorClip);
        
    }

    void Update(){
        GetComponent<AudioSource>().pitch = doorPitch;
    }
}
