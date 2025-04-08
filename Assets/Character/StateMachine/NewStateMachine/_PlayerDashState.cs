using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class _PlayerDashState : _PlayerActionState
    {
        public _PlayerDashState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            base.EnterState();
            _cntx.isDashing = true;
            DashInput();
        }
        public override void UpdateState()
        {

            base.UpdateState();
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {
            base.ExitState();
        }
        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            if (!_cntx.isDashing)
            {
                SwitchState(_factory.Fall());
            }
            if (_cntx.isHuggingWall)
            {
                SwitchState(_factory.WallSlide());

            }
        }

        #region Dash

        //public float drag;
        public void DashInput()
        {
            //start dash coroutine
            _cntx.StartCoroutine(Dash());
        }



        public IEnumerator Dash()
        {
            //set vars
            //_cntx.canDash = false;
            _cntx.isDashing = true;
            _cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
            //save gravity
            float originalGravity = _cntx.playerRb.gravityScale;
            _cntx.playerRb.gravityScale = 0f;
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
            //stop jumping
            _cntx.isJumping = false;
            //set jumping animation to stop
            _cntx.playerAnimator.SetBool("isJumping", _cntx.isJumping);
            //null velocity           
            _cntx.playerRb.velocity = Vector2.zero;
            //set dash direction if is wall sliding

            if (_cntx.isHuggingWall)
            {
                _cntx.transform.localScale = new Vector2(-_cntx.transform.localScale.x, _cntx.transform.localScale.y);
            }
            /**/
            //dash 
            _cntx.playerRb.velocity = new Vector2(Mathf.Sign(_cntx.transform.localScale.x) * _cntx.dashForce, 0f);
            //Debug.Log(_cntx.transform.localScale.x);
            yield return new WaitForSeconds(_cntx.dashTime);
            //reset everything
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.None);
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePosition);
            //_cntx.playerRb.drag = 0f;
            _cntx.playerRb.gravityScale = originalGravity;
            _cntx.playerRb.velocity = Vector2.zero;
            _cntx.isDashing = false;
            _cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
            yield return new WaitForSeconds(_cntx.dashTime);

        }
        #endregion
    }
}
