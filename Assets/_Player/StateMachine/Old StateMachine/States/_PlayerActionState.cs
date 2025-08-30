//[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/States/Base")]
public class _PlayerActionState : _PlayerBaseState
{
    public _PlayerActionState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        _cntx.currentActiveState = this.ToString();
    }
    public override void UpdateState()
    {
        //actions
        //_cntx.GetActionInputs();
    }
    public override void FixedUpdateState()
    {

    }
    public override void LateUpdateState()
    {

    }
    public override void ExitState()
    {

    }
    public override void CheckSwitchState()
    {

    }

}

