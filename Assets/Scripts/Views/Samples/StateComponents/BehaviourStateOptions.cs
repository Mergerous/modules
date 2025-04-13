using System;

namespace Modules.Views
{
    [Flags]
    public enum BehaviourStateOptions
    {
        None    = 0,
        Enabled = 1 << 0
    }
}