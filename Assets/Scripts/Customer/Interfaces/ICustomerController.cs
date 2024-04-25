// This is an Interface for controlling customer behavior.

#region Core Implementation

public interface ICustomerController
{
    // Transition to the next state in the customer's state machine.
    void TransitionToState(IState nextState);

    // Instruct the customer to walk to a destination point.
    void WalkToDestinationPoint();

    // Instruct the customer to walk to an exit point.
    void WalkToExitPoint();

    // Check if the customer has reached its destination.
    bool HasReachedDestination();

    // Set the status indicating whether the requested item has been provided to the customer.
    void SetItemProvidedStatus(bool status);

    // Get the status indicating whether the requested item has been provided to the customer.
    bool GetItemProvidedStatus();

    // Reset the color of the customer.
    void ResetColor();

    // Method called when the customer receives an item.
    void OnItemReceived();
}

#endregion