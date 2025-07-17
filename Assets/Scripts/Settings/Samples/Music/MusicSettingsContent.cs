using System;

namespace Settings
{
    [Serializable]
    public sealed class MusicSettingsContent : ISettingsContent
    {
        public string text;
        public bool isEnabled;
    }
}