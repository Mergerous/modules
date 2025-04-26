using System;

namespace Settings
{
    [Serializable]
    public sealed class VibrationsSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}