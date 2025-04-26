namespace Settings
{
    public sealed class VibrationsSettingsModel : ISettingsItemModel, IToggleContent
    {
        public VibrationsSettingsData Data { get; }
        public VibrationsSettingsContent Config { get; }

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public VibrationsSettingsModel(VibrationsSettingsContent config, VibrationsSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}