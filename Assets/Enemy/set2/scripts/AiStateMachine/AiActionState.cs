namespace StateMachine
{
    public class AiActionState : AiBaseState
    {
        public AiActionState(AiStateMachine currentContext, AiStateFactory StateFactory)
            : base(currentContext, StateFactory)
        {
            //_isRootState = true;
        }
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {

            CheckSwitchState();
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


