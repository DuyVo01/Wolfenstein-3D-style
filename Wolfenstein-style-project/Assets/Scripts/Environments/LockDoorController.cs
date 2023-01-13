using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorController : DoorController
{
    public string requiredItem;
    public PlayerInventory playerInventory;

    public override void OnInteract()
    {
        if (playerInventory.GetItemCount(requiredItem) > 0)
        {
            playerInventory.RemoveItem(requiredItem);
            base.OnInteract();
        }
    }

    public override IEnumerator DoorOpen()
    {
        audioSource.Play();
        while (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //yield return new WaitForSeconds(timeWaitToClose);
        //coroutine = StartCoroutine(DoorClose());
    }
}
