namespace StateMachine
{
    public class AiTemplateState : AiBaseState
    {
        public AiTemplateState(AiStateMachine currentContext, AiStateFactory StateFactory)
            : base(currentContext, StateFactory)
        {
            //_isRootState = true;
        }
        public override void EnterState()
        {
            //base.EnterState();
        }
        public override void UpdateState()
        {
            CheckSwitchState();
            //base.UpdateState();
        }
        public override void FixedUpdateState()
        {
            //base.FixedUpdateState();
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
}

