using UnityEngine;

public class StateMachineExmp : MonoBehaviour
{
    public BaseStateExmp _currentState;
    StateFactoryExmp _states;

    public string currentSuperState;
    public string currentSubState;

    private void Awake()
    {
        InitializeState();
        GetComponents();
    }

    private void Update()
    {

        //logic
        _currentState.UpdateStates();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
    }

    private void InitializeState()
    {
        _states = new StateFactoryExmp(this);
        _currentState = _states.Movement();
        _currentState.EnterState();
    }
    private void GetComponents()
    {

    }
}
