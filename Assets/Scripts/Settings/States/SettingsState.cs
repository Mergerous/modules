using JetBrains.Annotations;
using Modules.Ads;
using Modules.States;
using Modules.Views;

namespace Settings.States
{
    [UsedImplicitly]
    public sealed class SettingsState : IState
    {
        private readonly ViewsManager viewsManager;
        private readonly AdsManager adsManager;
        
        private SettingsViewPresenter Presenter { get; set; }

        public SettingsState(ViewsManager viewsManager, AdsManager adsManager)
        {
            this.viewsManager = viewsManager;;
            this.adsManager = adsManager;
        }

        public void Open()
        {
            View view = viewsManager.CreateView(ViewKeys.SETTINGS_VIEW_KEY);
            Presenter = diManager.Instantiate<SettingsViewPresenter>(view);
            Presenter.Initialize();
            adsManager.ShowBanner();
        }

        public void Close()
        {
            Presenter.Dispose();
            viewManager.DestroyAllViews(ViewKeys.SETTINGS_VIEW_KEY);
            adsManager.HideBanner();
        }
    }
}
