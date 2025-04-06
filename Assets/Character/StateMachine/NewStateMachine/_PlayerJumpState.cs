
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
            //animator
            _cntx.playerAnimator.SetBool("isJumping", true);
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
            //if is wall jumping
            if (Time.time - _cntx.wallJumpPressTime < _cntx.wallJumpDuration)
            {
                //_cntx.isWallJumping = Time.time - _cntx.wallJumpPressTime < _cntx.wallJumpDuration;
                _cntx.jumpDirection = new Vector2(-_cntx.transform.localScale.x * _cntx.wallJumpDirection.x, _cntx.wallJumpDirection.y);
                //_cntx.playerAnimator.SetBool("isJumping", true);
                //Jumping(_cntx.jumpDirection);

            }
            else // else if is normal jumping
            {
                base.UpdateState();
                _cntx.jumpDirection = new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpForce);
            }
                _cntx.jumpTimeCounter -= Time.deltaTime;
                Jumping(_cntx.jumpDirection);

            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {
        }
        public override void ExitState()
        {
            _cntx.willBufferJump = false;
            _cntx.playerAnimator.SetBool("isJumping", false);
            _cntx.c_MaxHSpeed = _cntx.j_MaxHSpeed;
            _cntx.c_Acceleration = _cntx.j_Acceleration;
            _cntx.c_Deceleration = _cntx.j_Deceleration;
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, _cntx.playerRb.velocity.y * 0.5f);
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
