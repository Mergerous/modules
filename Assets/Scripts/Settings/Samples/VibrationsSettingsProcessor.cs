using JetBrains.Annotations;
using Modules.Data;
using Modules.Settings;
using Modules.Vibrations;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class VibrationsSettingsProcessor : IToggleSettingsProcessor
    {
        private readonly DataManager dataManager;
        private readonly VibrationsManager vibrationsManager;
        private readonly SettingsModel settingModel;

        public VibrationsSettingsProcessor(DataManager dataManager, VibrationsManager vibrationsManager, SettingsModel settingModel)
        {
            this.dataManager = dataManager;
            this.vibrationsManager = vibrationsManager;
            this.settingModel = settingModel;
        }

        public void Process(bool isOn, ISettingsItemModel model)
        {
            if (model is MusicSettingsItemModel musicSettingsItemModel)
            {
                vibrationsManager.Enable(isOn);
                musicSettingsItemModel.Data.isEnabled = isOn;
                dataManager.Save(SettingsConstants.SETTINGS_DATA_SAVE_KEY, settingModel.Data);
            }
        }
    }
}