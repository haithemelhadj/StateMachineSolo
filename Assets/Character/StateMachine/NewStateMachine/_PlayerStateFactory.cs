using System.Collections.Generic;
namespace StateMachine
{
    enum _playerStates
    {
        //Movement,
        //Action,

        Grounded,
        Jump,
        Fall,
        Dash,
        WallSlide,
        WallJump,
        Attack,
        iFrames,
    }
    public class _PlayerStateFactory
    {
        _PlayerStateMachine context;
        Dictionary<_playerStates, _PlayerBaseState> _states = new Dictionary<_playerStates, _PlayerBaseState>();
        public _PlayerStateFactory(_PlayerStateMachine currentContext)
        {
            context = currentContext;
            //_states[_playerStates.Movement] = new _PlayerMovementState(context, this);
            //_states[_playerStates.Action] = new _PlayerActionState(context, this);
            _states[_playerStates.Grounded] = new _PlayerGroundedState(context, this);
            _states[_playerStates.Jump] = new _PlayerJumpState(context, this);
            _states[_playerStates.Fall] = new _PlayerFallingState(context, this);
            _states[_playerStates.Dash] = new _PlayerDashState(context, this);
            _states[_playerStates.WallSlide] = new _PlayerWallSlidingState(context, this);
            _states[_playerStates.WallJump] = new _PlayerWallJumpState(context, this);
            _states[_playerStates.Attack] = new _PlayerAttackState(context, this);
            _states[_playerStates.Attack] = new _PlayerAttackState(context, this);
            _states[_playerStates.Attack] = new _PlayerAttackState(context, this);
            _states[_playerStates.iFrames] = new _PlayerIFramesState(context, this);
        }

        //Parent state


        //Child States
        #region Child States
        public _PlayerBaseState Grounded()
        {
            return _states[_playerStates.Grounded];
        }
        public _PlayerBaseState Jump()
        {
            return (_states[_playerStates.Jump]);
        }
        public _PlayerBaseState Fall()
        {

            return (_states[_playerStates.Fall]);
        }
        /**/
        #endregion



        //actions
        #region actions

        public _PlayerBaseState Dash()
        {
            return (_states[_playerStates.Dash]);
        }
        #endregion

        #region wall

        public _PlayerBaseState WallSlide()
        {
            return (_states[_playerStates.WallSlide]);
            //return new _PlayerWallSlidingState(context, this);
        }
        public _PlayerBaseState WallJump()
        {
            return (_states[_playerStates.WallJump]);
            //return new _PlayerWallSlidingState(context, this);
        }

        /**/
        #endregion

        #region Parallel
        public _PlayerBaseState Attack()
        {
            return (_states[_playerStates.Attack]);
        }

        public _PlayerBaseState iFrames()
        {
            return (_states[_playerStates.iFrames]);
        }


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

