using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    [SerializeField] private PlayerActionInputHandler playerActionInputHandler;
    [SerializeField] private GameObject pausedMenu;
    private void Update()
    {
        if (!LevelRecord.Instance.IsEndGame)
        {
            if (playerActionInputHandler.IsPaused)
            {
                pausedMenu.SetActive(true);
                PauseGame();
            }
            else
            {
                pausedMenu.SetActive(false);
                UnPauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        LevelRecord.Instance.IsGameStop = true;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
        LevelRecord.Instance.IsGameStop = false;
        
    }
}
