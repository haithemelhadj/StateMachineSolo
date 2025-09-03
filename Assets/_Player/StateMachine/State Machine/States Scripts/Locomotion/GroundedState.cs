using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grounded State", menuName = "States List/Grounded")]
public class GroundedState : LocomotionState
{
    #region  walk Movement 
    [Header("Walk Movement")]
    public float walkMaxSpeed;
    public float walkAcceleration;
    public float walkDeceleration;
    #endregion

    #region  run Movement 
    [Header("Run Movement")]
    public float runMaxSpeed;
    public float runAcceleration;
    public float runDeceleration;
    #endregion
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.jumpInputDown || currentContext.willBufferJump)
        {
            SwitchState(factory.GetState(_States.Jump));
        }
        if (!currentContext.isGrounded && currentContext.Rb.velocity.y < 0f)
        {
            currentContext.canCyoteJump = true;
            SwitchState(factory.GetState(_States.Fall));
        }
        if(currentContext.attackInputDown)
        {
            SwitchState(factory.GetState(_States.EnterAttack));

        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //animation
        
        //cyote time
        currentContext.canCyoteJump = false;
        currentContext.dashReset = true;
        //Buffer time
        if (Time.time - currentContext.jumpPressTime > currentContext.jumpBufferTime)
        {
            currentContext.willBufferJump = false;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.LastTimeGrounded = Time.time;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        SetMoveSpeed();
    }

    #region chnage Speed Input

    public override void SetMoveSpeed()
    {
        if (currentContext.walkSpeedInput)
        {
            currentContext.currentMaxMoveSpeed = walkMaxSpeed;
            currentContext.currentAcceleration = walkAcceleration;
            currentContext.currentDeceleration = walkDeceleration;
        }
        else
        {
            currentContext.currentMaxMoveSpeed = runMaxSpeed;
            currentContext.currentAcceleration = runAcceleration;
            currentContext.currentDeceleration = runDeceleration;
        }
    }
    #endregion
}
