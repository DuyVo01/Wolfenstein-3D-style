using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracting : MonoBehaviour
{
    public PlayerInventory inventory;
    public LayerMask pickapleLayer;
    public float interactiveDistance;
    public string[] pickableTag;

    private Camera mainCamera;
    private PlayerStateManager playerStateManager;
    private Ray interactingRay;

    

    // Start is called before the first frame update
    private void Awake()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
    }

    private void Start()
    {
        mainCamera = playerStateManager.cameraMain;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        interactingRay = mainCamera.ScreenPointToRay(playerStateManager.playerMovementInputHandler.GetMousePosition());
        RaycastHit hit;
        if(Physics.Raycast(interactingRay, out hit, interactiveDistance))
        {
            Debug.DrawLine(mainCamera.transform.position, hit.point) ;
            IInteractable raycastedObject = hit.collider.GetComponent<IInteractable>();
            if(raycastedObject != null && playerStateManager.playerActionInputHandler.isInteract)
            {
                playerStateManager.playerActionInputHandler.isInteract = false;
                raycastedObject.OnInteract();
                if (isPickaple(hit.collider.tag))
                {
                    inventory.AddItem(hit.collider.gameObject);
                }
            }
        }
    }

    private bool isPickaple(string itemTag)
    {
        for (int i = 0; i < pickableTag.Length; i++)
        {
            if (pickableTag[i].Equals(itemTag))
            {
                return true;
            }
        }
        return false;
    }
}
