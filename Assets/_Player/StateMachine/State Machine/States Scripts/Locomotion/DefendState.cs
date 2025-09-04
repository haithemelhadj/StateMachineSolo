using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defend State", menuName = "States List/Defend")]
public class DefendState : GroundedState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (!currentContext.defendInput)
        {
            SwitchState(factory.GetState(_States.Grounded));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        currentContext.defendHitBox.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.defendHitBox.SetActive(false);
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
