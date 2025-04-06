public abstract class BaseStateExmp
{
    //protected bool _isRootState = false;
    protected StateMachineExmp _cntx;
    protected StateFactoryExmp _factory;
    //protected BaseStateExmp _cuurentSuperState;
    protected BaseStateExmp _currentState;
    public BaseStateExmp(StateMachineExmp currentContext, StateFactoryExmp stateFactory)
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
        /*
        if (_cuurentSubState != null)
        {
            _cuurentSubState.UpdateStates();
        }
        else
        {
            //Debug.Log("_cuurentSubState is null");
        }
        /**/
    }
    public void ExitStates()
    {
        ExitState();
        /*
        if (_cuurentSubState != null)
        {
            _cuurentSubState.ExitStates();
        }
        /**/
    }

    protected void SwitchState(BaseStateExmp newState)
    {
        //Debug.Log("switch states: " + newState.ToString());
        //exit current state
        ExitState();
        //ExitStates();
        //enter new state
        newState.EnterState();
        _cntx._currentState = newState;
        /*
        if (newState._isRootState)
        {
            _cntx.currentSuperState = newState.ToString();
        }
        else if (_cuurentSuperState != null)
        {
            _cuurentSuperState.SetSubState(newState);
            _cntx.currentSubState = newState.ToString();
        }
        else
        {
            Debug.Log("other state");
        }
        /**/
    }
    /*
    protected void SetSuperState(BaseStateExmp newSuperState)
    {
        _cuurentSuperState = newSuperState;
    }
    /**/
    protected void SetSubState(BaseStateExmp newState)
    {
        _currentState = newState;
        //newSubState.SetSuperState(this);
    }

}