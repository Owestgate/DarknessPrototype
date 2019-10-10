using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.AI;

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

    //Jumpscare testing 
    public GameObject jumpScarePosition;
    public GameObject jumpScareAudioObject; // game object with audio source set to play on awake
    public GameObject playerCamera;    
    public GameObject jumpScareLookAt;

    public GameObject jumpScarePos2;
    public bool jumpScare2 = false;


    public UnityEvent OnDie;

    // Update is called once per frame
    void Update()
    {
        //Gets the current distance, compares, then kills
        currentDist = Vector3.Distance(playerObj.transform.position, enemyObj.transform.position);
        if (currentDist < killDist && !RoomLights.Instance.switchingOn)
        {

            FirstPersonController fpsController = playerObj.GetComponent<FirstPersonController>();
            //playerObj.transform.position = jumpScarePosition.transform.position; // teleports player into position infront of enemy -- testint new one
            playerObj.transform.position = jumpScarePos2.transform.position;    // Testing new model/position
            fpsController.enabled = false; // Turns off player controller so it locks player looking at enemy
            playerCamera.transform.LookAt(jumpScareLookAt.transform); // looks at enemy (seperate object attached to enemy that is positioned better)
            jumpScareAudioObject.SetActive(true); // sets active gameobject with audio set to play on awake

            jumpScare2 = true;

            Invoke("LoadScreen", 2.0f); // auto loads menu after delay
            OnDie.Invoke();

            SaturationByLightState.Instance.OnLightSwitchOn();
            GrainByLightState.Instance.OnLightSwitchOn();
            RoomLights.Instance.bypass = true;
            RoomLights.Instance.SetSublightsState(true);
            RoomLights.Instance.StopCoroutine(RoomLights.Instance.LightsCoroutine);
            RoomLights.Instance.enabled = false;

            enemyObj.GetComponent<NavMeshAgent>().enabled = false;

        }       
    }
    
    void LoadScreen()
    {
        Invoke("LoadSceneDelayed", 8);
    }

    void LoadSceneDelayed()
    {
        SceneManager.LoadScene(killScreen);
    }
}
