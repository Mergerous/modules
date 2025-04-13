using System;

namespace Modules.Views
{
    [Serializable]
    public abstract class StateComponent
    {
        public abstract void Apply();
    }
}