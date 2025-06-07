using System;
using Modules.Architecture.Components;

namespace Units
{
    //TODO Add OverrideManager and apply there
    public interface IOverride
    {
        public void Apply(IComponent component);
    }
}
