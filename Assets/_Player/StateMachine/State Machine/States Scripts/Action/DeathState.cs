using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Sentry.MeasurementUnit;

public class DeathState : ActionState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Time.time-enterTime > duration)
        {
            Debug.Log(this.name + "duration has ended");
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("dead");
        currentContext.animatorController.UpdateAnimatorBool("Dead", true);
    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.animatorController.UpdateAnimatorBool("Dead", false);
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
