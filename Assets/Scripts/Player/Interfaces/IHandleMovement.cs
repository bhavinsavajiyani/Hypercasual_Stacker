// This script represents an Interface for handling movement (Primarily for Character Movement).

#region Importing Namespaces

using UnityEngine;

#endregion

#region Core Implementation

public interface IHandleMovement
{
    // Move the character to a target position with specified movement speed.
    void Move(Vector3 targetPosition, float movementSpeed);

    // Rotate the character to a target rotation with specified rotation speed.
    void Rotate(Quaternion targetRotation, float rotationSpeed);
    
    // Method to get movement speed of character
    float GetMovementSpeed();
    
    // Method to get rotation speed of character
    float GetRotationSpeed();
}

#endregion