using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalchase : MonoBehaviour
{
    bool onlyPlayItOnce;
    public GameObject roomLights;
    public GameObject enemy;
    public Vector2 newLightTimeOn;
    public Vector2 newLightTimeOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character" && onlyPlayItOnce == false)
        {
            onlyPlayItOnce = true;
            roomLights.GetComponent<RoomLights>().lightTimeOnRange = newLightTimeOn;
            roomLights.GetComponent<RoomLights>().lightTimeOffRange = newLightTimeOff;
            enemy.GetComponent<EnemyAI>().navSpeed += 1;
        }

    }
}
