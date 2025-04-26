using JetBrains.Annotations;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SoundsSettingsModel : ISettingsItemModel, IToggleContent
    {
        public SoundsSettingsData Data { get; }
        public SoundsSettingsContent Config { get; }

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public SoundsSettingsModel(SoundsSettingsContent config, SoundsSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}