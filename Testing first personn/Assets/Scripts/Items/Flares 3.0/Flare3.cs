using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Flare3 : MonoBehaviour
{
    public Animator flareAnim;
    public int flareDecay;

    private FirstPersonController player;
    private EnemyAI enemy;
    private RoomLights roomLights;

    public float effectDist;
    private float currentDist;

    void Start()
    {
        flareAnim.Play("flareAnim");
        player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        enemy = GameObject.Find("Chaser").GetComponent<EnemyAI>();
        roomLights = GameObject.Find("RoomLights").GetComponent<RoomLights>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDist(player.transform))
            player.nearFlare = true;
        else
            player.nearFlare = false;

        if (CheckDist(enemy.transform))
            enemy.nearFlare = true;
        else
            enemy.nearFlare = false;

        StartCoroutine(KillFlare());
    }

    IEnumerator KillFlare()
    {
        yield return new WaitForSeconds(flareDecay);
        player.nearFlare = false;
        enemy.nearFlare = false;
        roomLights.flareActive = false;
        Destroy(transform.parent.transform.parent.gameObject);
    }

    private bool CheckDist(Transform objTrans)
    {
        currentDist = Vector3.Distance(objTrans.position, transform.position);

        if (currentDist <= effectDist)
            return true;
        else
            return false;
    }
}
