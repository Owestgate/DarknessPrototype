using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopEnemyInLights : MonoBehaviour
{
    public NavMeshAgent enemyNav;
    public bool lightsAreSupposedlyOn;
    public float justTesting;
    private float navSpeedOnStart;
    

    IEnumerator LightsOn(){
        while (true){
        
        yield return new WaitForSeconds(justTesting);
        lightsAreSupposedlyOn = ! lightsAreSupposedlyOn;    // bool trigger
        
        }
           
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightsOn());
        navSpeedOnStart = enemyNav.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(lightsAreSupposedlyOn == true){
            enemyNav.speed = 0;

        }
        if(lightsAreSupposedlyOn == false){
            enemyNav.speed = navSpeedOnStart;
        }
    }
}
