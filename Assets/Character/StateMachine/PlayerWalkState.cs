using UnityEngine;

namespace StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { InitiliseSubState(); }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            Move();
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
            if (_cntx.isGrounded)
            {
                SetSubState(_factory.Grounded());
            }
            else if ((_cntx.jumpInputUp || _cntx.jumpTimeCounter < 0))
            {
                SetSubState(_factory.Fall());
            }
            else
            {
                SetSubState(_factory.Jump());
            }
        }

        public void Move()
        {
            //move player
            if (_cntx.horizontalInput != 0f)
            {
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(_cntx.horizontalInput * _cntx.c_MaxHSpeed, _cntx.playerRb.velocity.y, 0f), _cntx.c_Acceleration);                
                Flip();
            }
            else //slow player to stop
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(0f, _cntx.playerRb.velocity.y, 0f), _cntx.c_Deceleration);            
        }

        public void Flip()
        {
            Vector3 currentScale = _cntx.transform.localScale;
            currentScale.x = Mathf.Sign(_cntx.horizontalInput) * Mathf.Abs(_cntx.transform.localScale.x);
            _cntx.transform.localScale = currentScale;
        }
    }
}
