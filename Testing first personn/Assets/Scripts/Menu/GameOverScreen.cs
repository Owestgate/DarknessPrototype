using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource scarySound;
    public Text dist;
    public Text time;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dist.text = "Distance: " + System.Math.Round(PlayerPrefs.GetFloat("survdist"), 2).ToString() + " metres";
        time.text = "Time: " + System.Math.Round(PlayerPrefs.GetFloat("survtime"), 2).ToString() + " seconds";
    }

    public void Retry()
    {
        //WaitToPlay();
        StartCoroutine(WaitToPlay());
    }
    
    IEnumerator WaitToPlay()
    {
        scarySound.Play();
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        
        Application.Quit();
        Debug.Log("Quit");
    }
}
