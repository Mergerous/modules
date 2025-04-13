using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class State
    {
        [field: SerializeField] public bool IsDefault { get; private set; }
        [field: SerializeReference] public Enum Key { get; private set; }
        
        [SerializeReference] private StateComponent[] components;

        public IEnumerable<StateComponent> Components => components;
    }
}