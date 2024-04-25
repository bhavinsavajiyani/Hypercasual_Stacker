// State representing the behavior of a customer walking to it's destination.

#region Core Implementation

public class WalkToDestinationState : CustomerStateController
{
    // Constructor to initialize the WalkToDestinationState with a customer.
    public WalkToDestinationState(ICustomerController customer) : base(customer)
    {

    }

    // Method called when entering this state.
    public override void EnterState()
    {
        // Reset customer color and initiate walking to destination.
        _customer.ResetColor();
        _customer.WalkToDestinationPoint();
    }

    // Method called to update this state.
    public override void UpdateState()
    {
        // Check if the customer has reached its destination.
        if (_customer.HasReachedDestination())
        {
            // If destination is reached, transition to the WaitForItemState.
            _customer.TransitionToState(new WaitForItemState(_customer));
        }
    }
}

#endregion