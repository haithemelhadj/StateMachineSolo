using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateConfig", menuName = "Player/State Config")]
public class _PlayerStateConfig : ScriptableObject
{
    [System.Serializable]
    public class StateEntry
    {
        public _States state;
        public _PlayerBaseState stateClass;

    }

    public List<StateEntry> states = new List<StateEntry>();
}