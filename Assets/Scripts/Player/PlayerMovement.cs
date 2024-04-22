// This script is responsible for Player Movement, using Touch Input.

#region Imprting Namespaces

using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

#endregion

#region Core Implementation

[RequireComponent(typeof(CharacterController))] // Ensure that this GameObject has a Character Controller component
[DisallowMultipleComponent]                     // Prevent multiple instances of this component on GameObject
public class PlayerMovement : MonoBehaviour
{
    // Movement and Rotation speeds
    [SerializeField] private float _movementSpeed = 3.5f;
    [SerializeField] private float _rotationSpeed = 3.0f;

    // Size of Invisible Joystick
    [SerializeField] private Vector2 _joystickSize = Vector2.one;
    
    // Reference to Invisible Joystick for touch input
    [SerializeField] private FloatingJoystick _joystick;
    
    // Reference to CharacterController component
    [SerializeField] private CharacterController _characterController;

    // Controllers for Touch Input & Movement 
    private TouchInputController _touchInputController;
    private MovementController _movementController;

    // Animator Component for animation control
    private Animator _animator;

    // Finger for touch movement
    private Finger _movementFinger;

    // Current movement position
    private Vector3 _movement;

    // Target rotation for the player
    private Quaternion _targetRotation;

    // Initialize relevant references.
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController.detectCollisions = true;
        _touchInputController = new TouchInputController(_joystick, _joystickSize, _movementFinger);
        _movementController = new MovementController(_characterController, _movementSpeed, _rotationSpeed);
    }

    // Enable Touch Input events when this component is enabled.
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += _touchInputController.HandleFingerDown;
        ETouch.Touch.onFingerMove += _touchInputController.HandleFingerMove;
        ETouch.Touch.onFingerUp += _touchInputController.HandleFingerUp;
        TouchInputController.onMove += OnMovementInputReceived;
    }

    // Update player movement based on touch input
    private void Update()
    {
        // Get movement input from touch controller
        _movement = new Vector3(
                _touchInputController.GetMovementAmount().x,
                0,
                _touchInputController.GetMovementAmount().y
            ).normalized;

        // Rotate player towards movement direction, if moving.
        if (_movement != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(_movement, Vector3.up);
            _movementController.Rotate(_targetRotation, _rotationSpeed * Time.deltaTime);
        }

        // Move the player as per movement input
        _movementController.Move(_movement, _movementSpeed * Time.deltaTime);
    }

    // Disable Touch Input events when this component is disabled.
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= _touchInputController.HandleFingerDown;
        ETouch.Touch.onFingerMove -= _touchInputController.HandleFingerMove;
        ETouch.Touch.onFingerUp -= _touchInputController.HandleFingerUp;
        TouchInputController.onMove -= OnMovementInputReceived;
        EnhancedTouchSupport.Disable();
    }

    // Update animator parameter based on movement status
    private void OnMovementInputReceived(bool status)
    {
        _animator.SetBool("run", status);
    }
}

#endregion