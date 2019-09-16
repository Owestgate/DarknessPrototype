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

    // Start is called before the first frame update
    void Start()
    {
        navSpeedAtStart = enemyObject.GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(inDoorZone == true){
            
        }
    }

    void OnTriggerEnter (Collider other){

        Debug.Log("uh?");
        if(other.gameObject == enemyObject){
            enemyObject.GetComponent<NavMeshAgent>().speed = 0f;
            enemyObject.GetComponent<EnemyAI>().navSpeed = 0f;
            inDoorZone = true;
            StartCoroutine(WaitHere());
            Debug.Log("caught em boss");
        }
    }

    IEnumerator WaitHere(){
        
        yield return new WaitForSeconds(waitTime);
        Debug.Log("fgfgfgfgfgfgfgf");
        enemyObject.GetComponent<EnemyAI>().navSpeed = navSpeedAtStart;
        enemyObject.GetComponent<NavMeshAgent>().speed = navSpeedAtStart;
    }
}
