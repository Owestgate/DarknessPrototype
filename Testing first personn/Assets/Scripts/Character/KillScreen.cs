using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillScreen : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject playerObj;
    //Name of the screen to transition to
    public string killScreen;
    //If currentDist is less than this, you die.
    public float killDist;
    //Distance between player and chaser.
    private float currentDist;

    // Update is called once per frame
    void Update()
    {
        //Gets the current distance, compares, then kills
        currentDist = Vector3.Distance(playerObj.transform.position, enemyObj.transform.position);
        if (currentDist < killDist)
        {
            //Jump scare animation goes here.
            
            SceneManager.LoadScene(killScreen);
        }
    }
}
