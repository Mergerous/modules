using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SoundsSettingsModel : ISettingsItemModel, IToggleContent, ITitleContent
    {
        public SoundsSettingsData Data { get; set; }
        public SoundsSettingsContent Config { get; }

        public string Text => Config.text;

        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public SoundsSettingsModel(SoundsSettingsContent config, SoundsSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}