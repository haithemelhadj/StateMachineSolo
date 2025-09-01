using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "States List", menuName = "States List/States")]

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
