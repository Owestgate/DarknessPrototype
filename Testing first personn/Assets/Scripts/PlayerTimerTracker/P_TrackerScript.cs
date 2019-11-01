using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
//this comment can be ignored/deleted
public class P_TrackerScript : MonoBehaviour
{
    public FirstPersonController pt_player;
    private float timeSurvived;
    private float iniTime;

    private float distTravelled;
    private Vector3 lastPosition;

    private int cpNum;

    private Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {

        iniTime = Time.time;
        lastPosition = pt_player.transform.position;
        distTravelled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pt_player != null)
        {
            timeSurvived = Time.time - iniTime;
            PlayerPrefs.SetFloat("survtime", Mathf.Round(timeSurvived));


            distTravelled += Vector3.Distance(pt_player.transform.position, lastPosition);
            lastPosition = pt_player.transform.position;
            PlayerPrefs.SetFloat("survdist", Mathf.Round(distTravelled / 4));
        } else
        {
            currentScene = SceneManager.GetActiveScene();
            distTravelled = 0;

            if (currentScene.name == "Test")
            {
                pt_player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
            }
            if (currentScene.name == "MainMenu")
            {
                iniTime = Time.time;
            }
        }
    }
}
