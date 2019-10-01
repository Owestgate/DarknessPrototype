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

    public GameObject bashParticleSystem;
    public Transform doorPosition;

    // Start is called before the first frame update
    void Start()
    {
        navSpeedAtStart = enemyObject.GetComponent<NavMeshAgent>().speed;
        doorBangSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    IEnumerator BangingOnTheDoor ()
    {
        while(inDoorZone == true){
            doorBangSource.PlayOneShot(doorBangSound);

            Instantiate(bashParticleSystem, doorPosition.position, doorPosition.rotation);
            yield return new WaitForSeconds(doorBanginterval);
            doorBanginterval = Random.Range(0.6f, 2.0f);         // Randomises interval sound
            doorPitch = Random.Range (0.85f, 1.15f);               // Randomises pitch so its a bit different each time
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
        
    }

    void Update(){
        
        GetComponent<AudioSource>().pitch = doorPitch;
    }
}
