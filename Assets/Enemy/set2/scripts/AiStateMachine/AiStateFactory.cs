
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

enum AiStates
{
    Movement,
    Action,

    Idle,
    patrol,
    Search,
    Chase,
    Retreat,
    Jump,
    Fall,
    
    /*
    None,
    Movement,
    Action,
    /*
    Grounded,
    Idle,
    Jump,
    Fall,
    Dash,
    WallSlide,
    WallJump
    /**/
}
public class AiStateFactory
{
    AiStateMachine context;
    Dictionary<AiStates, AiBaseState> _states = new Dictionary<AiStates, AiBaseState>();
    public AiStateFactory(AiStateMachine currentContext)
    {
        context = currentContext;
        //_states[States.None] = new AiAnyStateCopy(context, this);
        _states[AiStates.Movement] = new AiMovementState(context, this);
    }


    //Parent state
    #region Parent state
    public AiBaseState Movement()
    {
        return _states[AiStates.Movement];
    }
    
    public AiBaseState Action()
    {
        return (_states[AiStates.Action]);
    }
    /**/
    #endregion


    //Child States
    #region Child States
    /*
    public _PlayerBaseState Grounded()
    {
        return _states[AiStates.Grounded];
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
