using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Locomotion
{
    private CharacterController _characterController;
    private Transform _body;
    private LocomotionData _data;

    public Locomotion(CharacterController controller, Transform body, LocomotionData data)
    {
        _characterController = controller;
        _body = body;
        _data = data;
    }

    public void Move(Vector3 movementInput)
    {
        var direction = _body.forward * movementInput.normalized.magnitude * _data.MovementSpeed;
        _characterController.Move(direction * Time.deltaTime);
    }

    public void Rotate(Vector3 movementInput) 
    {
        if (movementInput == Vector3.zero) return;

        var rotation = Quaternion.LookRotation(movementInput.ToIsometric(), Vector3.up);
        _body.rotation = Quaternion.RotateTowards(_body.rotation, rotation, _data.RotationSpeed * Time.deltaTime);
    }
}
