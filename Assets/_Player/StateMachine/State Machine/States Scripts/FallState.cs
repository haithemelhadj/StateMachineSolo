using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fall State", menuName = "States List/Fall")]
public class FallState : LocomotionState
{

    #region  walk Movement 
    [Header("Walk Movement")]
    public float fallMaxSpeed;
    public float fallAcceleration;
    public float fallDeceleration;
    #endregion
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.isGrounded)
        {
            SwitchState(factory.GetState(_States.Grounded));
            //currentContext.playerAnimator.SetBool("isJumping", false);
        }
        if (currentContext.canCyoteJump && currentContext.jumpInput)
        {
            SwitchState(factory.GetState(_States.Jump));
        }

        if (!currentContext.isGrounded && currentContext.isHuggingWall)
        {
            SwitchState(factory.GetState(_States.WallSlide));
            //currentContext.playerAnimator.SetBool("isJumping", false);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetMoveSpeed();
        currentContext.canCyoteJump = true;
        if (currentContext.fasterFallMultiplier == 0f) currentContext.fasterFallMultiplier = 1f;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Fall();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (currentContext.jumpInput)
        {
            currentContext.jumpPressTime = Time.time;
            currentContext.willBufferJump = true;
        }
        CyoteTime();
    }

    public void Fall()
    {
        if (currentContext.Rb.velocity.y <= 0f)
        {
            //currentContext.playerAnimator.SetBool("isJumping", false);
            //make fall speed faster
            currentContext.Rb.velocity += Vector2.up * Physics2D.gravity.y * currentContext.fasterFallMultiplier * Time.deltaTime;
            //limit fall speed 
            if (currentContext.Rb.velocity.y < currentContext.maxFallSpeed)
            {
                currentContext.Rb.velocity = new Vector2(currentContext.Rb.velocity.x, currentContext.maxFallSpeed);
            }

        }
        else//JumpApexControll
        {
            //Debug.Log("jump Apex");
            //currentContext.playerAnimator.SetBool("isJumping", true);
            //currentContext.playerRb.velocity= 
            //Vector2.Lerp(currentContext.playerRb.velocity, new Vector2(currentContext.playerRb.velocity.x, 0f,0.2f);

        }

    }

    public void CyoteTime()
    {
        if (Time.time - currentContext.LastGrounded > currentContext.cyoteTime)// || Time.time - currentContext.LastTimeWalled > currentContext.cyoteTime)
        {
            currentContext.canCyoteJump = false;
        }
    }

    #region change Speed Input

    public override void SetMoveSpeed()
    {

        currentContext.currentMaxMoveSpeed = fallMaxSpeed;
        currentContext.currentAcceleration = fallAcceleration;
        currentContext.currentDeceleration = fallDeceleration;
    }
    #endregion
}
