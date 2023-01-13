using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int CurrentMinutes { get; set; }
    public int CurrentSeconds { get; set; }
    private float timePassed = 0;

    private void Update()
    {
        if (!LevelRecord.Instance.IsEndGame)
        {

            timePassed += Time.deltaTime;
            CurrentSeconds = (int)timePassed % 60;
            CurrentMinutes = (int)timePassed / 60;
            LevelRecord.Instance.UpdateTime();
            
        }
        
    }
}
