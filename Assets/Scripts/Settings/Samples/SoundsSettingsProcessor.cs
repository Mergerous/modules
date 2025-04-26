using JetBrains.Annotations;
using Modules.Audio;
using Modules.Data;
using Modules.Settings;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SoundsSettingsProcessor : IToggleSettingsProcessor
    {
        private readonly AudioManager audioManager;
        private readonly DataManager dataManager;
        private readonly SettingsModel settingModel;

        public SoundsSettingsProcessor(AudioManager audioManager, DataManager dataManager, SettingsModel settingModel)
        {
            this.audioManager = audioManager;
            this.dataManager = dataManager;
            this.settingModel = settingModel;
        }
        
        public void Process(bool isOn, ISettingsItemModel model)
        {
            if (model is SoundsSettingsModel soundsSettingsModel)
            {
                audioManager.EnableMusic(isOn);
                soundsSettingsModel.Data.isEnabled = isOn;
                dataManager.Save(SettingsConstants.SETTINGS_DATA_SAVE_KEY, settingModel.Data);
            }
        }
    }
}
