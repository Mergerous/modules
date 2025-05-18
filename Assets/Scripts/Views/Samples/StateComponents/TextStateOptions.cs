using System;

namespace Modules.Views
{
    [Flags]
    public enum TextStateOptions
    {
        None   = 0,
        Text   = 1 << 0,
        Style  = 1 << 1,
        OutlineColor = 1 << 2
    }
}