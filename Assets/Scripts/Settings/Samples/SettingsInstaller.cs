using System;
using Modules.Data;
using Modules.States;
using Modules.Views;
using Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Settings
{
    [Serializable]
    public sealed class SettingsInstaller : IInstaller
    {
        [SerializeField] private SettingsRemoteInfo remoteInfo;
        public void Install(IContainerBuilder builder)
        {
            // States
            //
            builder.Register<IState, SettingsState>(Lifetime.Singleton);
            
            // Views
            //
            builder.Register<SettingsPresenter>(Lifetime.Singleton);
            builder.Register<SettingsItemPresenter>(Lifetime.Transient);
            builder.RegisterFactory<SettingsItemPresenter>(container => container.Resolve<SettingsItemPresenter>, Lifetime.Singleton);
            
            // Models
            //
            builder.Register<SettingsModel>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<DataManager>().Load(SettingsConstants.SETTINGS_DATA_SAVE_KEY, new SettingsData()))
                .WithParameter(remoteInfo);

            builder.Register<SettingsManager>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, VibrationsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, MusicSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, SoundsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IButtonSettingsProcessor, PrivacyPolicySettingsProcessor>(Lifetime.Singleton);
        }
    }
}
