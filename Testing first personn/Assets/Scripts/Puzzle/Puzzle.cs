using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject puzzlePiece;
    public GameObject pieceOnWall;

    public Material puzzleMaterial;

    private Collider sphereRangeCollider;
    public string puzzlePieceName;
    public bool pieceInPosition;

    public AudioClip placedSound;
    private AudioSource placedSource;
    private bool soundPlayed;


    // Start is called before the first frame update
    void Start()
    {
        sphereRangeCollider = this.gameObject.GetComponent<SphereCollider>();
        pieceInPosition = false;
        //pieceOnWall.SetActive(false);
        sphereRangeCollider = GetComponent<SphereCollider>();
        placedSource = GetComponent<AudioSource>();
        soundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pieceInPosition == true && soundPlayed == false){
           //pieceOnWall.SetActive(true);
           Destroy(puzzlePiece, 0);
           pieceOnWall.GetComponent<MeshRenderer>().material = puzzleMaterial;
           placedSource.PlayOneShot(placedSound);
           soundPlayed = true;
        }
    }

    void OnTriggerStay (Collider pieceCollider){
        if (pieceCollider.gameObject.tag == "PuzzlePiece" && pieceCollider.gameObject.name == puzzlePieceName && puzzlePiece.GetComponent<PickUpableItem>().itemBeingHeld == false){
            pieceInPosition = true;
        }
    }
}
