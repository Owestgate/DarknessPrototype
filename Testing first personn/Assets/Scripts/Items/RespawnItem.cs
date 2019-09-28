using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject respawnObjectLocation; // new empty gameobject is created on start and is used as items respawn location
    public Transform thisObjectStartPos; // objects position on start

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSpawn());
        thisObjectStartPos = this.gameObject.transform;
    }

    IEnumerator WaitForSpawn(){           // after a short delay(delay because of paintings original position is changed on start) new object is made
        yield return new WaitForSeconds(0.1f);
        respawnObjectLocation = new GameObject(this.gameObject.name + "RespawnLocation");
        respawnObjectLocation.transform.position = thisObjectStartPos.position;
    }

    void OnTriggerStay (Collider respawnBox){    // if item hits Kill box + is not being held, item is respawned + item velocity is set to 0
        if(respawnBox.gameObject.name == "KillBox" && gameObject.GetComponent<PickUpableItem>().itemBeingHeld == false){
            gameObject.transform.position = respawnObjectLocation.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    
        if(respawnBox.gameObject.name == "KillBoxPaintingOnly" && this.gameObject.tag == "PuzzlePiece" && gameObject.GetComponent<PickUpableItem>().itemBeingHeld == false){
            gameObject.transform.position = respawnObjectLocation.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
