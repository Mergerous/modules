using System;
using UnityEngine;

namespace Modules.UI.Views
{
    [Serializable]
    public abstract class Element : IDisposable
    {
        [field: SerializeField] public string Key { get; private set; }

        public virtual void Initialize()
        {
            
        }

        public virtual void Dispose()
        {
            
        }
    }
}
