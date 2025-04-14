using JetBrains.Annotations;
using Modules.Views;
using Settings.PrivacyPolicy;
using VContainer.Unity;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SettingsViewPresenter : Presenter
    {
        private const string SOUNDS_TOGGLE_VIEW_KEY = "sounds_toggle_view";
        private const string MUSIC_TOGGLE_VIEW_KEY = "music_toggle_view";
        private const string PRIVACY_POLICY_BUTTON_KEY = "privacy_policy_button";
        
        private readonly SettingsMusicController settingsMusicController;
        private readonly SettingsSoundsController settingsSoundsController;
        private readonly SettingsPrivacyPolicyController settingsPrivacyPolicyController;
        private readonly View view;

        private CustomToggleViewPresenter soundsToggleViewPresenter;
        private CustomToggleViewPresenter musicToggleViewPresenter;

        public SettingsViewPresenter(SettingsMusicController settingsMusicController, 
            SettingsSoundsController settingsSoundsController, 
            SettingsPrivacyPolicyController settingsPrivacyPolicyController, 
            View view)
        {
            this.settingsMusicController = settingsMusicController;
            this.settingsSoundsController = settingsSoundsController;
            this.settingsPrivacyPolicyController = settingsPrivacyPolicyController;
            this.view = view;
        }
        public void Initialize()
        {
            soundsToggleViewPresenter = diManager.Instantiate<CustomToggleViewPresenter>(view[SOUNDS_TOGGLE_VIEW_KEY]);
            soundsToggleViewPresenter.Initialize();
            soundsToggleViewPresenter.SetValue(settingsSoundsController.IsSoundsEnabled);
            soundsToggleViewPresenter.OnValueChanged += SoundsToggle_OnValueChanged;
            
            musicToggleViewPresenter = diManager.Instantiate<CustomToggleViewPresenter>(view[MUSIC_TOGGLE_VIEW_KEY]);
            musicToggleViewPresenter.Initialize();
            musicToggleViewPresenter.SetValue(settingsMusicController.IsMusicEnabled);
            musicToggleViewPresenter.OnValueChanged += MusicToggle_OnValueChanged;
            
            view.GetElement<ButtonElement>(PRIVACY_POLICY_BUTTON_KEY).Subscribe(PrivacyPolicyButton_OnClicked);
            
        }

        public override void Dispose()
        {
            base.Dispose();
            soundsToggleViewPresenter.Dispose();
            soundsToggleViewPresenter.OnValueChanged -= MusicToggle_OnValueChanged;
            
            musicToggleViewPresenter.Dispose();
            musicToggleViewPresenter.OnValueChanged -= MusicToggle_OnValueChanged;
            view.GetElement<ButtonElement>(PRIVACY_POLICY_BUTTON_KEY).Unsubscribe(PrivacyPolicyButton_OnClicked);
        }
        
        private void SoundsToggle_OnValueChanged(bool isOn)
        {
            settingsSoundsController.EnableSounds(isOn);
        }
        
        private void MusicToggle_OnValueChanged(bool isOn)
        {
            settingsMusicController.EnableMusic(isOn);
        }

        private void PrivacyPolicyButton_OnClicked()
        {
            settingsPrivacyPolicyController.OpenPrivacyPolicy();
        }
    }
}
