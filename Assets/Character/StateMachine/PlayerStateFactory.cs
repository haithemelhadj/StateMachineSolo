
namespace StateMachine
{
    public class PlayerStateFactory
    {
        PlayerStateMachine context;
        //super state
        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedSuperState(context, this);
        }
        public PlayerBaseState AirBorne()
        {
            return new PlayerAirBorneSuperState(context, this);
        }
        public PlayerBaseState Action()
        {
            return new PlayerActionSuperState(context, this);
        }

        // airborne
        public PlayerBaseState Jump()
        {
            return new PlayerJumpState(context, this);
        }
        public PlayerBaseState Fall()
        {
            return new PlayerFallingState(context, this);
        }


        //movement
        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            context = currentContext;
        }
        public PlayerBaseState Idle()
        {
            return new PlayerIdleState(context, this);
        }
        public PlayerBaseState Walk()
        {
            return new PlayerWalkState(context, this);
        }
        public PlayerBaseState Run()
        {
            return new PlayerRunState(context, this);
        }

        //actions
        public PlayerBaseState Dash()
        {
            return new PlayerDashState(context, this);
        }
        public PlayerBaseState WallSlide()
        {
            return new PlayerWallSlidingState(context, this);
        }

    }
}
