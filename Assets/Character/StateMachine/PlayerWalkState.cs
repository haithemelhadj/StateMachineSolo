using UnityEngine;

namespace StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {
            //Move();

        }
        public override void ExitState()
        {

        }
        public override void CheckSwitchState()
        {

        }
        public override void InitiliseSubState()
        {

        }

        public void Move()
        {
            //move player
            if (_cntx.horizontalInput != 0f)
            {
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(_cntx.horizontalInput * _cntx.c_MaxHSpeed, _cntx.playerRb.velocity.y, 0f), _cntx.c_Acceleration);                
            }
            else //slow player to stop
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(0f, _cntx.playerRb.velocity.y, 0f), _cntx.c_Deceleration);

            //flip character and keep it that way when no inputs        
            if (_cntx.horizontalInput != 0)
            {
                Flip();
            }

        }

        public void Flip()
        {
            Vector3 currentScale = _cntx.transform.localScale;
            currentScale.x = Mathf.Sign(_cntx.horizontalInput) * Mathf.Abs(_cntx.transform.localScale.x);
            _cntx.transform.localScale = currentScale;
        }
    }
}
