using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource scarySound;
    public Text dist;
    public Text time;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        dist.text = "Distance: " + PlayerPrefs.GetFloat("survdist").ToString() + " metres";
        time.text = "Time: " + System.TimeSpan.FromSeconds(PlayerPrefs.GetFloat("survtime")).ToString();
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
