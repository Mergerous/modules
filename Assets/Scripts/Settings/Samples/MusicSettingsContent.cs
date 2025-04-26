using System;

namespace Settings
{
    [Serializable]
    public sealed class MusicSettingsContent : ISettingsContent
    {
        public bool isEnabled;
    }
}