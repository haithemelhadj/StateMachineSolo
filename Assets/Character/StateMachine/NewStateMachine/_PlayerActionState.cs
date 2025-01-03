using UnityEngine;

namespace StateMachine
{
    public class _PlayerActionState : _PlayerBaseState
    {
        public _PlayerActionState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) 
        {
            _isRootState = true;
            //Debug.Log("action rt: " + _isRootState);
        }
        public override void EnterState()
        {
            //InitiliseSubState();
            //Debug.Log("Enter action");
        }
        public override void UpdateState()
        {
            //Debug.Log("Action");
            //CheckSwitchState();
        }
        public override void FixedUpdateState()
        {

        }
        public override void ExitState()
        {
            //Debug.Log("Exit action");

        }
        public override void CheckSwitchState()
        {
            
            //if (!_cntx.isDashing)
            //{
            //    SwitchState(_factory.Movement());
            //}
        }
        public override void InitiliseSubState()
        {
            //Debug.Log("action InitiliseSubState");
            if (_cntx.dashInputDown)
            {
                SetSubState(_factory.Dash());
                //Debug.Log("dash sub");
            }
        }
    }
}
