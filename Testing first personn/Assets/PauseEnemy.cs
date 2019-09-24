using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEnemy : MonoBehaviour
{

    public GameObject enemyObject;

    private bool stopping;

    // Start is called before the first frame update
    void Start()
    {
        stopping = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject == enemyObject)
        {
            if (stopping == true)
            {
                enemyObject.SetActive(false);
                stopping = false;
            }
        }
    }
}
