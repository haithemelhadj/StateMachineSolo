using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player States", menuName = "States Config/Player States")]

public class StatesList : ScriptableObject
{
    [System.Serializable]
    public class StateEntry
    {
        public _States state;
        public State stateClass;

    }

    public List<StateEntry> states = new List<StateEntry>();
}
