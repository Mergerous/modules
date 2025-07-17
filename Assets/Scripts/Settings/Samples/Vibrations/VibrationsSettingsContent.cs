using System;

namespace Settings
{
    [Serializable]
    public sealed class VibrationsSettingsContent : ISettingsContent
    {
        public string text;
        public bool isEnabled;
    }
}