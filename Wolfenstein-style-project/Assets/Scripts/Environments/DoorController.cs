using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public Animator doorAnimator;
    public float timeWaitToClose;

    public bool isOpen = false;
    private Coroutine coroutine = null;

    public float targetPosX;
    public float time;

    private Vector3 originalPos;
    private Vector3 targetPos;
    public float timePassed;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        originalPos = transform.localPosition;
        targetPos = new Vector3(targetPosX, transform.localPosition.y, transform.localPosition.z);
    }
    public void OnInteract()
    {
        audioSource.Play();
        isOpen = !isOpen;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        timePassed = 0;

        if (isOpen)
        {
            coroutine = StartCoroutine(DoorOpen());
        }
        else
        {
            coroutine = StartCoroutine(DoorClose());
        }

    }



    public IEnumerator DoorOpen()
    {
        while(timePassed < time)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, time * Time.deltaTime);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(Vector3.Distance(transform.localPosition, targetPos) < 0.01f)
        {
            coroutine = StartCoroutine(DoorAuto());
        }
    }

    public IEnumerator DoorClose()
    {
        while (timePassed < time)
        {
            timePassed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, time * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator DoorAuto()
    {
        yield return new WaitForSeconds(timeWaitToClose);
        isOpen = false;
    }
}
