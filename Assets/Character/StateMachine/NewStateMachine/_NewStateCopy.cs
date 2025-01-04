namespace StateMachine
{
    public class _NewStateCopy : _PlayerMovementState
    {
        public _NewStateCopy(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { }
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

        public override void InitiliseSubState()
        {

        }
    }
}

