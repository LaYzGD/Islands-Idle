using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputsHandler : MonoBehaviour
{
    private Vector2 _movementInput;
    private bool _isMousePressed;

    public event Action OnEscapePressed;

    public bool IsMousePressed => _isMousePressed;

    public void MoveInput(InputAction.CallbackContext callback)
    {
        _movementInput = callback.ReadValue<Vector2>();
    }

    public void MousePressedInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            _isMousePressed = true;
        }

        if (callback.canceled)
        {
            _isMousePressed = false;
        }
    }

    public void OnEscapeButtonPressed(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            OnEscapePressed?.Invoke();
        }
    }

    public Vector3 GetInputVector()
    {
        return new Vector3(_movementInput.x, 0f, _movementInput.y);
    }
}
