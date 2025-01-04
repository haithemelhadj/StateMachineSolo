using UnityEngine;

namespace StateMachine
{
    public class _PlayerMovementState : _PlayerBaseState
    {
        public _PlayerMovementState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _isRootState = true;
        }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            Move();
            //LedgeBump();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {

        }

        public override void CheckSwitchState()
        {
            if (_cntx.dashInputDown)
            {
                SwitchState(_factory.Dash());
            }
        }
        public override void InitiliseSubState()
        {
            //Debug.Log("mvt InitiliseSubState");
            if (_cntx.isGrounded)
            {
                //Debug.Log("1 movement switch state ground");
                SetSubState(_factory.Grounded());
            }
            else if (_cntx.jumpInputDown || _cntx.playerRb.velocity.y > 0)
            {
                //Debug.Log("1 movement switch state jump");
                SetSubState(_factory.Jump());
            }
            else
            {
                //Debug.Log("1 movement switch state fall");
                SetSubState(_factory.Fall());
            }
            /**/

        }

        #region Movement
        public void Move()
        {
            //move player
            if (_cntx.horizontalInput != 0f)
            {
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(_cntx.horizontalInput * _cntx.c_MaxHSpeed, _cntx.playerRb.velocity.y, 0f), _cntx.c_Acceleration);
                //flip character and keep it that way when no inputs        
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
        #endregion

        #region Ledge Bump
        public void LedgeBump()
        {
            if (_cntx.WallDetectionLower() && !_cntx.WallDetectionMiddle() && _cntx.playerRb.velocity.y > 0)
            {
                _cntx.isLedgeBumping = true;
                //_cntx.playerRb.velocity = new Vector2(0f, _cntx.playerRb.velocity.y);
                _cntx.Invoke("CancleLedgeBumb", _cntx.bumpTime);
            }
        }
        private void CancleLedgeBumb()
        {
            _cntx.isLedgeBumping = false;
        }
        #endregion
    }
}
