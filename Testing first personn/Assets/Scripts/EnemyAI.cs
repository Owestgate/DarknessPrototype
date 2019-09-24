using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public GameObject playerCharacter;
    public NavMeshAgent navAgent;
    public bool lightsOn;
    public Mesh model1; //These are the two models currently. Its currently set up for two only but it shouldnt take too long to make space for more if we want more
    public Mesh model2;
    public float navSpeed;
    public AudioSource EnemyCloseSound;

    private MeshFilter modelSlot;
    private bool lightsJustOn = false; //The enemy figures out the exact moment the lights switch, and thats what lightsJustOn is
    private bool currentModel;

    void Start()
    {
        lightsOn = false;
        playerCharacter = GameObject.FindGameObjectWithTag("Character");
        navAgent = GetComponent<NavMeshAgent>();
        navSpeed = navAgent.speed;
        modelSlot = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOn && lightsJustOn)
        {
            currentModel = !currentModel; //if the lights just came on, switch models
        }
        if (currentModel == false)
        {
            modelSlot.mesh = model1;
        } else
        {
            modelSlot.mesh = model2;
        }
        if (lightsOn)
        {
            if (lightsJustOn == true) //if the lights are on, turn lightsJustOn off. if you want to use lightsJustOn, it has to be before this check
            {
                lightsJustOn = false;
            }
            navAgent.speed = 0;
            if (EnemyCloseSound.isPlaying) EnemyCloseSound.Stop();
        } else
        {
            if (lightsJustOn == false) //lightsJustOn can also track when the lights just turned off (but the values will be opposite to when the lights come on). ask me (ewen) if you're confused
            {
                lightsJustOn = true;
            }
            navAgent.speed = navSpeed;
            if (!EnemyCloseSound.isPlaying) EnemyCloseSound.Play();
        }
        navAgent.destination = playerCharacter.transform.position;
    }
}
