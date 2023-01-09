using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollecting : MonoBehaviour, IInteractable
{
    public int healthAmount;
    public AudioClip eatFood;

    public void OnInteract()
    {
        
        if (PlayerStatus.currentHealth < 100)
        {
            PlayerStatus.Recover(healthAmount);
            gameObject.SetActive(false);
            EnvironmentObjects.PlayObjectAudio(eatFood);
            DisplayNotice.AddText("Health +30");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(PlayerStatus.currentHealth < 100)
            {
                PlayerStatus.Recover(healthAmount);
                gameObject.SetActive(false);
                EnvironmentObjects.PlayObjectAudio(eatFood);
                DisplayNotice.AddText("Health +30");
            } 
        }
    }
}
