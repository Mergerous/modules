using System;

namespace Settings
{
    [Serializable]
    public sealed class MusicSettingsData : ISettingsData
    {
        public bool isEnabled;
    }
}