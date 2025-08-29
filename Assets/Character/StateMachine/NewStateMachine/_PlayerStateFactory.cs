using StateMachine;
using System;
using System.Collections.Generic;


public class _PlayerStateFactory
{
    _PlayerStateMachine context;
    Dictionary<_States, _PlayerBaseState> _states = new Dictionary<_States, _PlayerBaseState>();

    public _PlayerStateFactory(_PlayerStateMachine currentContext, _PlayerStateConfig config)
    {
        context = currentContext;
        foreach(var entry in config.states)
        {
            if (!_states.ContainsKey(entry.state))
            {
                _states[entry.state] = entry.stateClass;
                _states[entry.state].Initialize(context, this);
            }
        }
    }

    public _PlayerBaseState GetState(_States state)
    {
        return _states[state];
    }
}


