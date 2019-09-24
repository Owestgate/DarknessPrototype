using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareManager : MonoBehaviour
{
    // -->If you are looking for Flare Decay, it is on the Flare Prefab.<-- \\

    public GameObject player;
    public GameObject flare;
    //Time until new flare usable
    public int flareLife;
    //Current active flares
    private GameObject Flare;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canUse)
            {
                canUse = false;
                CreateFlare();
                Debug.Log("New flare.");
                StartCoroutine(CoolDown());
            }
            else
            {
                Debug.Log("Didn't create flare.");
            }
        }
    }

    private void CreateFlare()
    {
        flareHeld = true;
        Flare = Instantiate(flare, player.transform);
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(flareLife);
        canUse = true;
        Debug.Log("Flares are usable again.");
    }
}
