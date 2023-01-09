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
        PlayerStatus.currentLives--;
        if(PlayerStatus.currentLives >= 0)
        {

            gameOver.SetActive(false);
            SceneManager.LoadScene(0);
        }
        else
        {
            gameOver.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
