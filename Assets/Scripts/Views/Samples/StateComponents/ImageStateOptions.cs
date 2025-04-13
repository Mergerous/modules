using System;

namespace Modules.Views
{
    [Flags]
    public enum ImageStateOptions
    {
        None   = 0,
        Sprite = 1 << 0,
    }
}