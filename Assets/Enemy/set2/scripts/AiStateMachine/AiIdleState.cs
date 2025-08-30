using UnityEngine;

public class AiIdleState : AiMovementState
{
    public AiIdleState(AiStateMachine currentContext, AiStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
        //_isRootState = true;
    }
    public override void EnterState()
    {
        base.EnterState();
        ChangeColor(color: Color.white);
        _cntx.randIdleWaitTime = Random.Range(1f, 5f);
        _cntx.idleEnterTime = Time.time;
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _cntx.selfRb.velocity = Vector3.zero;
        _cntx.animator.SetFloat("speed", 0);
        CheckSwitchState();
    }
    public override void ExitState()
    {
        base.ExitState();
        if (RandomChance(50)) _cntx.Flip();
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - _cntx.randIdleWaitTime >= _cntx.idleEnterTime)
        {
            SwitchState(_factory.Patrol());
        }
    }

}