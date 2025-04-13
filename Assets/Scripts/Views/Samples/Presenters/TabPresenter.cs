using System;
using JetBrains.Annotations;
using Modules.UI.Views;
using R3;

namespace Modules.UI
{
    [UsedImplicitly]
    public sealed class TabPresenter : IDisposable
    {
        private readonly Subject<bool> onClicked = new();
        private ToggleElement toggleElement;
        private TextElement textElement;

        public Observable<bool> OnClicked => onClicked;

        public void Initialize(string text, View view)
        {
            textElement.SetText(text);
            toggleElement.Subscribe(Callback);
            
            textElement = view.GetElement<TextElement>("text");
            toggleElement = view.GetElement<ToggleElement>("toggle");
        }
        
        public void Dispose()
        {
            toggleElement.Unsubscribe(Callback);
        }

        private void Callback(bool isOn)
        {
            onClicked.OnNext(isOn);
        }
    }
}