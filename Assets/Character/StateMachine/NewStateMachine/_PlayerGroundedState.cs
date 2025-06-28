using UnityEngine;
namespace StateMachine
{
    public class _PlayerGroundedState : _PlayerMovementState
    {
        public _PlayerGroundedState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Debug.Log("Grounded State Entered");
            base.EnterState();
            //animation
            _cntx.playerAnimator.SetBool("isGrounded", true);
            //set speed
            //_cntx.c_MaxHSpeed = _cntx.r_MaxHSpeed;
            //_cntx.c_Acceleration = _cntx.r_Acceleration;
            //_cntx.c_Deceleration = _cntx.r_Deceleration;
            //cyote time
            _cntx.canCyoteJump = false;
            //Buffer time
            if (Time.time - _cntx.jumpPressTime > _cntx.jumpBufferTime)
            {
                _cntx.willBufferJump = false;
            }
        }
        public override void UpdateState()
        {
            base.UpdateState();
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {
            _cntx.LastGrounded = Time.time;
            _cntx.playerAnimator.SetBool("isGrounded", false);
        }
        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            if (_cntx.jumpInputDown || _cntx.willBufferJump)
            {
                SwitchState(_factory.Jump());
            }
            if (!_cntx.isGrounded && _cntx.playerRb.velocity.y < 0f)
            {
                SwitchState(_factory.Fall());

            }

        }
    }
}
