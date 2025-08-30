using System.Collections.Generic;

enum AiStates
{
    Movement,
    Action,

    Idle,
    patrol,
    Search,
    Chase,
    Attack,
    //Retreat,
    //Jump,
    //Fall,

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
        _states[AiStates.Action] = new AiActionState(context, this);

        _states[AiStates.Idle] = new AiIdleState(context, this);
        _states[AiStates.patrol] = new AiPatrolState(context, this);
        _states[AiStates.Chase] = new AiChaseState(context, this);
        _states[AiStates.Search] = new AiSearchState(context, this);
        _states[AiStates.Attack] = new AiAttackState(context, this);
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
    public AiBaseState Idle()
    {
        return _states[AiStates.Idle];
    }

    public AiBaseState Patrol()
    {
        return _states[AiStates.patrol];
    }

    public AiBaseState Chase()
    {
        return _states[AiStates.Chase];
    }

    public AiBaseState Search()
    {
        return _states[AiStates.Search];
    }

    #endregion



    //actions
    #region actions
    public AiBaseState Attack()
    {
        return (_states[AiStates.Attack]);
    }
    /*
    public AiBaseState Dash()
    {
        return (_states[AiStates.Dash]);
    }

    public AiBaseState WallSlide()
    {
        return (_states[AiStates.WallSlide]);
    }
    public AiBaseState WallJump()
    {
        return (_states[AiStates.WallJump]);
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
