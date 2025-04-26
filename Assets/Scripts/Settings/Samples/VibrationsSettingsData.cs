using System;

namespace Modules.Settings
{
    [Serializable]
    public sealed class VibrationsSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}