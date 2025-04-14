using Settings.Models;
using Settings.PrivacyPolicy;
using Settings.States;
using Zenject;

namespace Settings.DI
{
    public sealed class SettingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // States
            //
            Container.BindInterfacesTo<SettingsState>().AsSingle();
            
            // Audio 
            //
            Container.Bind<SettingsMusicController>().AsSingle();
            Container.Bind<SettingsSoundsController>().AsSingle();

            // Privacy Policy 
            //
            Container.Bind<SettingsPrivacyPolicyController>().AsSingle();
            
            // Models
            //
            Container.BindInterfacesTo<SettingsModel>().AsSingle();
        }
    }
}
