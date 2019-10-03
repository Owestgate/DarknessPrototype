using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource scarySound;

    void Start (){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame(){ //Play Game Button - doesnt load scene
        StartCoroutine(WaitToPlay());
    }

    public void ReadyButton(){     // on the fake pause menu - actually loads scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    IEnumerator WaitToPlay(){
        scarySound.Play();
        yield return new WaitForSeconds(4); // adds a delay so loading screen/ controls are on screen for a few seconds
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        
        Application.Quit();
        Debug.Log("Quit");
    }
}
