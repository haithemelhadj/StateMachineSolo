using UnityEngine;

namespace StateMachine
{
    public class _PlayerWallSlidingState : _PlayerMovementState
    {
        public _PlayerWallSlidingState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            base.EnterState();
            _cntx.isWallSliding = true;
            _cntx.playerAnimator.SetBool("isWallSliding", _cntx.isWallSliding);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, -_cntx.wallSlidingSpeed);
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {
            _cntx.isWallSliding = false;
            _cntx.playerAnimator.SetBool("isWallSliding", _cntx.isWallSliding);
            base.ExitState();

        }
        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            if (_cntx.isGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            if (_cntx.jumpInputDown || _cntx.willBufferJump)
            {
                _cntx.wallJumpPressTime = Time.time;
                SwitchState(_factory.Jump());
                //SwitchState(_factory.WallJump());
            }
            if (!_cntx.isGrounded && !_cntx.isHuggingWall)
            {
                SwitchState(_factory.Fall());
            }
        }

    }
}


