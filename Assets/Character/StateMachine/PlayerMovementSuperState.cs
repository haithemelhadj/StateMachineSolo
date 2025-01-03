using UnityEngine;

namespace StateMachine
{
    public class PlayerMovementSuperState : PlayerBaseState
    {

        public PlayerMovementSuperState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {

        }
        public override void CheckSwitchState()
        {

        }
        public override void InitiliseSubState()
        {

        }
    }
}


