using UnityEngine;

[CreateAssetMenu(fileName = "Ground Attack State", menuName = "States List/Ground Attack")]
public class GroundAttackState : MeleBaseState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.ExitAttack));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //attack
        currentContext.attackHitBox.SetActive(true);
        currentContext.animatorController.PlayAnimation(animationName);
        Debug.Log("started attack " + animationName);

    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.attackHitBox.SetActive(false);

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
