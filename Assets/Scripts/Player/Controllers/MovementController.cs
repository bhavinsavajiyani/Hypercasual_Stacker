// This script is responsible for controlling player movement and rotation.

#region Importing Namespaces

using UnityEngine;

#endregion

#region Core Implementation

public class MovementController : IHandleMovement
{
    // Movement and rotation speeds.
    private float _movementSpeed, _rotationSpeed;

    // Reference to the CharacterController.
    private CharacterController _characterController;

    // Constructor to initialize the MovementController with required parameters.
    public MovementController(CharacterController characterController, float movementSpeed, float rotationSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
        _rotationSpeed = rotationSpeed;
    }

    // Method to move the player to a target position with a specified movement speed.
    public void Move(Vector3 targetPosition, float movementSpeed)
    {
        _characterController.Move(targetPosition * movementSpeed);
    }

    // Method to rotate the player to a target rotation with a specified rotation speed.
    public void Rotate(Quaternion targetRotation, float rotationSpeed)
    {
        _characterController.transform.rotation = Quaternion.Slerp(_characterController.transform.rotation, targetRotation, rotationSpeed);
    }

    // Method to get the movement speed of the player.
    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

    // Method to get the rotation speed of the player.
    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }
}

#endregion