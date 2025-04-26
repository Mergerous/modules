using JetBrains.Annotations;
using Modules.Views;
using R3;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsTogglePresenter : Presenter
    {
        private readonly SettingsManager settingsManager;
        private readonly CustomTogglePresenter togglePresenter;
        
        public SettingsTogglePresenter(SettingsManager settingsManager, 
            CustomTogglePresenter togglePresenter)
        {
            this.settingsManager = settingsManager;
            this.togglePresenter = togglePresenter;
        }

        public void Subscribe(View view, bool isOn, ISettingsItemModel model)
        {
            base.Subscribe();
            
            togglePresenter.Subscribe(view["toggle_view"]);
            togglePresenter.SetValue(isOn);
            togglePresenter.ValueChangedObservable
                .Subscribe(isOn =>
                {
                    settingsManager.Process(isOn, model);
                })
                .AddTo(disposables);
        }
    }
}