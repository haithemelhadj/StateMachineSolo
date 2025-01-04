
using UnityEngine;

namespace StateMachine
{
    public abstract class _PlayerBaseState
    {
        protected bool _isRootState = false;
        protected _PlayerStateMachine _cntx;
        protected _PlayerStateFactory _factory;
        protected _PlayerBaseState _cuurentSuperState;
        protected _PlayerBaseState _cuurentSubState;
        public _PlayerBaseState(_PlayerStateMachine currentContext, _PlayerStateFactory playerStateFactory)
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
            UpdateState();
            if (_cuurentSubState != null)
            {
                _cuurentSubState.UpdateStates();
            }
            else
            {
                //Debug.Log("_cuurentSubState is null");
            }
        }
        public void ExitStates()
        {
            ExitState();
            if (_cuurentSubState != null)
            {
                _cuurentSubState.ExitStates();
            }
        }

        protected void SwitchState(_PlayerBaseState newState)
        {
            Debug.Log("switch states: " + newState.ToString());
            //exit current state
            ExitState();
            //ExitStates();
            //enter new state
            newState.EnterState();

            if (newState._isRootState)
            {
                _cntx._currentState = newState;
                _cntx.currentSuperState = newState.ToString();
            }
            else if (_cuurentSuperState != null)
            {
                _cuurentSuperState.SetSubState(newState);
                _cntx.currentSubState = newState.ToString();
            }
            else
            {
                Debug.Log("other state");
            }
            /**/
        }
        protected void SetSuperState(_PlayerBaseState newSuperState)
        {
            _cuurentSuperState = newSuperState;
        }
        protected void SetSubState(_PlayerBaseState newSubState)
        {
            _cuurentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

    }
}
