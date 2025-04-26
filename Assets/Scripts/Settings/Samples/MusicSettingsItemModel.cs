using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class MusicSettingsItemModel : ISettingsItemModel, IToggleContent
    {
        public MusicSettingsData Data { get; set; }
        public MusicSettingsContent Config { get; }

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public MusicSettingsItemModel(MusicSettingsContent config, MusicSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}