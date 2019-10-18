using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        PlayerPrefs.SetInt("pcheckpoint", 0);
        SceneManager.LoadScene("Test");
    }
}
