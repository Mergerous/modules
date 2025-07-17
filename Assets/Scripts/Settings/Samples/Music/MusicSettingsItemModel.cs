using JetBrains.Annotations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class MusicSettingsItemModel : ISettingsItemModel, IToggleContent, ITitleContent
    {
        public MusicSettingsData Data { get; }
        public MusicSettingsContent Config { get; }

        public string Text => Config.text;
        public bool IsEnabled => Data?.isEnabled ?? Config.isEnabled;
        
        public MusicSettingsItemModel(MusicSettingsContent config, MusicSettingsData data)
        {
            Data = data;
            Config = config;
        }
    }
}