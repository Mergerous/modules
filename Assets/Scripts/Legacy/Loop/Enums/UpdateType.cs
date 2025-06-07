using System;

namespace Modules.Loop
{
    [Flags]
    public enum UpdateType
    {
        None        = 0,
        Update      = 1 << 0,
        FixedUpdate = 1 << 1,
        LateUpdate  = 1 << 2
    }

    [Flags]
    public enum ExecutionType
    {
        None = 0,
        OnStart = 1 << 0,
        Manually = 1 << 1
    }
}