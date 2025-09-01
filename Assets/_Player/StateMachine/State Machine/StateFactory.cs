using System.Collections.Generic;
//using SM;

public class StateFactory
{
    StateMachine stateMachine;
    Dictionary<_States, State> _states = new Dictionary<_States, State>();

    public StateFactory(StateMachine currentContext, StatesList config)
    {
        stateMachine = currentContext;
        foreach (var entry in config.states)
        {
            if (!_states.ContainsKey(entry.state))
            {
                _states[entry.state] = entry.stateClass;
                _states[entry.state].Initialize(stateMachine, this,stateMachine.currentContext);
            }
        }
    }

    public State GetState(_States state)
    {
        return _states[state];
    }
}
