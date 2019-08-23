using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzlePiece;
    public GameObject pieceOnWall;

    public Collider sphereRangeCollider;
    public string puzzlePieceName;
    public bool pieceInPosition;


    // Start is called before the first frame update
    void Start()
    {
        sphereRangeCollider = this.gameObject.GetComponent<SphereCollider>();
        pieceInPosition = false;
        pieceOnWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pieceInPosition == true){
           pieceOnWall.SetActive(true);
           Destroy(puzzlePiece, 0);
        }
    }

    void OnTriggerStay (Collider pieceCollider){
        if (pieceCollider.gameObject.tag == "PuzzlePiece" && pieceCollider.gameObject.name == puzzlePieceName && puzzlePiece.GetComponent<MouseDragObject>().inHand == false){
            pieceInPosition = true;
        }
    }
}
