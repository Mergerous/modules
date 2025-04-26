using System;
using Modules.States;
using Settings;
using VContainer;
using VContainer.Unity;

namespace Modules.Settings
{
    [Serializable]
    public sealed class SettingsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // States
            //
            builder.Register<IState, SettingsState>(Lifetime.Singleton);
            
            // Views
            //
            builder.Register<SettingsPresenter>(Lifetime.Singleton);
            builder.Register<SettingsButtonPresenter>(Lifetime.Transient);
            builder.Register<SettingsTogglePresenter>(Lifetime.Transient);
            builder.RegisterFactory<SettingsButtonPresenter>(container => container.Resolve<SettingsButtonPresenter>, Lifetime.Singleton);
            builder.RegisterFactory<SettingsTogglePresenter>(container => container.Resolve<SettingsTogglePresenter>, Lifetime.Singleton);
            
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
