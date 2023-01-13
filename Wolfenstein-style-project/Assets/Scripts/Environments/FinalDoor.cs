using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private LevelSummary levelSummary;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void OnInteract()
    {
        FinishLevel();
        Time.timeScale = 0;
    }

    private void FinishLevel()
    {
        LevelRecord.Instance.IsGameStop = true;
        LevelRecord.Instance.IsEndGame = true;
        Cursor.lockState = CursorLockMode.None;
        levelSummary.Summary();
    }
   
}
