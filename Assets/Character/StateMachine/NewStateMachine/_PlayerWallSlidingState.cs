using UnityEngine;

namespace StateMachine
{
    public class _PlayerWallSlidingState : _PlayerBaseState
    {
        public _PlayerWallSlidingState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            Debug.Log("wall sliding");
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
            if (_cntx.isGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            /*
            if (_cntx.jumpInputDown || _cntx.willBufferJump)
            {
                SwitchState(_factory.Jump());
            }
            /**/
        }
        public override void InitiliseSubState()
        {

        }

        public void WallSlide()
        {
            if (_cntx.isGrounded || _cntx.playerRb.velocity.y > 0)
            {
                _cntx.isWallSliding = false;
                _cntx.playerAnimator.SetBool("isWallSliding", _cntx.isWallSliding);
                return;
            }

            if (_cntx.WallDetectionUpper() || _cntx.WallDetectionMiddle() || _cntx.WallDetectionLower())
            {
                _cntx.isWallSliding = true;
                _cntx.playerAnimator.SetBool("isWallSliding", _cntx.isWallSliding);
                //StopCoroutine(actionsScript.Dash());
                _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, -_cntx.wallSlidingSpeed);
            }
            else
            {
                _cntx.isWallSliding = false;
                _cntx.playerAnimator.SetBool("isWallSliding", _cntx.isWallSliding);
            }
        }
    }
}


