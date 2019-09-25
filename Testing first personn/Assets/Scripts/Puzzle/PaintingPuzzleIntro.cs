using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzleIntro : MonoBehaviour
{
    private Animator puzzleAnimator;
    public GameObject particleSmoke; // each individual pieces particle system
    public GameObject particleSmokePuff; // only used by 1 of the ppieces, it just makes the initial smoke when they all move

    // Start is called before the first frame update
    void Start()
    {
        puzzleAnimator = GetComponent<Animator>();
        
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
        yield return new WaitForSeconds(1.1f);
        particleSmoke.SetActive (enabled); // makes particle system play
        gameObject.GetComponent<MeshRenderer>().enabled = false; // hides object -- looks like its dissapeared in the smoke
        Destroy(gameObject, 4f); // destroys the object
    }
}
