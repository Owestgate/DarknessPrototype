using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public GameObject playerCharacter;
    public GameObject PlyrKillScreen;
    public NavMeshAgent navAgent;
    public bool lightsOn;
    public Mesh model1; //These are the two models currently. Its currently set up for two only but it shouldnt take too long to make space for more if we want more
    public Mesh model2;
    public Mesh killPose;
    public float navSpeed;
    public float hardSpeed;
    public float nightmareSpeed;
    public AudioSource EnemyCloseSound;
    public AudioSource EnemyCloseSound2;
    public AudioSource EnemyCloseSound3;
    public AudioSource EnemyCloseSound4;
    public AudioSource EnemyCloseSound5;
    private float distToPlayer;
    private float speedMultiplier;

    private MeshFilter modelSlot;
    private bool lightsJustOn = false; //The enemy figures out the exact moment the lights switch, and thats what lightsJustOn is
    private bool currentModel;

    public bool nearFlare;
    public AudioSource[] enemySounds;
    public float hardPitch = 1.3f;

    public Material normalMat;
    public Material brutalMat;
    public GameObject brutalEffects;

    void Start()
    {
        lightsOn = false;
        playerCharacter = GameObject.FindGameObjectWithTag("Character");
        navAgent = GetComponent<NavMeshAgent>();

        int difficulty = PlayerPrefs.GetInt("difficulty");
        Debug.Log(difficulty);
        //Difficulty 
        if (difficulty == 2){
            navSpeed = nightmareSpeed;
            gameObject.GetComponent<MeshRenderer>().material = brutalMat;
            brutalEffects.SetActive(true);
        }
        if (difficulty == 1){
            navSpeed = hardSpeed;
            gameObject.GetComponent<MeshRenderer>().material = brutalMat;
            brutalEffects.SetActive(true);
            for (int i = 0; i < enemySounds.Length; i++)
            {
                enemySounds[i].pitch = hardPitch;
            }
        } 
        if (difficulty == 0){
            navSpeed = navAgent.speed;
            gameObject.GetComponent<MeshRenderer>().material = normalMat;
            brutalEffects.SetActive(false);
        } // --

        modelSlot = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlyrKillScreen.GetComponent<KillScreen>().jumpScare2 == false){
            if (lightsOn && lightsJustOn)
            {
            currentModel = !currentModel; //if the lights just came on, switch models
            }
            if (currentModel == false)
            {
            modelSlot.mesh = model1;
            }
            else
            {
            modelSlot.mesh = model2;
            }
        }
        if (lightsOn)
        {
            if (lightsJustOn == true) //if the lights are on, turn lightsJustOn off. if you want to use lightsJustOn, it has to be before this check
            {
                lightsJustOn = false;
            }
            navAgent.speed = 0;
            if (EnemyCloseSound.isPlaying) EnemyCloseSound.Stop();
            if (EnemyCloseSound2.isPlaying) EnemyCloseSound2.Stop();
            if (EnemyCloseSound3.isPlaying) EnemyCloseSound3.Stop();
            if (EnemyCloseSound4.isPlaying) EnemyCloseSound4.Stop();
            if (!EnemyCloseSound5.isPlaying) EnemyCloseSound5.Stop();
        }
        else
        {
            if (lightsJustOn == false) //lightsJustOn can also track when the lights just turned off (but the values will be opposite to when the lights come on). ask me (ewen) if you're confused
            {
                lightsJustOn = true;
            }
            navAgent.speed = navSpeed;
            if (!EnemyCloseSound.isPlaying) EnemyCloseSound.Play();
            if (!EnemyCloseSound2.isPlaying) EnemyCloseSound2.Play();
            if (!EnemyCloseSound3.isPlaying) EnemyCloseSound3.Play();
            if (!EnemyCloseSound4.isPlaying) EnemyCloseSound4.Play();
            if (EnemyCloseSound5.isPlaying) EnemyCloseSound5.Play();

        }
        if (navAgent.enabled)
        {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, playerCharacter.transform.position, NavMesh.AllAreas, path))
            {
                navAgent.SetPath(path);
            }
        }
        if (nearFlare)
        {
            navAgent.speed = 0;
        }


        //make the chaser run faster if the player is too far ahead
        distToPlayer = Vector3.Distance(playerCharacter.transform.position, transform.position);
        //Debug.Log(distToPlayer);
        speedMultiplier = distToPlayer / 100 + 1;
        navAgent.speed = navAgent.speed * speedMultiplier;

        if(PlyrKillScreen.GetComponent<KillScreen>().jumpScare2 == true){
            modelSlot.mesh = killPose;
        }
    }
}
