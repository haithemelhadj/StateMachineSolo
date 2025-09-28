using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallSlide State", menuName = "States List/Player/Wall Slide")]
public class WallSlideState : LocomotionState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (currentContext.isGrounded)
        {
            SwitchState(factory.GetState(_States.Grounded));
        }
        if (currentContext.jumpInputDown || currentContext.willBufferJump)
        {
            currentContext.wallJumpPressTime = Time.time;
            SwitchState(factory.GetState(_States.Jump));
        }
        if (!currentContext.isGrounded && !currentContext.isHuggingWall)
        {
            SwitchState(factory.GetState(_States.Fall));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        currentContext.dashReset = true;
        currentContext.canFlip = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.canFlip = true;

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        currentContext.Rb.velocity = new Vector2(currentContext.Rb.velocity.x, -currentContext.wallSlidingSpeed);
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
