using Settings;

namespace Modules.Settings
{
    public sealed class VibrationsSettingsModel : ISettingsItemModel, IToggleContent
    {
        public VibrationsSettingsData Data { get; set; }
        public VibrationsSettingsContent Config { get; }

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public VibrationsSettingsModel(VibrationsSettingsContent config, VibrationsSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}