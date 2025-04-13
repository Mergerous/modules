using System;
using JetBrains.Annotations;
using Modules.UI.Views;

namespace Common.Views
{
    [UsedImplicitly]
    public sealed class CustomToggleViewPresenter : IDisposable
    {
        public event Action<bool> OnValueChanged; 
        
        private const string TOGGLE_KEY = "toggle";
        
        private readonly View view;

        public CustomToggleViewPresenter(View view)
        {
            this.view = view;
        }

        public void Initialize()
        {
            view.GetElement<ToggleElement>(TOGGLE_KEY).Subscribe(SetValueInternal);
        }
        
        public void Dispose()
        {
            view.GetElement<ToggleElement>(TOGGLE_KEY).Unsubscribe(SetValueInternal);
        }

        public void SetValue(bool isOn)
        {
            view.GetElement<ToggleElement>(TOGGLE_KEY).SetValue(isOn);
        }

        private void SetValueInternal(bool isOn)
        {
            view.SetState(isOn ? CustomToggleState.On : CustomToggleState.Off);
            OnValueChanged?.Invoke(isOn);
        }
    }
}
