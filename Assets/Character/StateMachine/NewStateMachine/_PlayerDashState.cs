using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StateMachine
{
    public class _PlayerDashState : _PlayerBaseState
    {
        public _PlayerDashState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
        public override void EnterState()
        {
            Debug.Log("Enter dash");
            _cntx.isDashing = true;
            Debug.Log("Dashing start");
            DashInput();
        }
        public override void UpdateState()
        {
            CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {
            Debug.Log("Exit dash");
        }
        public override void CheckSwitchState()
        {
            if (!_cntx.isDashing)
            {
                SwitchState(_factory.Movement());
            }
        }

        public override void InitiliseSubState()
        {

        }
        #region Dash

        //public float drag;
        public void DashInput()
        {
            Debug.Log("dash input method");
                _cntx.StartCoroutine(Dash());
            if (_cntx.canDash)
            {
            }
            //stop dasing when hitting a wall ( when enabled the player cannot dash from wall)
            //if (isDashing && wallSlideScript.isWallSliding)
            //{
            //    StopCoroutine(Dash());
            //    isDashing = false;
            //    inputsScript.playerAnimator.SetBool("Dashing", isDashing);
            //}

            if (!_cntx.isDashing && (_cntx.isGrounded || _cntx.isWallSliding))
            {
                _cntx.canDash = true;
            }
        }

        
        
        public IEnumerator Dash()
        {
            Debug.Log("starting dash");
            //set vars
            _cntx.canDash = false;
            _cntx.isDashing = true;
            //_cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
            //save gravity
            float originalGravity = _cntx.playerRb.gravityScale;
            _cntx.playerRb.gravityScale = 0f;
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
            //set air friction 
            //float originalDrag = inputsScript.playerRb.drag;
            //inputsScript.playerRb.drag = drag;
            //stop jumping
            _cntx.isJumping = false;
            //set jumping animation to stop
            //_cntx.playerAnimator.SetBool("isJumping", _cntx.isJumping);
            //null velocity
            _cntx.playerRb.velocity = Vector2.zero;
            //set dash direction if is wall sliding
            if (_cntx.isWallSliding)
            {
                _cntx.transform.localScale = new Vector2(-_cntx.transform.localScale.x, _cntx.transform.localScale.y);
            }
            //dash 
            _cntx.playerRb.velocity = new Vector2(Mathf.Sign(_cntx.transform.localScale.x) * _cntx.dashForce, 0f);
            yield return new WaitForSeconds(_cntx.dashTime);
            //reset everything
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.None);
            _cntx.playerRb.constraints.Equals(RigidbodyConstraints2D.FreezePosition);
            _cntx.playerRb.drag = 0f;
            _cntx.playerRb.gravityScale = originalGravity;
            //inputsScript.playerRb.drag = originalDrag;
            _cntx.isDashing = false;
            //_cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
            yield return new WaitForSeconds(_cntx.dashTime);
            Debug.Log("starting dash");

        }
        #endregion
    }
}
