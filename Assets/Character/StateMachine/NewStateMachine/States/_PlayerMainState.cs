using StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Main State", menuName = "Player/States/Main")]
public class _PlayerMainState : _PlayerBaseState
{
    public _PlayerMainState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {

    }
    public override void UpdateState()
    {

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
