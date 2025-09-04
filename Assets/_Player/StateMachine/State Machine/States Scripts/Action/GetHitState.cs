using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : ActionState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime > duration)
        {
            Debug.Log(this.name + "duration has ended");
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //currentContext.animatorController.PlayAnimation(animationName = "Get Hit");
    }

    public override void OnExit()
    {
        base.OnExit();
        //currentContext.animatorController.UpdateAnimatorBool("GetHit", false);
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
    }
}
