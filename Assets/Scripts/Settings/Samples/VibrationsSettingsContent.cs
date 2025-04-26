using System;

namespace Settings
{
    [Serializable]
    public sealed class VibrationsSettingsContent : ISettingsContent
    {
        public bool isEnabled;
    }
}