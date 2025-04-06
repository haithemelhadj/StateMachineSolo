namespace StateMachine
{
    public class _PlayerActionState : _PlayerBaseState
    {
        public _PlayerActionState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _isRootState = true;
        }
        public override void EnterState()
        {

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
        public override void InitiliseSubState()
        {
            if (_cntx.dashInputDown)
            {
                SetState(_factory.Dash());
            }
        }
    }
}
