using System;

namespace Modules.Settings
{
    [Serializable]
    public sealed class SoundsSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}