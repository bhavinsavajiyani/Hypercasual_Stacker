// This script represents a floating joystick UI element (Invisible for our use case).

#region Import Namespaces

using UnityEngine;

#endregion

#region Core Implementation

[RequireComponent(typeof(RectTransform))] // Ensure that this GameObject has a RectTransform component.
[DisallowMultipleComponent] // Prevent multiple instances of this component on a GameObject.
public class FloatingJoystick : MonoBehaviour
{
    // Reference to the RectTransform of the joystick.
    [HideInInspector]
    public RectTransform rectTransform;

    // Reference to the RectTransform of the joystick knob.
    public RectTransform knob;

    // Initialize references.
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component attached to this GameObject.
    }
}

#endregion
