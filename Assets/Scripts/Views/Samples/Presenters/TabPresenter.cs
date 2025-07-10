using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    public enum TabState
    {
        On,
        Off
    }
    
    [UsedImplicitly]
    public sealed class TabPresenter : Presenter
    {
        private readonly Subject<bool> onClicked = new();
        private CustomTogglePresenter togglePresenter;

        public Observable<bool> OnClicked => onClicked;

        public TabPresenter(CustomTogglePresenter togglePresenter)
        {
            this.togglePresenter = togglePresenter;
        }

        public void Subscribe(View view)
        {
            base.Subscribe();
            
            togglePresenter.Subscribe(view);
            togglePresenter.ValueChangedObservable
                .Do(isOn => view.SetState(isOn ? TabState.On : TabState.Off))
                .Subscribe(onClicked.OnNext)
                .AddTo(disposables);
        }
    }
}