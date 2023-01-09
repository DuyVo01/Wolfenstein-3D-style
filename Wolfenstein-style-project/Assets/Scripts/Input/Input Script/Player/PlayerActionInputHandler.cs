using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionInputHandler : MonoBehaviour
{
    private bool _isShoot;
    public bool isInteract;
    public bool isReloading;

    private float _interactingStart;
    public float _interactingTime;

    private void Update()
    {
        if (isInteract && Time.time > _interactingStart + _interactingTime)
        {
            isInteract = false;
        }
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isShoot = true;
        }
        if (context.canceled)
        {
            _isShoot = false;
        }
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isReloading = true;
        }
        if (context.canceled)
        {
            isReloading = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _interactingStart = Time.time;
            isInteract = true;
        }
        if (context.canceled )
        {
            isInteract = false;
        }
    }

    public bool GetShootInput()
    {
        return _isShoot;
    }
    
}
