// This script represents the behavior of a customer.

#region Import Namespaces

using UnityEngine;
using UnityEngine.AI;

#endregion

#region Core Implementation

[RequireComponent(typeof(NavMeshAgent))] // Ensure the GameObject has a NavMeshAgent component.
public class CustomerBehaviour : MonoBehaviour, ICustomerController
{
    // Destination point where the customer wants to go.
    public Transform destinationPoint;

    // Exit point where the customer will leave after receiving the item.
    public Transform exitPoint;

    // Item wanted by the customer.
    public GameObject itemWanted;

    // References to NavMeshAgent, Animator, and SphereCollider components.
    private NavMeshAgent _agent;
    private Animator _animator;
    private SphereCollider _collider;

    // Current state of the customer.
    private IState _currentState;

    // Flags indicating the status of the customer.
    private bool _itemProvided; // Whether the requested item has been provided.
    private bool _hasReachedDestination; // Whether the customer has reached its destination.

    // Initialize references.
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<SphereCollider>();
    }

    // Set up initial settings and transition to the first state on Start.
    private void Start()
    {
        _agent.updateRotation = true;
        _currentState = new WalkToDestinationState(this);
        TransitionToState(_currentState);
    }

    // Update is called once per frame.
    private void Update()
    {
        // Update the current state if it exists.
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }

    // Transition to a new state.
    public void TransitionToState(IState nextState)
    {
        // Exit the current state.
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        // Set the new state.
        _currentState = nextState;

        // Enter the new state.
        if (_currentState != null)
        {
            _currentState.EnterState();
        }
    }

    // Instruct the customer to walk to the destination point.
    public void WalkToDestinationPoint()
    {
        _animator.SetBool("run", true);
        _agent.SetDestination(destinationPoint.position);
        _collider.enabled = false;
    }

    // Instruct the customer to walk to the exit point.
    public void WalkToExitPoint()
    {
        _animator.SetBool("run", true);
        _agent.SetDestination(exitPoint.position);
        _collider.enabled = false;
    }

    // Check if the customer has reached its destination.
    public bool HasReachedDestination()
    {
        _hasReachedDestination = !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

        if (_hasReachedDestination)
        {
            _collider.enabled = true;
            _animator.SetBool("run", false);
        }

        return _hasReachedDestination;
    }

    // Get the status indicating whether the item has been provided.
    public bool GetItemProvidedStatus()
    {
        return _itemProvided;
    }

    // Set the status indicating whether the item has been provided.
    public void SetItemProvidedStatus(bool status)
    {
        _itemProvided = status;
    }

    // Method called when the customer receives the item.
    public void OnItemReceived()
    {
        // Change the color of the customer to match the received item.
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = itemWanted.GetComponent<Renderer>().sharedMaterial.color;
    }

    // Reset the color of the customer.
    public void ResetColor()
    {
        transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}

#endregion