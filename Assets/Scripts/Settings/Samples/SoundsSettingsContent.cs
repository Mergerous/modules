using System;

namespace Settings
{
    [Serializable]
    public sealed class SoundsSettingsContent : ISettingsContent
    {
        public bool isEnabled;
    }
}