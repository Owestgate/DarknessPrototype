using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopEnemyBehindDoor : MonoBehaviour
{
    public GameObject enemyObject;
    public float waitTime;
    public bool inDoorZone;
    private float navSpeedAtStart;
    public GameObject player;
    public float resetThing;

    public Animator animSlidingDoor;

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

        if(other.gameObject == enemyObject){
            enemyObject.GetComponent<NavMeshAgent>().speed = 0f;
            player.GetComponent<StopEnemyInLights>().navSpeedOnStart = 0f;
            inDoorZone = true;
            StartCoroutine(WaitHere());
        }
    }

    IEnumerator WaitHere(){
        
        yield return new WaitForSeconds(4);
        Debug.Log("fgfgfgfgfgfgfgf");
        player.GetComponent<StopEnemyInLights>().navSpeedOnStart = resetThing;
        enemyObject.GetComponent<NavMeshAgent>().speed = navSpeedAtStart;
    }
}
