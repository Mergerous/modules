using System;

namespace Modules.UI.Views
{
    [Serializable]
    public abstract class StateComponent
    {
        public abstract void Apply();
    }
}