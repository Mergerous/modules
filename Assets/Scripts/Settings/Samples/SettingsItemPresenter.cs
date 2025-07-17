using JetBrains.Annotations;
using Modules.Views;
using R3;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsItemPresenter : Presenter
    {
        private readonly SettingsManager settingsManager;
        private readonly CustomTogglePresenter togglePresenter;
        
        public SettingsItemPresenter(SettingsManager settingsManager, 
            CustomTogglePresenter togglePresenter)
        {
            this.settingsManager = settingsManager;
            this.togglePresenter = togglePresenter;
        }

        public void Subscribe(View view, ISettingsItemModel model)
        {
            base.Subscribe();
            SettingsItemState state = SettingsItemState.None;
            
            if (model is IToggleContent toggleContent)
            {
                state |= SettingsItemState.Toggle;
                view.SetState(state);
                togglePresenter.Subscribe(view["toggle_view"]);
                togglePresenter.SetValue(toggleContent.IsEnabled);
                togglePresenter.ValueChangedObservable
                    .Subscribe(isOn => settingsManager.Process(isOn, model))
                    .AddTo(disposables);
            }

            if (model is IButtonContent)
            {
                state |= SettingsItemState.Button;
                view.SetState(state);
                view.GetElement<ButtonElement>("button").ClickObservable
                    .Subscribe(_ => settingsManager.Process(model))
                    .AddTo(disposables);
            }
    
            if (model is ITitleContent textContent)
            {
                state |= SettingsItemState.Title;
                view.SetState(state);
                view.GetElement<TextElement>("title").SetText(textContent.Text);
            }
        }
    }
}