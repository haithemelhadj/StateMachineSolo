using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash State", menuName = "Player/States/Dash")]
public class _PlayerDashState : _PlayerActionState
{
    public _PlayerDashState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        base.EnterState();
        _cntx.isDashing = true;
        _cntx.dashReset = false;
        _cntx.StartCoroutine(Dash());
    }
    public override void UpdateState()
    {

        base.UpdateState();
        if (!_cntx.isDashing)
            CheckSwitchState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
        _cntx.lastDashFinishTime = Time.time;
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();

        if (_cntx.isHuggingWall)
        {
            SwitchState(_factory.GetState(_States.WallSlide));
            //Debug.Log("Switching to WallSlide from Dash");
        }
        else if (!_cntx.isDashing)
        {
            SwitchState(_factory.GetState(_States.Fall));
            //Debug.Log("Switching to Fall from Dash");
        }
    }

    #region Dash

    public IEnumerator Dashed()
    {
        //set vars
        //_cntx.canDash = false;
        _cntx.isDashing = true;
        _cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
        //save gravity
        float originalGravity = _cntx.playerRb.gravityScale;
        _cntx.playerRb.gravityScale = 0f;
        _cntx.playerRb.constraints = (RigidbodyConstraints2D.FreezePositionY);
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
        _cntx.playerRb.constraints = (RigidbodyConstraints2D.None);
        _cntx.playerRb.constraints = (RigidbodyConstraints2D.FreezePosition);
        //_cntx.playerRb.drag = 0f;
        _cntx.playerRb.gravityScale = originalGravity;
        _cntx.playerRb.velocity = Vector2.zero; //stop immediately, but it’s a hard stop. Some players may want momentum to continue slightly.
        _cntx.isDashing = false;
        _cntx.playerAnimator.SetBool("Dashing", _cntx.isDashing);
        yield return new WaitForSeconds(_cntx.dashTime);

    }
    public IEnumerator Dash()
    {
        //set dash vars
        _cntx.isDashing = true;
        _cntx.playerAnimator.SetBool("Dashing", true);

        //save gravity
        float originalGravity = _cntx.playerRb.gravityScale;
        _cntx.playerRb.gravityScale = 0f;

        //stop jumping and set jumping animation to stop
        //_cntx.playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
        _cntx.isJumping = false;
        _cntx.playerAnimator.SetBool("isJumping", false);

        //null velocity
        _cntx.playerRb.velocity = Vector2.zero;

        //set dash direction if is wall sliding
        if (_cntx.isHuggingWall)
            _cntx.transform.localScale = new Vector2(-_cntx.transform.localScale.x, _cntx.transform.localScale.y);

        // Apply dash velocity
        _cntx.playerRb.velocity = new Vector2(Mathf.Sign(_cntx.transform.localScale.x) * _cntx.dashForce, 0f);

        // Wait for dash duration (FPS-independent)
        yield return new WaitForSeconds(_cntx.dashTime);

        // Reset physics and gravity
        //_cntx.playerRb.constraints = RigidbodyConstraints2D.None;
        //_cntx.playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _cntx.playerRb.gravityScale = originalGravity;
        _cntx.playerRb.velocity = Vector2.zero;

        _cntx.isDashing = false;
        _cntx.playerAnimator.SetBool("Dashing", false);
    }

    #endregion
}

