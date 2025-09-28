using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ground Npc States", menuName = "States Config/Ground Npc States")]

public class GroundNpcStates : ScriptableObject
{
    [System.Serializable]
    public class StateEntry
    {
        public _States state;
        public GroundNpcState stateClass;

    }

    public List<StateEntry> states = new List<StateEntry>();
}
