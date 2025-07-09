using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class TabPresenter : Presenter
    {
        private readonly Subject<bool> onClicked = new();
        private CustomTogglePresenter togglePresenter;
        private TextElement textElement;

        public Observable<bool> OnClicked => onClicked;

        public TabPresenter(CustomTogglePresenter togglePresenter)
        {
            this.togglePresenter = togglePresenter;
        }

        public void Subscribe(string text, View view)
        {
            base.Subscribe();
            
            textElement.SetText(text);
            togglePresenter.Subscribe(view);
            togglePresenter.ValueChangedObservable
                .Subscribe(onClicked.OnNext)
                .AddTo(disposables);
            
            textElement = view.GetElement<TextElement>("text");
        }
    }
}