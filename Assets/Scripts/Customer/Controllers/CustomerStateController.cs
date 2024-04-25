// This script represents a base class for controlling customer states in a state machine.

#region Core Implementation

public class CustomerStateController : IState
{
    protected ICustomerController _customer; // Reference to the customer being controlled.

    // Constructor to initialize the CustomerStateController with a customer.
    public CustomerStateController(ICustomerController customer)
    {
        this._customer = customer;
    }

    // Method called when entering this state.
    public virtual void EnterState()
    {
        // To be overridden in derived classes.
    }

    // Method called when exiting this state.
    public virtual void ExitState()
    {
        // To be overridden in derived classes.
    }

    // Method called to update this state.
    public virtual void UpdateState()
    {
        // To be overridden in derived classes.
    }
}

#endregion