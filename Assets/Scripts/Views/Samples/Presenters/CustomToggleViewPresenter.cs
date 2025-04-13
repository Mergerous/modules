using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class CustomToggleViewPresenter : Presenter
    {
        private const string TOGGLE_KEY = "toggle";
        
        private readonly Subject<bool> valueChangedSubject = new();
        private readonly View view;

        public Observable<bool> ValueChangedObservable => valueChangedSubject;

        public CustomToggleViewPresenter(View view)
        {
            this.view = view;
        }

        public override void Subscribe()
        {
            base.Subscribe();
            view.GetElement<ToggleElement>(TOGGLE_KEY).IsOnObservable
                .Subscribe(isOn =>
                {
                    view.SetState(isOn ? CustomToggleState.On : CustomToggleState.Off);
                    valueChangedSubject.OnNext(isOn);
                })
                .AddTo(disposables);
        }

        public void SetValue(bool isOn)
        {
            view.GetElement<ToggleElement>(TOGGLE_KEY).SetValue(isOn);
        }
    }
}
