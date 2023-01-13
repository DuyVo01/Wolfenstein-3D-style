using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private PlayerActionInputHandler playerActionInputHandler;
    [SerializeField] private string nextLevel;

    public void OnNext()
    {
        //saveManager.Save();
        SceneManager.LoadScene(nextLevel);
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnResume()
    {
        LevelRecord.Instance.IsGameStop = false;
        playerActionInputHandler.IsPaused = false;
    }
}
