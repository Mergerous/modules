using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class CustomTogglePresenter : Presenter
    {
        private readonly Subject<bool> valueChangedSubject = new();
        private ToggleElement toggleElement;
        public Observable<bool> ValueChangedObservable => valueChangedSubject;
        
        public void Subscribe(View view)
        {
            base.Subscribe();
            toggleElement = view.GetElement<ToggleElement>("toggle");
            toggleElement.IsOnObservable
                .Subscribe(isOn =>
                {
                    view.SetState(isOn ? CustomToggleState.On : CustomToggleState.Off);
                    valueChangedSubject.OnNext(isOn);
                })
                .AddTo(disposables);
        }

        public void SetValue(bool isOn)
        {
            toggleElement.SetValue(isOn);
        }
    }
}
