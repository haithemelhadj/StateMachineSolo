using UnityEngine;

[CreateAssetMenu(fileName = "Ground Attack State", menuName = "States List/Player/Ground Attack")]
public class GroundAttackState : MeleBaseState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.ExitAttack));
        }
        if (!currentContext.isGrounded && currentContext.Rb.velocity.y < 0.1f)
        {
            SwitchState(factory.GetState(_States.Fall));
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //attack
        currentContext.attackHitBox.SetActive(true);

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
