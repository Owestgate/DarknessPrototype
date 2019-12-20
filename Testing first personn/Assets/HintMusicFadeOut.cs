using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintMusicFadeOut : MonoBehaviour
{

    public bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading == true)
        {
            GetComponent<AudioSource>().volume -= 0.01f;
            if (GetComponent<AudioSource>().volume <= 0)
            {
                fading = false;
            }
        }
    }
}
