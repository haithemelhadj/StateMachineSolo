using StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Defend State", menuName = "Player/States/Defend")]
public class _PlayerDefendState : _PlayerActionState // _PlayerMovementState
{
    public _PlayerDefendState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
    }
}

