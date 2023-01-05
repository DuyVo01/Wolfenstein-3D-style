using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracting : MonoBehaviour
{
    Camera mainCamera;
    public LayerMask interactiveLayer;
    public float interactiveDistance;

    PlayerStateManager playerStateManager;
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
        if(Physics.Raycast(interactingRay, out hit, interactiveDistance, interactiveLayer))
        {
            Debug.Log(true);
            IInteractable raycastedObject = hit.collider.GetComponent<IInteractable>();
            if(raycastedObject != null && playerStateManager.playerActionInputHandler.isInteract)
            {
                
                playerStateManager.playerActionInputHandler.isInteract = false;
                raycastedObject.OnInteract();
            }
        }
    }
}
