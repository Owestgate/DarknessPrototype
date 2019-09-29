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
            Debug.Log("soundndndnd");
            yield return new WaitForSeconds(doorBanginterval);
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
        
        
    }
}
