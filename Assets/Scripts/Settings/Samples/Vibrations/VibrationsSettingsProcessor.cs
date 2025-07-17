using JetBrains.Annotations;
using Modules.Data;
using Modules.Vibrations;

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
            if (model is VibrationsSettingsModel vibrationsSettingsModel)
            {
                vibrationsManager.Enable(isOn);
                vibrationsSettingsModel.Data.isEnabled = isOn;
                dataManager.Save(SettingsConstants.SETTINGS_DATA_SAVE_KEY, settingModel.Data);
            }
        }
    }
}