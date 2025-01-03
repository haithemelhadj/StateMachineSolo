using UnityEngine;

namespace StateMachine
{
    public class PlayerFallingState : PlayerBaseState
    {
        //float originalGravity;
        public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            //originalGravity = _cntx.playerRb.gravityScale;
        }
        public override void UpdateState()
        {
            
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
            if(_cntx.isGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            if(_cntx.canJump && _cntx.jumpInputDown)
            {
                SwitchState(_factory.Jump());
            }
        }
        public override void InitiliseSubState()
        {

        }

        public void Fall()
        {
            //if (_cntx.playerRb.velocity.y < 0f && _cntx.playerRb.velocity.y > _cntx.jumpApexThreshhold)
            //{
            //    //change x & y velocity
            //    //Vector2.MoveTowards(_cntx.playerRb.velocity, new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpApexThreshhold), _cntx.fasterFallMultiplier * Time.deltaTime);
            //    _cntx.playerRb.gravityScale = originalGravity * _cntx.jumpApexGravityMultiplier;
            //}
            if (_cntx.playerRb.velocity.y < _cntx.jumpApexThreshhold)
            {
                //_cntx.c_MaxHSpeed = _cntx.a_MaxHSpeed;
                //_cntx.c_Acceleration = _cntx.a_Acceleration;
                //_cntx.c_Deceleration = _cntx.a_Deceleration;
                //_cntx.playerRb.gravityScale = originalGravity;
                Vector2.MoveTowards(_cntx.playerRb.velocity, new Vector2(_cntx.playerRb.velocity.x, _cntx.maxFallSpeed), _cntx.fasterFallMultiplier * Time.deltaTime);
            }
        }

        public void CyoteTime()
        {
            //
            if (_cntx.playerRb.velocity.y < 0f && _cntx.playerRb.velocity.y > _cntx.jumpApexThreshhold)
            {
                //change x & y velocity
            }
            if (Time.time - _cntx.LastGrounded > _cntx.cyoteTime || Time.time - _cntx.LastTimeWalled > _cntx.cyoteTime)
            {
                _cntx.canJump = false;
            }
        }
    }
}
