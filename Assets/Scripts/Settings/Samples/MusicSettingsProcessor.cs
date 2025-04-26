using JetBrains.Annotations;
using Modules.Audio;
using Modules.Data;
using Modules.Settings;

namespace Settings
{
    [UsedImplicitly]
    public sealed class MusicSettingsProcessor : IToggleSettingsProcessor
    {
        private readonly DataManager dataManager;
        private readonly AudioManager audioManager;
        private readonly SettingsModel settingModel;

        public MusicSettingsProcessor(DataManager dataManager, AudioManager audioManager, SettingsModel settingModel)
        {
            this.dataManager = dataManager;
            this.audioManager = audioManager;
            this.settingModel = settingModel;
        }

        public void Process(bool isOn, ISettingsItemModel model)
        {
            if (model is MusicSettingsItemModel musicSettingsItemModel)
            {
                audioManager.EnableMusic(isOn);
                musicSettingsItemModel.Data ??= new MusicSettingsData();
                musicSettingsItemModel.Data.isEnabled = isOn;
                dataManager.Save(SettingsConstants.SETTINGS_DATA_SAVE_KEY, settingModel.Data);
            }
        }
    }
}
