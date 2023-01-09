using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollecting : MonoBehaviour, IInteractable
{
    public int scoreToAdd;
    public AudioClip collectGoid;

    public void OnInteract()
    {
        PlayerStatus.AddScore(scoreToAdd);
        gameObject.SetActive(false);
        EnvironmentObjects.PlayObjectAudio(collectGoid);
        DisplayNotice.AddText("Gold +10");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.AddScore(scoreToAdd);
            gameObject.SetActive(false);
            EnvironmentObjects.PlayObjectAudio(collectGoid);
            DisplayNotice.AddText("Gold +10");
        }
    }
}
