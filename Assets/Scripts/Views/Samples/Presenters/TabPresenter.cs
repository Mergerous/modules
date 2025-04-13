using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class TabPresenter : Presenter
    {
        private readonly Subject<bool> onClicked = new();
        private ToggleElement toggleElement;
        private TextElement textElement;

        public Observable<bool> OnClicked => onClicked;

        public void Initialize(string text, View view)
        {
            textElement.SetText(text);
            toggleElement.IsOnObservable
                .Subscribe(onClicked.OnNext)
                .AddTo(disposables);
            
            textElement = view.GetElement<TextElement>("text");
            toggleElement = view.GetElement<ToggleElement>("toggle");
        }
    }
}