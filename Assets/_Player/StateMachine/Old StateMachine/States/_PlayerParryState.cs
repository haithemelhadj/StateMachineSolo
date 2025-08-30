using UnityEngine;

[CreateAssetMenu(fileName = "Parry State", menuName = "Player/States/Parry")]
public class _PlayerParryState : _PlayerActionState
{
    public _PlayerParryState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
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

