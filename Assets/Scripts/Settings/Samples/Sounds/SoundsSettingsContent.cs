using System;

namespace Settings
{
    [Serializable]
    public sealed class SoundsSettingsContent : ISettingsContent
    {
        public string text;
        public bool isEnabled;
    }
}