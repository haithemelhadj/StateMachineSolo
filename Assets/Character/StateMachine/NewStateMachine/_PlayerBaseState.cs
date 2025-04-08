namespace StateMachine
{
    public abstract class _PlayerBaseState
    {
        protected bool _isRootState = false;
        protected _PlayerStateMachine _cntx;
        protected _PlayerStateFactory _factory;
        protected _PlayerBaseState _currentState;
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

        public void UpdateStates()
        {
            UpdateState();
            /*
            if (_currentState != null)
            {
                _currentState.UpdateStates();
                Debug.Log("doing updates***");
            }
            else
            {
                Debug.Log("_cuurentSubState is null");
            }
            /**/
        }
        public void ExitStates()
        {
            ExitState();
            /*
            if (_currentState != null)
            {
                _currentState.ExitStates();
            }
            /**/
        }

        protected void SwitchState(_PlayerBaseState newState)
        {
            //Debug.Log("switch states: " + newState.ToString());
            //exit current state
            ExitState();
            //ExitStates();
            //enter new state
            newState.EnterState();
            _cntx._currentState = newState;
            /*
            if (newState._isRootState)
            {
                _cntx.currentSuperState = newState.ToString();
                Debug.Log("doing root state logic");
            }
            /*
            else if (_cuurentSuperState != null)
            {
                _cuurentSuperState.SetSubState(newState);
                _cntx.currentSubState = newState.ToString();
                Debug.Log("doing sub state logic");

            }
            else
            {
                Debug.Log("other state");
            }
            /**/
        }
        /*
        protected void SetSuperState(_PlayerBaseState newSuperState)
        {
            _cuurentSuperState = newSuperState;
        }
        /**/
        protected void SetState(_PlayerBaseState newState)
        {
            _currentState = newState;
            //newSubState.SetSuperState(this);
        }

    }
}
