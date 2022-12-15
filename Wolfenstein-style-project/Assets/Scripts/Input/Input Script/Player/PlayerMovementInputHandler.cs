using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputHandler : MonoBehaviour
{
    private Vector2 _rawMovementInput;
    public Vector2 normalizeMovementInput { get; private set; }


    private Vector2 _mousePosition;

    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        _rawMovementInput = context.ReadValue<Vector2>();
        normalizeMovementInput = _rawMovementInput.normalized;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }
    public Vector2 GetMousePosition()
    {
        return _mousePosition;
    }

}
