
    using UnityEngine;

namespace StateMachine
{
    public class _PlayerJumpState : _PlayerMovementState
    {
        public _PlayerJumpState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            base.EnterState();
            //
            _cntx.willBufferJump = false;
            //null y velocity
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, 0f);
            //set air movement speed
            _cntx.c_MaxHSpeed = _cntx.a_MaxHSpeed;
            _cntx.c_Acceleration = _cntx.a_Acceleration;
            _cntx.c_Deceleration = _cntx.a_Deceleration;
            //set jump start counter
            _cntx.jumpTimeCounter = _cntx.jumpTime;
        }
        public override void UpdateState()
        {
            base.UpdateState();
            _cntx.jumpTimeCounter -= Time.deltaTime;
            _cntx.jumpDirection = new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpForce);
            Jumping(_cntx.jumpDirection);
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {
        }
        public override void ExitState()
        {
            _cntx.willBufferJump = false;
            //_cntx.isWallJumping = false;
        }
        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            if ((_cntx.jumpInputUp || _cntx.jumpTimeCounter < 0))
            {
                SwitchState(_factory.Fall());
            }
        }
        
        public void Jumping(Vector2 JumpDirection)
        {
            _cntx.playerRb.velocity = JumpDirection;
        }
        public override void InitiliseSubState()
        {

        }
    }
}
