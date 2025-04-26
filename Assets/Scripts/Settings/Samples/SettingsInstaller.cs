using System;
using Modules.Settings;
using VContainer;
using VContainer.Unity;

namespace Settings.DI
{
    [Serializable]
    public sealed class SettingsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // States
            //
            builder.Register<SettingsState>(Lifetime.Singleton);
            
            // Models
            //
            builder.Register<SettingsModel>(Lifetime.Singleton);
            
            builder.Register<IToggleSettingsProcessor, VibrationsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, MusicSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, SoundsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IButtonSettingsProcessor, PrivacyPolicySettingsProcessor>(Lifetime.Singleton);
        }
    }
}
