using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public GameObject playerCharacter;
    public NavMeshAgent enemy;

    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Character");
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = playerCharacter.transform.position;
    }
}
