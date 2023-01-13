using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public float timeWaitToClose;

    public bool isOpen = false;
    protected Coroutine coroutine = null;

    public float targetPosX;
    public float moveSpeed;

    protected Vector3 originalPos;
    protected Vector3 targetPos;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        originalPos = transform.localPosition;
        targetPos = new Vector3(targetPosX, transform.localPosition.y, transform.localPosition.z);
    }
    public virtual void OnInteract()
    {
        isOpen = !isOpen;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if (isOpen)
        {
            coroutine = StartCoroutine(DoorOpen());
        }
        else
        {
            coroutine = StartCoroutine(DoorClose());
        }
    }


    public virtual IEnumerator DoorOpen()
    {
        audioSource.Play();
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeWaitToClose);
        coroutine = StartCoroutine(DoorClose());
    }

    public IEnumerator DoorClose()
    {
        audioSource.Play();
        isOpen = false;
        while (Vector3.Distance(transform.localPosition, originalPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
