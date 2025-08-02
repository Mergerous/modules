using System;

namespace Modules.States
{
    [Flags]
    public enum StateOptions
    {
        None       = 0,
        Close  = 1 << 0,
        Remove = 1 << 1,
        CloseAndRemove = Close | Remove
    }
}