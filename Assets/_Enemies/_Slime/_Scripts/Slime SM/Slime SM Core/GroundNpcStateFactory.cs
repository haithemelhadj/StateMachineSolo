using System.Collections.Generic;
//using SM;

public class GroundNpcStateFactory
{
    GroundNpcStateMachine stateMachine;
    Dictionary<_States, GroundNpcState> _states = new Dictionary<_States, GroundNpcState>();

    public GroundNpcStateFactory(GroundNpcStateMachine currentContext, GroundNpcStates config)
    {
        stateMachine = currentContext;
        foreach (var entry in config.states)
        {
            if (!_states.ContainsKey(entry.state))
            {
                _states[entry.state] = entry.stateClass;
                _states[entry.state].Initialize(stateMachine, this, stateMachine.currentContext);
            }
        }
    }

    public GroundNpcState GetState(_States state)
    {
        return _states[state];
    }
}
