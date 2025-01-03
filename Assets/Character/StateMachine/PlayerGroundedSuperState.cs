using UnityEngine;

namespace StateMachine
{
    public class PlayerGroundedSuperState : PlayerBaseState
    {
        public PlayerGroundedSuperState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            _cntx.c_MaxHSpeed = _cntx.g_MaxHSpeed;
            _cntx.c_Acceleration= _cntx.g_Acceleration;
            _cntx.c_Deceleration = _cntx.g_Deceleration;
        }
        public override void ExitState()
        {
            _cntx.canJump = true;
            _cntx.LastGrounded = Time.time;

        }
        public override void UpdateState()
        {            
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void CheckSwitchState()
        {            
            if(_cntx.jumpInputDown || _cntx.willBufferJump)
            {
                SetSubState(_factory.Jump());                
            }
            if(!_cntx.isGrounded)
            {
                SetSubState(_factory.Fall());                
            }
        }
        public override void InitiliseSubState()
        {

        }
    }
}
