
public abstract class AiBaseState
{
    //protected bool _isRootState = false;
    protected AiStateMachine _cntx;
    protected AiStateFactory _factory;
    //protected AiBaseState _cuurentSuperState;
    protected AiBaseState _currentState;
    public AiBaseState(AiStateMachine currentContext, AiStateFactory stateFactory)
    {
        _cntx = currentContext;
        _factory = stateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitiliseSubState();

    public void UpdateStates()
    {
        UpdateState();
    }
    public void ExitStates()
    {
        ExitState();
    }

    protected void SwitchState(AiBaseState newState)
    {
        //Debug.Log("switch states: " + newState.ToString());
        //exit current state
        ExitState();
        //ExitStates();
        //enter new state
        newState.EnterState();
        _cntx._currentState = newState;
        
    }
    protected void SetSubState(AiBaseState newState)
    {
        _currentState = newState;
    }

}