// This script is responsible for handling touch input and controlling a floating joystick.

#region Import Namespaces

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

#endregion

#region Core Implementation

public class TouchInputController : IHandleTouchInput
{
    // Reference to the floating joystick for touch input.
    private FloatingJoystick _joystick;

    // Size of the joystick.
    private Vector2 _joystickSize;

    // Amount of movement based on touch input.
    private Vector2 _movementAmount;

    // Finger used for movement.
    private Finger _movementFinger;

    // Custom Event delegate for notifying movement status changes.
    public delegate void OnMove(bool isMoving);
    public static event OnMove onMove;

    // Constructor to initialize the TouchInputController with required parameters.
    public TouchInputController(FloatingJoystick joystick, Vector2 joystickSize, Finger movementFinger)
    {
        _joystick = joystick;
        _joystickSize = joystickSize;
        _movementFinger = movementFinger;
    }

    // Method to handle finger down event.
    public void HandleFingerDown(Finger touchedFinger)
    {
        // If no movement finger is set and the touched finger is on the left side of the screen, set it as the movement finger.
        if (_movementFinger == null && touchedFinger.screenPosition.x < Screen.width)
        {
            _movementFinger = touchedFinger;
            _movementAmount = Vector2.zero;
            _joystick.rectTransform.sizeDelta = _joystickSize;
            _joystick.rectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition, _joystickSize);
        }
    }

    // Method to handle finger move event.
    public void HandleFingerMove(Finger movedFinger)
    {
        // If the moved finger matches the movement finger, update joystick position and movement amount.
        if (movedFinger == _movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = _joystickSize.x / 2.0f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            // Calculate joystick knob position based on distance from initial touch position.
            if (Vector2.Distance(currentTouch.screenPosition, _joystick.rectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - _joystick.rectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - _joystick.rectTransform.anchoredPosition;
            }

            _joystick.knob.anchoredPosition = knobPosition;
            _movementAmount = knobPosition / maxMovement;

            // Notify movement event listeners that movement is occurring.
            onMove(true);
        }
    }

    // Method to handle finger up event.
    public void HandleFingerUp(Finger lostFinger)
    {
        // If the lost finger matches the movement finger, reset movement finger and joystick position.
        if (lostFinger == _movementFinger)
        {
            _movementFinger = null;
            _joystick.knob.anchoredPosition = Vector2.zero;
            _movementAmount = Vector2.zero;

            // Notify movement event listeners that movement has stopped.
            onMove(false);
        }
    }

    // Method to get the current movement amount.
    public Vector2 GetMovementAmount()
    {
        return _movementAmount;
    }

    // Method to clamp the initial joystick position within the screen boundaries.
    private Vector2 ClampStartPosition(Vector2 startPosition, Vector2 joystickSize)
    {
        // Clamp X position to ensure the joystick is not partially off-screen.
        if (startPosition.x < joystickSize.x)
        {
            startPosition.x = joystickSize.x;
        }

        // Clamp Y position to keep the joystick within the screen bounds.
        if (startPosition.y < joystickSize.y)
        {
            startPosition.y = joystickSize.y;
        }
        else if (startPosition.y > Screen.height - joystickSize.y)
        {
            startPosition.y = Screen.height - joystickSize.y;
        }

        return startPosition;
    }
}

#endregion