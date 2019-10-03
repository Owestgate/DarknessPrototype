using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzleIntro : MonoBehaviour
{
    private Animator puzzleAnimator;
    public GameObject particleSmoke; // each individual pieces particle system
    public GameObject particleSmokePuff; // only used by 1 of the ppieces, it just makes the initial smoke when they all move
    public GameObject realPieceLocation;

    private bool intro;

    public float smoothness = 1.0f;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        puzzleAnimator = GetComponent<Animator>();
        intro = false;
        realPieceLocation.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character"){
            
            if(gameObject.name == "PaintingTopLeftINTRO"){
                particleSmokePuff.SetActive (enabled); // first puff of smoke when all paintings move
                puzzleAnimator.Play("PaintingTopLeftIntroAnim");
                StartCoroutine(WaitTime());
            }

            if(gameObject.name == "PaintingTopRightINTRO"){
                puzzleAnimator.Play("PaintingTopRightIntroAnim");
                StartCoroutine(WaitTime());
            }

            if(gameObject.name == "PaintingBotLeftINTRO"){
                puzzleAnimator.Play("PaintingBotLeftIntroAnim");
                StartCoroutine(WaitTime());
            }
            if(gameObject.name == "PaintingBotRightINTRO"){
                puzzleAnimator.Play("PaintingBotRightIntroAnim");
                StartCoroutine(WaitTime());
            }
        }
    }

    IEnumerator WaitTime(){
        yield return new WaitForSeconds(2f);
        //transform.Rotate = (realPieceLocation.transform.rotation * (2.0f * Time.deltaTime));
        transform.rotation= Quaternion.Lerp (transform.rotation, targetRotation , 50 * smoothness * Time.deltaTime); 
        puzzleAnimator.enabled = false;
        Instantiate(particleSmoke, gameObject.transform.position, gameObject.transform.rotation); // makes particle system play
        intro = true;
        Invoke("SpawnDeathParticles", 0.9f);
        Destroy(gameObject, 2f); // destroys the object
    }

    void SpawnDeathParticles(){
        Instantiate(particleSmoke, gameObject.transform.position, gameObject.transform.rotation);
        realPieceLocation.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update(){
        targetRotation = realPieceLocation.transform.rotation;
        if(intro == true){
            transform.position = Vector3.MoveTowards(transform.position, realPieceLocation.transform.position, 0.8f);
            targetRotation *=  Quaternion.AngleAxis(60, Vector3.up);
        }
    }
}
