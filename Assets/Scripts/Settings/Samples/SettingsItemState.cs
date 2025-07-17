using System;

namespace Modules.Settings
{
    [Flags]
    public enum SettingsItemState
    {
        None = 0,
        Toggle = 1 << 0,
        Button = 1 << 1,
        Title  = 1 << 2
    }
}