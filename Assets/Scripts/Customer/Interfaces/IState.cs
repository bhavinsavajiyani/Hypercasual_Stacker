// This script represents an Interface defining the behavior of a state in a state machine.

#region Core Implementation

public interface IState
{
    // Method called when entering this state.
    void EnterState();

    // Method called to update this state.
    void UpdateState();

    // Method called when exiting this state.
    void ExitState();
}

#endregion