using StateMachine;
namespace StateMachine
{

    public class _PlayerStateFactory
    {
        _PlayerStateMachine context;
        public _PlayerStateFactory(_PlayerStateMachine currentContext)
        {
            context = currentContext;
        }

        //Parent state
        #region Parent state
        
        public _PlayerBaseState Movement()
        {
            return new _PlayerMovementState(context, this);
        }
        public _PlayerBaseState Action()
        {
            return new _PlayerActionState(context, this);
        }
        #endregion


        //Child States
        #region Child States
        public _PlayerBaseState Grounded()
        {
            return new _PlayerGroundedState(context, this);
        }
        public _PlayerBaseState Jump()
        {
            return new _PlayerJumpState(context, this);
        }
        public _PlayerBaseState Fall()
        {
            return new _PlayerFallingState(context, this);
        }
        /**/
        #endregion



        //actions
        #region actions
        
        public _PlayerBaseState Dash()
        {
            return new _PlayerDashState(context, this);
        }
        
        public _PlayerBaseState WallSlide()
        {
            return new _PlayerWallSlidingState(context, this);
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
}

