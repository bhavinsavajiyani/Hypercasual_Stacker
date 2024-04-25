// State representing the behavior of a customer waiting for an item.

#region Core Implementation

public class WaitForItemState : CustomerStateController
{
    // Constructor to initialize the WaitForItemState with a customer.
    public WaitForItemState(ICustomerController customer) : base(customer)
    {

    }

    // Method called to update this state.
    public override void UpdateState()
    {
        // Check if the requested item has been provided to the customer.
        if (_customer.GetItemProvidedStatus())
        {
            // If item is received, transition to the WalkAwayState.
            _customer.TransitionToState(new WalkAwayState(_customer));
        }
    }

    // Method called when exiting this state.
    public override void ExitState()
    {
        // Perform actions when the customer receives the item.
        _customer.OnItemReceived();
    }
}

#endregion