using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvidenceCompletion : MonoBehaviour
{

    public List<GameObject> spawnPoints; // 8 spawn points
    public List<GameObject> hardSpawnPoints;
    public List<Transform> pieces; // 5 pieces + extra empty spots so placements change every game
                                   // public GameObject puzzlePiece5;

    public Animator doorOpenAnimator; // change to what ever animation we use
    public AudioSource doorAudio;

    private bool doorOpened = false;

    void Start()
    {
        RandomiseSpawnList();
        SpawnPositions(pieces);
    }

    //spawn objects
    public void SpawnPositions(List<Transform> objects)
    {
        if (PlayerPrefs.GetInt("difficulty") == 1)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = hardSpawnPoints[i].transform.position;
                objects[i].transform.rotation = hardSpawnPoints[i].transform.rotation;
            }
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = spawnPoints[i].transform.position;
                objects[i].transform.rotation = hardSpawnPoints[i].transform.rotation;
            }
        }
    }

    // Randomise order of puzzle pieces
    void RandomiseSpawnList()
    {

        List<GameObject> tempList = new List<GameObject>();

        if (PlayerPrefs.GetInt("difficulty") == 1 || PlayerPrefs.GetInt("difficulty") == 2)
        {
            for (int i = 0; i < hardSpawnPoints.Count; i++)
            {
                tempList.Add(hardSpawnPoints[i]);
            }

            for (int i = 0; i < hardSpawnPoints.Count; i++)
            {
                GameObject tempColor = hardSpawnPoints[i];
                int randomIndex = Random.Range(i, hardSpawnPoints.Count);
                hardSpawnPoints[i] = hardSpawnPoints[randomIndex];
                hardSpawnPoints[randomIndex] = tempColor;
            }
        }
        else
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                tempList.Add(spawnPoints[i]);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameObject tempColor = spawnPoints[i];
                int randomIndex = Random.Range(i, spawnPoints.Count);
                spawnPoints[i] = spawnPoints[randomIndex];
                spawnPoints[randomIndex] = tempColor;
            }
        }
    }
}
