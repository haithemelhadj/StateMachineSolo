using UnityEngine;
public class AiPatrolState : AiMovementState
{
    public AiPatrolState(AiStateMachine currentContext, AiStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
        //_isRootState = true;
    }
    public override void EnterState()
    {
        base.EnterState();
        ChangeColor(color: Color.blue);
        _cntx.randPatrolTime = Random.Range(1f, 5f);
        _cntx.patrolEnterTime = Time.time;
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Move();
        CheckSwitchState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - _cntx.randPatrolTime >= _cntx.patrolEnterTime)
        {
            SwitchState(_factory.Idle());
        }
    }



}