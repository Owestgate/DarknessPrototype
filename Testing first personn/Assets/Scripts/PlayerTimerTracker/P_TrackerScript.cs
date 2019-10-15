using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class P_TrackerScript : MonoBehaviour
{
    public FirstPersonController pt_player;
    private float distTravelled;
    private float timeSurvived;

    //Initial time and position
    private Vector3 iniPos;
    private float iniTime;
    // Start is called before the first frame update
    void Start()
    {
        distTravelled = 0;
        iniTime = Time.time;
        iniPos = pt_player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeSurvived = Time.time - iniTime;
        PlayerPrefs.SetFloat("survtime", timeSurvived);
        //This will need an overhaul due to the map circling back on itself.
        distTravelled = Vector3.Distance(pt_player.transform.position, iniPos);
        PlayerPrefs.SetFloat("survdist", distTravelled);
    }
}
