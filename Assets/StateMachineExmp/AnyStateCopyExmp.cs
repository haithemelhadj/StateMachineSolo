namespace StateMachine
{
    public class AnyStateCopyExmp : BaseStateExmp
    {
        public AnyStateCopyExmp(StateMachineExmp currentContext, StateFactoryExmp StateFactory)
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
            //base.UpdateState();
            CheckSwitchState();
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

        public override void InitiliseSubState()
        {

        }
    }
}

