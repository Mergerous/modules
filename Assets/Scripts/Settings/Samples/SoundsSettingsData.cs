using System;

namespace Settings
{
    [Serializable]
    public sealed class SoundsSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}