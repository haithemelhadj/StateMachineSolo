using UnityEngine;

namespace StateMachine
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine _cntx;
        protected PlayerStateFactory _factory;
        protected PlayerBaseState _cuurentSuperState;
        protected PlayerBaseState _cuurentSubState;
        public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        {
            _cntx = currentContext;
            _factory = playerStateFactory;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchState();
        public abstract void InitiliseSubState();

        public void UpdateStates()
        {
            
        }
        protected void SwitchState(PlayerBaseState newState)
        {
            //exit current state
            ExitState();
            //enter new state
            newState.EnterState();
            _cntx._currentState = newState;

            Debug.Log("switching state to:" + newState);
        }
        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            _cuurentSuperState = newSuperState;
            Debug.Log("switching super state to:" + newSuperState);
        }
        protected void SetSubState(PlayerBaseState newSubState)
        {
            _cuurentSubState = newSubState;
            newSubState.SetSuperState(this);
            Debug.Log("switching Sub state to:" + newSubState);
        }

    }
}
