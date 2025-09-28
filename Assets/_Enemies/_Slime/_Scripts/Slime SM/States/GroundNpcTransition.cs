using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ground Npc Context Transition", menuName = "States List/Ground Npc /Ground Npc Context Transition")]
public class GroundNpcTransition : GroundNpcState
{
    public _States transitionToState;
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
        Vector2 newVelocity = new Vector3(0, currentContext.Rb.velocity.y, 0);
        currentContext.Rb.velocity = newVelocity;
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
