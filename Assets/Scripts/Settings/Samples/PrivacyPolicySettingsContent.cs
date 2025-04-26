using System;

namespace Settings
{
    [Serializable]
    public sealed class PrivacyPolicySettingsContent : ISettingsContent
    {
        public string url;
    }
}