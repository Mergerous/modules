using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class CustomTogglePresenter : Presenter
    {
        private readonly ReactiveProperty<bool> value = new();
        private ToggleElement toggleElement;
        public Observable<bool> ValueChangedObservable => value;
        public bool Value => value.Value;
        
        public void Subscribe(View view)
        {
            base.Subscribe();
            toggleElement = view.GetElement<ToggleElement>("toggle");
            toggleElement.IsOnObservable
                .Subscribe(isOn =>
                {
                    view.SetState(isOn ? CustomToggleState.On : CustomToggleState.Off);
                    value.Value = isOn;
                })
                .AddTo(disposables);
        }

        public void SetValue(bool isOn)
        {
            toggleElement.SetValue(isOn);
        }
    }
}
