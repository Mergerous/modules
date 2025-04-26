using JetBrains.Annotations;
using Modules.Views;
using R3;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsButtonPresenter : Presenter
    {
        private readonly SettingsManager settingsManager;

        public SettingsButtonPresenter(SettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
        }
        
        public void Subscribe(View view, ISettingsItemModel model)
        {
            base.Subscribe();
            view.GetElement<ButtonElement>("button").ClickObservable
                .Subscribe(_ => settingsManager.Process(model))
                .AddTo(disposables);
        }
    }
}