// This script represents an Interface for handling touch input.

# region Importing Namespaces

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

#endregion

#region Core Implementation

public interface IHandleTouchInput
{
    // Method to handle finger down event.
    void HandleFingerDown(Finger touchedFinger);

    // Method to handle finger up event.
    void HandleFingerUp(Finger lostFinger);

    // Method to handle finger move event.
    void HandleFingerMove(Finger movedFinger);

    // // Method to get movement amount based on touch input.
    Vector2 GetMovementAmount();
}

#endregion