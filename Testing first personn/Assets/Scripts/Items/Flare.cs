using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    public Animator flareAnim;
    public GameObject flareHold;
    public bool animCanPlay;
    public bool flareCanPlay;
    
    // Start is called before the first frame update
    void Start()
    {
       flareHold.SetActive(false); // Flare not in hand on start of game
       animCanPlay = true;
       flareCanPlay = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && animCanPlay == true && flareCanPlay == true){
            flareHold.SetActive(true);
            flareAnim.Play("flareAnim", 0, 0);
            animCanPlay = false;
        }
        if (Input.GetKeyDown(KeyCode.H)){
            flareCanPlay = true;
        }

        if (animCanPlay == false){
            flareCanPlay = false;

        }
        
    }
}
