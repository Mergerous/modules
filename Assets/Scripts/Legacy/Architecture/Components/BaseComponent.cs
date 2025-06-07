using System;

namespace Modules.Architecture.Components
{
    [Serializable]
    public abstract class BaseComponent : IComponent
    {
        public virtual void OnGizmos()
        {
            
        }
    }
}