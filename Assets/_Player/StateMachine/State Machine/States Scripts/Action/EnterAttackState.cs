using UnityEngine;

[CreateAssetMenu(fileName = "Enter Attack State", menuName = "States List/Enter Attack")]
public class EnterAttackState : MeleBaseState
{

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.GoundAttack));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //attack
        currentContext.animatorController.UpdateAnimatortrrigger(attackName);
        Debug.Log("started attack " + attackName);

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
