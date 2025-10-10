using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetHit State", menuName = "States List/Player/GetHit")]
public class GetHitState : ActionState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime > duration)
        {
            SwitchState(factory.GetState(_States.Grounded));
        }
        if(currentContext.currentHealth<=0f)
        {
            SwitchState(factory.GetState(_States.Death));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log("hit");
        currentContext.currentHealth -= currentContext.dmgAmount;
    }

    public override void OnExit()
    {
        base.OnExit();
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
