using System;
using Modules.Data;
using Modules.States;
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
            builder.Register<SettingsButtonPresenter>(Lifetime.Transient);
            builder.Register<SettingsTogglePresenter>(Lifetime.Transient);
            builder.RegisterFactory<SettingsButtonPresenter>(container => container.Resolve<SettingsButtonPresenter>, Lifetime.Singleton);
            builder.RegisterFactory<SettingsTogglePresenter>(container => container.Resolve<SettingsTogglePresenter>, Lifetime.Singleton);
            
            // Models
            //
            builder.Register<SettingsModel>(Lifetime.Singleton)
                .WithParameter(remoteInfo);
            
            // Data
            //
            builder.RegisterFactory<SettingsData>(container =>
            {
                DataManager dataManager = container.Resolve<DataManager>();
                return () => dataManager.Load(SettingsConstants.SETTINGS_DATA_SAVE_KEY, new SettingsData());
            }, Lifetime.Singleton);
            
            builder.Register<IToggleSettingsProcessor, VibrationsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, MusicSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IToggleSettingsProcessor, SoundsSettingsProcessor>(Lifetime.Singleton);
            builder.Register<IButtonSettingsProcessor, PrivacyPolicySettingsProcessor>(Lifetime.Singleton);
        }
    }
}
