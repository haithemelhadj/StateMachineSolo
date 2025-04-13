using UnityEngine;

namespace StateMachine
{
    public class AiAttackState : AiActionState
    {
        public AiAttackState(AiStateMachine currentContext, AiStateFactory StateFactory) : base(currentContext, StateFactory) { }

        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("attack started");
            SwitchState(_factory.Chase());
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
}

