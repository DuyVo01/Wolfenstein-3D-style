using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionInputHandler : MonoBehaviour
{
    private bool _isShoot;

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

    public bool GetShootInput()
    {
        return _isShoot;
    }
    
}
