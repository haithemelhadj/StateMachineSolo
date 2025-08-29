using StateMachine;

public class ToCopy : _PlayerBaseState
{
    public ToCopy(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        //base.EnterState();
    }
    public override void UpdateState()
    {
        //base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        //base.FixedUpdateState();
    }
    public override void LateUpdateState()
    {
        //base.LateUpdateState();
    }
    public override void ExitState()
    {
        //base.ExitState();
    }
    public override void CheckSwitchState()
    {
        //base.CheckSwitchState();
    }

}
