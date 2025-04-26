using JetBrains.Annotations;
using Modules.Ads;
using Modules.States;
using Settings;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsState : IState
    {
        private readonly AdsManager adsManager;
        private readonly SettingsPresenter presenter;
        
        public SettingsState(AdsManager adsManager, SettingsPresenter presenter)
        {
            this.adsManager = adsManager;
            this.presenter = presenter;
        }

        public void Open()
        {
            presenter.Subscribe();
            adsManager.ShowBanner();
        }

        public void Close()
        {
            presenter.Unsubscribe();
            adsManager.HideBanner();
        }
    }
}
