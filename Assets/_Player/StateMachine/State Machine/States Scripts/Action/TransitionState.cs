using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Transition State", menuName = "States List/Transition State")]
public class TransitionState : ActionState
{
    _States transitionToState;
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(transitionToState));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //attack
        Vector2 newVelocity = new Vector3(0, currentContext.Rb.velocity.y, 0);
        currentContext.Rb.velocity = newVelocity;
        currentContext.animatorController.PlayAnimation(animationName);
        Debug.Log("started transition to " + animationName);
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
