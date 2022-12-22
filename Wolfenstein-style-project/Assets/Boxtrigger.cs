using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("HIT");
        }
    }
}
