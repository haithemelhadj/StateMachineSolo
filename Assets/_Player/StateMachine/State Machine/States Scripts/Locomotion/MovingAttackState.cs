using UnityEngine;

[CreateAssetMenu(fileName = "Move Ground Attack State", menuName = "States List/Player/Move Ground Attack")]
public class MovingAttackState : GroundedState
{
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - enterTime >= duration)
        {
            SwitchState(factory.GetState(_States.Grounded));
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
        //currentContext.attackHitBox.SetActive(true);
        currentContext.canFlip = false;

    }

    public override void OnExit()
    {
        base.OnExit();
        currentContext.attackHitBox.SetActive(false);
        currentContext.canFlip = true;

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
