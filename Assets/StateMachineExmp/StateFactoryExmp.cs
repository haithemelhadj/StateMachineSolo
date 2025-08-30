using System.Collections.Generic;

enum States
{
    Movement,
    Action,

    /*
    Idle,
    patrol,
    Search,
    Chase,
    Retreat,

    Jump,
    Fall,
    Grounded,
    Idle,
    Dash,
    WallSlide,
    WallJump
    /**/
}
public class StateFactoryExmp
{
    StateMachineExmp context;
    Dictionary<States, BaseStateExmp> _states = new Dictionary<States, BaseStateExmp>();
    public StateFactoryExmp(StateMachineExmp currentContext)
    {
        context = currentContext;
        _states[States.Action] = new AnyStateCopyExmp(context, this);
        _states[States.Movement] = new AnyStateCopyExmp(context, this);
    }

    public BaseStateExmp None()
    {
        return _states[States.Action];
    }

    //Parent state
    #region Parent state
    public BaseStateExmp Movement()
    {
        return _states[States.Movement];
    }
    /*
    public _PlayerBaseState Action()
    {
        return (_states[States.Action]);
    }
    /**/
    #endregion


    //Child States
    #region Child States
    /*
    public _PlayerBaseState Grounded()
    {
        return _states[States.Grounded];
    }
    public _PlayerBaseState Jump()
    {
        return (_states[States.Jump]);
    }
    public _PlayerBaseState Fall()
    {

        return (_states[States.Fall]);
    }
    /**/
    #endregion



    //actions
    #region actions
    /*
    public _PlayerBaseState Dash()
    {
        return (_states[States.Dash]);
    }

    public _PlayerBaseState WallSlide()
    {
        return (_states[States.WallSlide]);
    }
    public _PlayerBaseState WallJump()
    {
        return (_states[States.WallJump]);
    }

    /**/
    #endregion

    //movement sub sub states
    #region movement sub sub states
    /*
    public _PlayerBaseState Idle()
    {
        return new _PlayerIdleState(context, this);
    }


    public _PlayerBaseState Walk()
    {
        return new _PlayerWalkState(context, this);
    }
    public _PlayerBaseState Run()
    {
        return new _PlayerRunState(context, this);
    }
    /**/
    #endregion

}
