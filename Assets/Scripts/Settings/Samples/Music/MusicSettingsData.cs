using System;
using Settings;

namespace Modules.Settings
{
    [Serializable]
    public sealed class MusicSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}