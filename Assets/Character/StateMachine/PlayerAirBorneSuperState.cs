using UnityEngine;

namespace StateMachine
{
    public class PlayerAirBorneSuperState : PlayerBaseState
    {

        public PlayerAirBorneSuperState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            //if (Time.time - _cntx.jumpPressTime > _cntx.jumpBufferTime)
            //{
            //    _cntx.willBufferJump = false;
            //}
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

