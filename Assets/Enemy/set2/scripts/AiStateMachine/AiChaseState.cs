using UnityEngine;

public class AiChaseState : AiMovementState
{
    public AiChaseState(AiStateMachine currentContext, AiStateFactory StateFactory)
        : base(currentContext, StateFactory)
    {
        //_isRootState = true;
    }
    public override void EnterState()
    {
        base.EnterState();
        ChangeColor(color: Color.red);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        MoveTowardsTargetPosition(_cntx.targetPlayer.position, _cntx.chaseSpeed);
        _cntx.animator.SetFloat("speed", 1);
        CheckSwitchState();
    }
    public override void ExitState()
    {
        base.ExitState();

    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if (!IsInFOV(_cntx.targetPlayer) && Vector2.Distance(_cntx.targetPlayer.position, _cntx.transform.position) > _cntx.detectionRange * 2)
        {
            _cntx.playerlastSeenPos = _cntx.targetPlayer.position;
            SwitchState(_factory.Search());
        }
    }




}