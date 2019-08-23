﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    public GameObject puzzlePiece1;
    public GameObject puzzlePiece2;
    public GameObject puzzlePiece3;
    public GameObject puzzlePiece4;
    public GameObject puzzlePiece5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if all puzzle pieces are in position
        if(puzzlePiece1.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece2.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece1.GetComponent<Puzzle>().pieceInPosition == true && puzzlePiece1.GetComponent<Puzzle>().pieceInPosition == true){
            Debug.Log ("PUZZLE COMPLETE!!!!");
        }
    }
}
