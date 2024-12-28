using UnityEngine;

namespace StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        
        float oldGravity;
        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            

            //null y velocity
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, 0f);
            //set air movement speed
            _cntx.c_MaxHSpeed = _cntx.a_MaxHSpeed;
            _cntx.c_Acceleration = _cntx.a_Acceleration;
            _cntx.c_Deceleration = _cntx.a_Deceleration;
            //set jump start counter
            _cntx.jumpTimeCounter = _cntx.jumpTime;
        }
        public override void ExitState()
        {
            //null y velocity
            _cntx.playerRb.velocity = new Vector2(_cntx.playerRb.velocity.x, 0f);
            //
            //_cntx.c_MaxHSpeed = _cntx.j_MaxHSpeed;
            //_cntx.c_Acceleration = _cntx.j_Acceleration;
            //_cntx.c_Deceleration = _cntx.j_Deceleration;
            //Vector2.MoveTowards(_cntx.playerRb.velocity, new Vector2(_cntx.playerRb.velocity.x, 0f), _cntx.playerRb.velocity.y * 0.2f);
        }
        public override void UpdateState()
        {
            _cntx.jumpTimeCounter -= Time.deltaTime;
            _cntx.jumpDirection = new Vector2(_cntx.playerRb.velocity.x, _cntx.jumpForce);

            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {
            Jumping(_cntx.jumpDirection);
        }
        public override void CheckSwitchState()
        {
            if ((_cntx.jumpInputUp || _cntx.jumpTimeCounter < 0))// && Mathf.Abs(_cntx.playerRb.velocity.y)<0.2f)// || _cntx.playerRb.velocity.y < 0)  
            {
                SwitchState(_factory.Fall());
            }
        }
        public override void InitiliseSubState()
        {

        }

        public void Jumping(Vector2 JumpDirection)
        {
            _cntx.playerRb.velocity = JumpDirection;
        }

    }
}
