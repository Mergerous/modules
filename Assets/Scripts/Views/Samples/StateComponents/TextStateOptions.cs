using System;

namespace Modules.Views
{
    [Flags]
    public enum TextStateOptions
    {
        None  = 0,
        Text = 1 << 0,
    }
}