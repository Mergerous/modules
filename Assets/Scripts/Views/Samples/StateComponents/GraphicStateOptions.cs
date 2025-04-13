using System;

namespace Modules.Views
{
    [Flags]
    public enum GraphicStateOptions
    {
        None  = 0,
        Color = 1 << 0,
    }
}