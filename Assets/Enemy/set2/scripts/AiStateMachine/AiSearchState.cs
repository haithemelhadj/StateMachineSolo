using UnityEngine;

public class AiSearchState : AiMovementState
{
    public AiSearchState(AiStateMachine currentContext, AiStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
        //_isRootState = true;
    }
    public override void EnterState()
    {
        base.EnterState();
        ChangeColor(color: Color.yellow);
        _cntx.randSearchTime = Random.Range(1f, 5f);
        _cntx.searchEnterTime = Time.time;
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        MoveTowardsTargetPosition(_cntx.playerlastSeenPos, _cntx.searchSpeed);
        CheckSwitchState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (Time.time - _cntx.randSearchTime >= _cntx.searchEnterTime)
        {
            SwitchState(_factory.Patrol());
        }
    }

}