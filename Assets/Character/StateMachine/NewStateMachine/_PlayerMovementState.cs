using UnityEngine;

namespace StateMachine
{
    public class _PlayerMovementState : _PlayerBaseState
    {
        public _PlayerMovementState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            //inputs
            _cntx.GetMovementInputs();
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
            _cntx.playerAnimator.SetFloat("Hvelocity", _cntx.playerRb.velocity.x);
            _cntx.playerAnimator.SetFloat("Yvelocity", _cntx.playerRb.velocity.y);

            if (_cntx.dashInputDown && _cntx.canDashCheck())
            {
                SwitchState(_factory.Dash());
            }
        }

        #region Movement
        public void Move()
        {

            //move player
            if (_cntx.horizontalInput != 0f)
            {
                _cntx.playerRb.velocity = Vector3.MoveTowards(_cntx.playerRb.velocity, new Vector3(_cntx.horizontalInput * _cntx.c_MaxHSpeed, _cntx.playerRb.velocity.y, 0f), _cntx.c_Acceleration);
                //Debug.Log("velocity: "+_cntx.playerRb.velocity.x);

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
