using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public Animator doorAnimator;
    public float timeWaitToClose;

    private bool isOpen = false;
    private Coroutine coroutine = null;
    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnInteract()
    {
        if (isOpen)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            DoorClose();
        }
        else
        {
            DoorOpen();
            coroutine = StartCoroutine(DoorCloseAuto());
        }
    }

    public void DoorOpen()
    {
        isOpen = true;
        audioSource.Play();
        doorAnimator.SetBool("Opening", true);
    }

    public void DoorClose()
    {
        isOpen = false;
        audioSource.Play();
        doorAnimator.SetBool("Opening", false);
    }

    public IEnumerator DoorCloseAuto()
    {
        yield return new WaitForSeconds(timeWaitToClose);
        DoorClose();
    }
}
