using UnityEngine;

//[CreateAssetMenu(fileName = "Base State", menuName = "Player/States/Base")]
public abstract class _PlayerBaseState : ScriptableObject
{
    protected _PlayerStateMachine _cntx;
    protected _PlayerStateFactory _factory;
    public _PlayerBaseState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
    {
        _cntx = currentContext;
        _factory = playerStateFactory;
    }

    public void Initialize(_PlayerStateMachine context, _PlayerStateFactory factory)
    {
        _cntx = context;
        _factory = factory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void LateUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();


    protected void SwitchState(_PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();
        _cntx._currentState = newState;
    }


}

