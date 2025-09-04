using UnityEngine;

[CreateAssetMenu(fileName = "Exit Attack State", menuName = "States List/Exit Attack")]
public class ExitAttackState : MeleBaseState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.Grounded));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //attack
        //currentContext.animatorController.PlayAnimation(animationName);
        Debug.Log("started attack " + animationName);
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
