using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareManager3 : MonoBehaviour
{
    // -->If you are looking for the Flare's lifespan, it is on the Flare Prefab.<-- \\

    public GameObject player;
    public GameObject flarePrefab;

    //Time until new flare usable
    public int flareLife;

    //Current active flares
    private GameObject flareItem;
    private bool flareHeld;
    private bool canUse;

    // Start is called before the first frame update
    void Start()
    {
        flareHeld = false;
        canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUse && !flareHeld)
        {
            canUse = false;
            CreateFlare();
            Debug.Log("New flare.");
            StartCoroutine(CoolDown());
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUse && flareHeld)
        {
            DropFlare();
            Debug.Log("Flare Dropped.");
        }
    }

    private void CreateFlare()
    {
        flareHeld = true;
        flareItem = Instantiate(flarePrefab, player.transform);
        flareItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void DropFlare()
    {
        flareHeld = false;
        flareItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        flareItem.transform.parent = null;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(flareLife);
        canUse = true;
        flareHeld = false;
        Debug.Log("Flares are usable again.");
    }
}
