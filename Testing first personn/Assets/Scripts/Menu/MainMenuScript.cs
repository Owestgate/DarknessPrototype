using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource scarySound;
    bool loadOnce;
    public string NextSceneName;

    void Start (){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loadOnce = false;
    }
    public void SetNextSceneName(string newSceneName)
    {
        NextSceneName = newSceneName;
    }

    public void PlayGame(){ //Play Game Button - doesnt load scene
        StartCoroutine(WaitToPlay());
    }

    public void ReadyButton(){     // on the fake pause menu - actually loads scene
    if(loadOnce == false){
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadSceneAsync(NextSceneName);
            loadOnce = true;
    }
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
