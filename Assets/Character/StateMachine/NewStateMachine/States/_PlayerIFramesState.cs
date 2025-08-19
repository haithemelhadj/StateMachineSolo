

using UnityEngine;

namespace StateMachine
{
    public class _PlayerIFramesState : _PlayerBaseState
    {
        public _PlayerIFramesState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

        public override void EnterState()
        {
            Debug.Log("i frames enter");
            _cntx.isInIframes = true;
            _cntx.Invoke(nameof(_cntx.ExitIFrames), _cntx.iFramesDuration);
        }
        public override void UpdateState()
        {

        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {

        }
        public override void CheckSwitchState()
        {

        }
    }
}

