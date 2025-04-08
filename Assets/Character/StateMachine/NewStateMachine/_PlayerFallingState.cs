using UnityEngine;


namespace StateMachine
{
    public class _PlayerFallingState : _PlayerMovementState
    {
        public _PlayerFallingState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            base.EnterState();
            _cntx.canCyoteJump = true;
            if (_cntx.fasterFallMultiplier == 0f) _cntx.fasterFallMultiplier = 1f;

        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (_cntx.jumpInputDown)
            {
                _cntx.jumpPressTime = Time.time;
                _cntx.willBufferJump = true;
            }
            CyoteTime();
            Fall();
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
            base.CheckSwitchState();
            if (_cntx.isGrounded)
            {
                SwitchState(_factory.Grounded());
                _cntx.playerAnimator.SetBool("isJumping", false);
            }
            if (_cntx.canCyoteJump && _cntx.jumpInputDown)
            {
                SwitchState(_factory.Jump());
            }

            if (!_cntx.isGrounded && _cntx.isHuggingWall)
            {
                SwitchState(_factory.WallSlide());
                _cntx.playerAnimator.SetBool("isJumping", false);
            }
        }


        public void Fall()
        {
            if (_cntx.playerRb.velocity.y <= 0f)
            {
                //limit fall speed 
                _cntx.playerAnimator.SetBool("isJumping", false);
                _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, Mathf.Max(_cntx.playerRb.velocity.y, _cntx.maxFallSpeed));
                //make fall speed faster
                if (_cntx.playerRb.velocity.y > _cntx.maxFallSpeed)
                {
                    _cntx.playerRb.velocity += Vector2.up * Physics2D.gravity.y * _cntx.fasterFallMultiplier * Time.deltaTime;
                }
                //_cntx.c_MaxHSpeed = _cntx.f_MaxHSpeed;
                //_cntx.c_Acceleration = _cntx.f_Acceleration;
                //_cntx.c_Deceleration = _cntx.f_Deceleration;
            }
            else//JumpApexControll
            {
                //Debug.Log("jump Apex");
                _cntx.playerAnimator.SetBool("isJumping", true);
                //_cntx.playerRb.velocity= 
                //Vector2.Lerp(_cntx.playerRb.velocity, new Vector2(_cntx.playerRb.velocity.x, 0f,0.2f);

            }

        }


        public void CyoteTime()
        {
            if (Time.time - _cntx.LastGrounded > _cntx.cyoteTime)// || Time.time - _cntx.LastTimeWalled > _cntx.cyoteTime)
            {
                _cntx.canCyoteJump = false;
            }
        }
    }
}
