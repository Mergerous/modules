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

        public IEnumerable<ISettingsItemModel> ItemModels 
            => RemoteInfo.contents.Select(CreateModel);

        private ISettingsItemModel CreateModel(ISettingsContent content)
        {
            switch (content)
            {
                case MusicSettingsContent musicSettingsContent:
                    MusicSettingsData musicData = Data.data.OfType<MusicSettingsData>().FirstOrDefault();
                    return new MusicSettingsItemModel(musicSettingsContent, musicData);
                case SoundsSettingsContent soundsSettingsContent:
                    SoundsSettingsData soundsData = Data.data.OfType<SoundsSettingsData>().FirstOrDefault();
                    return new SoundsSettingsModel(soundsSettingsContent, soundsData);
                case PrivacyPolicySettingsContent privacyPolicySettingsContent:
                    return new PrivacyPolicySettingsModel(privacyPolicySettingsContent);
                case VibrationsSettingsContent vibrationsSettingsContent:
                    VibrationsSettingsData vibrationsData = Data.data.OfType<VibrationsSettingsData>().FirstOrDefault();
                    return new VibrationsSettingsModel(vibrationsSettingsContent, vibrationsData);
                default:
                    return default;
            }
        }
    }
}
