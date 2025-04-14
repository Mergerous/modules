using System;
using Modules.Remote;
using Settings.Core;

namespace Settings.Configs
{
    [Serializable, AddToRemote(RemoteKeys.SETTINGS_REMOTE_INFO_KEY)]
    public sealed class SettingsRemoteInfo
    {
        public SettingsData defaultSettingsData;
        public string privacyPolicyUrl;
    }
}
