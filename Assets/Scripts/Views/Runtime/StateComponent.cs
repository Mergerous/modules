using System;

namespace Modules.Views
{
    [Serializable]
    public abstract class StateComponent
    {
        public View View { private get; set; }
        public abstract void Apply();
    }
}