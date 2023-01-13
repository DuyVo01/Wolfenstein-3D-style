using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelRecord : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeValue;
    [SerializeField]
    private TextMeshProUGUI enemyValue;
    [SerializeField]
    private TextMeshProUGUI treasureValue;
    [SerializeField]
    private TextMeshProUGUI scoreValue;

    private Timer timer;

    public bool IsGameStop { get; set; }
    public bool IsEndGame { get; set; }

    private int enemyKilled;
    private int treasureCollected;
    
    public static LevelRecord Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        IsGameStop = false;
        IsEndGame = false;
    }

    private void Start()
    {
        enemyKilled = 0;
        treasureCollected = 0;
        PlayerStatus.score = 0;
        timer = GetComponent<Timer>();
    }

    public void UpdateEnemyKilled()
    {
        enemyKilled++;
        enemyValue.text = enemyKilled + "";
    }

    public void UpdateTreasureCollected()
    {
        treasureCollected++;
        treasureValue.text = "" + treasureCollected;
    }

    public void UpdateScore(int score)
    {
        scoreValue.text = "" + score;
    }

    public void UpdateTime()
    {
        timeValue.text = string.Format("{0:00}:{1:00}", timer.CurrentMinutes, timer.CurrentSeconds);
    }


}
