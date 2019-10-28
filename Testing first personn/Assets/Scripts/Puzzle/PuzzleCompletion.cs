using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    public GameObject puzzlePiece1;
    public GameObject puzzlePiece2;
    public GameObject puzzlePiece3;
    public GameObject puzzlePiece4;

    public List<GameObject> spawnPoints; // 8 spawn points
    public List<GameObject> hardSpawnPoints;
    public List<Transform> pieces; // 4 pieces + 4 empty spots so placements change every game
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
    public void SpawnPositions (List<Transform> objects){
        if (PlayerPrefs.GetInt("difficulty") == 1)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = hardSpawnPoints[i].transform.position;
                Debug.Log("asdasda");
            }
        } else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = spawnPoints[i].transform.position;
            }
        }
    }

    // Randomise order of puzzle pieces
    void RandomiseSpawnList (){
        
        List<GameObject> tempList = new List<GameObject>();

        if (PlayerPrefs.GetInt("difficulty") == 1 || PlayerPrefs.GetInt("difficulty") == 2)
        {
            Debug.Log("asdasda2");
            for (int i = 0; i < hardSpawnPoints.Count; i++)
            {
                tempList.Add(hardSpawnPoints[i]);
            }

            for (int i = 0; i < hardSpawnPoints.Count; i++)
            {
                GameObject tempColor = hardSpawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, hardSpawnPoints.Count);
                hardSpawnPoints[i] = hardSpawnPoints[randomIndex];
                hardSpawnPoints[randomIndex] = tempColor;
            }
        } else
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                tempList.Add(spawnPoints[i]);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameObject tempColor = spawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, spawnPoints.Count);
                spawnPoints[i] = spawnPoints[randomIndex];
                spawnPoints[randomIndex] = tempColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if all puzzle pieces are in position
        if(puzzlePiece1.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece2.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece3.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece4.GetComponent<Puzzle>().pieceInPosition == true && !doorOpened){
            doorOpenAnimator.Play("SlidingDoorOpen");
            doorAudio.enabled = true;
            doorOpened = true;
        }
    }
}
