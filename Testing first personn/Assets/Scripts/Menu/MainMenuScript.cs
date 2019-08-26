using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource scarySound;

    void Start (){
        
    }

    public void PlayGame(){
        WaitToPlay();
        StartCoroutine(WaitToPlay());
    }
    
    IEnumerator WaitToPlay(){
        scarySound.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        
        Application.Quit();
        Debug.Log("Quit");
    }
}
