using JetBrains.Annotations;

namespace Settings
{
    [UsedImplicitly]
    public sealed class MusicSettingsItemModel : ISettingsItemModel, IToggleContent
    {
        public MusicSettingsData Data { get; }
        public MusicSettingsContent Config { get; }

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public MusicSettingsItemModel(MusicSettingsContent config, MusicSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}