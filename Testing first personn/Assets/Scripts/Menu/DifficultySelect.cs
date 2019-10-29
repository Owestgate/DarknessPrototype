using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("difficulty", 0);
        Debug.Log("reset");
    }

    public void Easy(){
        PlayerPrefs.SetInt("difficulty", 0);
        Debug.Log("Easy");
    }

    public void Hard(){
        PlayerPrefs.SetInt("difficulty", 1);
        Debug.Log("Hard");
    }

    public void Night(){
        PlayerPrefs.SetInt("difficulty", 2);
        Debug.Log("Night");
    }

}
