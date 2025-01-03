using System.Collections.Generic;
using StateMachine;
namespace StateMachine
{
    enum _playerStates
    {
        Movement,
        Action,
        Grounded,
        Jump,
        Fall,
        Dash,
        WallSlide,
        WallJump
    }
    public class _PlayerStateFactory
    {
        _PlayerStateMachine context;
        Dictionary<_playerStates,_PlayerBaseState> _states=new Dictionary<_playerStates,_PlayerBaseState>();
        public _PlayerStateFactory(_PlayerStateMachine currentContext)
        {
            context = currentContext;
            _states[ _playerStates.Movement] = new _PlayerMovementState(context, this);
            _states[ _playerStates.Action] = new _PlayerActionState(context, this);
            _states[ _playerStates.Grounded]= new _PlayerGroundedState(context, this);
            _states[ _playerStates.Jump]= new _PlayerJumpState(context, this);
            _states[ _playerStates.Fall]= new _PlayerFallingState(context, this);
            _states[ _playerStates.Dash]= new _PlayerDashState(context, this);
            _states[ _playerStates.WallSlide]= new _PlayerWallSlidingState(context, this);
        }

        //Parent state
        #region Parent state
        
        public _PlayerBaseState Movement()
        {
            return _states[_playerStates.Movement];
            //return new _PlayerMovementState(context, this);
        }
        public _PlayerBaseState Action()
        {
            return (_states[_playerStates.Action]);
            //return new _PlayerActionState(context, this);
        }
        #endregion


        //Child States
        #region Child States
        public _PlayerBaseState Grounded()
        {
            return _states[_playerStates.Grounded];
            //return new _PlayerGroundedState(context, this);
        }
        public _PlayerBaseState Jump()
        {
            return ( _states[_playerStates.Jump] );
            //return new _PlayerJumpState(context, this);
        }
        public _PlayerBaseState Fall()
        {
            
            return ( _states[_playerStates.Fall] );
            //return new _PlayerFallingState(context, this);
        }
        /**/
        #endregion



        //actions
        #region actions
        
        public _PlayerBaseState Dash()
        {
            return (_states[_playerStates.Dash]);
            //return new _PlayerDashState(context, this);
        }
        
        public _PlayerBaseState WallSlide()
        {
            return (_states[_playerStates.WallSlide]);
            //return new _PlayerWallSlidingState(context, this);
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

