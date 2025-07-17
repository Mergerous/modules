using Settings;

namespace Modules.Settings
{
    public sealed class VibrationsSettingsModel : ISettingsItemModel, IToggleContent, ITitleContent
    {
        public VibrationsSettingsData Data { get; }
        public VibrationsSettingsContent Config { get; }
        
        public string Text => Config.text;
        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public VibrationsSettingsModel(VibrationsSettingsContent config, VibrationsSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}