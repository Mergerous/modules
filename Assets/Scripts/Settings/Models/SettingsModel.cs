using Common.Constants;
using JetBrains.Annotations;
using Modules.Data;
using Modules.Remote;
using Settings.Configs;
using Settings.Core;

namespace Settings.Models
{
    [UsedImplicitly]
    [Data(SaveKeys.SETTINGS_DATA_SAVE_KEY, typeof(SettingsData))]
    [GetFromRemote(RemoteKeys.SETTINGS_REMOTE_INFO_KEY, typeof(SettingsRemoteInfo))]
    public sealed class SettingsModel : IDataHandler, IRemoteHandler, 
        ISoundsContent, IMusicContent, IPrivacyPolicyContent
    {
        private DataHandle DataHandle { get; set; }
        private SettingsData Data => DataHandle.GetData(remoteInfo.defaultSettingsData.Clone() as SettingsData);
        private SettingsRemoteInfo remoteInfo;

        public string PrivacyPolicyUrl => remoteInfo.privacyPolicyUrl;
        public bool IsSoundsEnabled => Data.isSoundsEnabled;
        public bool IsMusicEnabled => Data.isMusicEnabled;

        public void EnableMusic(bool isEnabled, bool shouldSave = true)
        {
            Data.isMusicEnabled = isEnabled;
            if (shouldSave)
            {
                DataHandle.Save();
            }
        }
        
        public void EnableSounds(bool isEnabled, bool shouldSave)
        {
            Data.isSoundsEnabled = isEnabled;
            if (shouldSave)
            {
                DataHandle.Save();
            }
        }

        public void OnLoaded(DataHandle handle) => DataHandle = handle;
        public void OnFetched(RemoteInfoHandle handle) => remoteInfo = handle.GetRemoteInfo<SettingsRemoteInfo>();
    }
}
