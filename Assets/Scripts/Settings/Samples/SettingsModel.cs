using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsModel
    {
        public SettingsData Data { get; set; }
        public SettingsRemoteInfo RemoteInfo { get; set; }

        private ISettingsItemModel[] itemModels;

        public SettingsModel(SettingsData data, SettingsRemoteInfo remoteInfo)
        {
            Data = data;
            RemoteInfo = remoteInfo;
        }

        public IEnumerable<ISettingsItemModel> ItemModels => itemModels ??= RemoteInfo.contents.Select(CreateModel).ToArray();

        private ISettingsItemModel CreateModel(ISettingsContent content)
        {
            switch (content)
            {
                case MusicSettingsContent musicSettingsContent:
                    MusicSettingsData musicData = Data.data.OfType<MusicSettingsData>().FirstOrDefault();
                    if (musicData == null)
                    {
                        Data.data.Add(musicData = new MusicSettingsData());
                    }
                    return new MusicSettingsItemModel(musicSettingsContent, musicData);
                case SoundsSettingsContent soundsSettingsContent:
                    SoundsSettingsData soundsData = Data.data.OfType<SoundsSettingsData>().FirstOrDefault();
                    if (soundsData == null)
                    {
                        Data.data.Add(soundsData = new SoundsSettingsData());
                    }
                    return new SoundsSettingsModel(soundsSettingsContent, soundsData);
                case PrivacyPolicySettingsContent privacyPolicySettingsContent:
                    return new PrivacyPolicySettingsModel(privacyPolicySettingsContent);
                case VibrationsSettingsContent vibrationsSettingsContent:
                    VibrationsSettingsData vibrationsData = Data.data.OfType<VibrationsSettingsData>().FirstOrDefault();
                    if (vibrationsData == null)
                    {
                        Data.data.Add(vibrationsData = new VibrationsSettingsData());
                    }
                    return new VibrationsSettingsModel(vibrationsSettingsContent, vibrationsData);
                default:
                    return default;
            }
        }
    }
}
