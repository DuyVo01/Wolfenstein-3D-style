using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CurrentLives", PlayerStatus.currentLives);
        Cursor.lockState = CursorLockMode.None;

        if(PlayerStatus.currentLives >= 0)
        {
            gameOver.SetActive(false);
            SceneManager.LoadScene(PlayerPrefs.GetString("CurrentScene"));
        }
        else
        {
            gameOver.SetActive(true);
        }
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnRetry()
    {
        PlayerPrefs.SetInt("CurrentLives", 1);
        SceneManager.LoadScene("Level01");
    }
}
