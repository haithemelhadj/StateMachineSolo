
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    public AiBaseState _currentState;
    AiStateFactory _states;

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
        _states = new AiStateFactory(this);
        _currentState = _states.Movement();
        _currentState.EnterState();
    }
    private void GetComponents()
    {

    }
}
