// State representing the behavior of a customer walking away after receiving an item.

#region Core Implementation

public class WalkAwayState : CustomerStateController
{
    // Constructor to initialize the WalkAwayState with a customer.
    public WalkAwayState(ICustomerController customer) : base(customer)
    {

    }

    // Method called when entering this state.
    public override void EnterState()
    {
        // Instruct the customer to walk to the exit point.
        _customer.WalkToExitPoint();
    }

    // Method called to update this state.
    public override void UpdateState()
    {
        // Check if the customer has reached the exit point.
        if (_customer.HasReachedDestination())
        {
            // If the exit point is reached, transition to null state (no state).
            _customer.TransitionToState(new WalkToDestinationState(_customer));
        }
    }

    // Method called when exiting this state.
    public override void ExitState()
    {
        // Reset the item collection status.
        _customer.SetItemProvidedStatus(false);
    }
}

#endregion