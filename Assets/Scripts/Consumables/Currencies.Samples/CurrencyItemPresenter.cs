using Consumables.Currencies;
using JetBrains.Annotations;
using Modules.States;
using Modules.Views;
using R3;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class CurrencyItemPresenter : Presenter
    {
        private const string ICON_KEY = "icon";
        private const string TEXT_KEY = "text";
        private const string BUTTON_KEY = "store_button";
        // private const string ANCHOR_KEY = "anchor";
        
        private readonly ICurrencyContent consumablesModel;
        private readonly StatesManager statesManager;

        // private readonly AnchorManager anchorManager;
        
        // private AnchorElement anchorElement;
        private View view;
        private CurrencyModel model;

        public CurrencyItemPresenter(StatesManager statesManager)
            // AnchorManager anchorManager)
        {
            this.statesManager = statesManager;
            // this.anchorManager = anchorManager;
            

            // anchorElement = view.GetElement<AnchorElement>(ANCHOR_KEY);
        }

        public void Subscribe(View view, CurrencyModel model)
        {
            base.Subscribe();

            this.view = view;
            this.model = model;

            // view.GetElement<ButtonElement>(BUTTON_KEY).ClickObservable
            //     .Subscribe(_ => statesManager.Open<MapState>())
            //     .AddTo(disposables);
            //
            // view.GetElement<ImageElement>(ICON_KEY).SetSprite(model.Config.icon);

            model.ValueObservable
                .Subscribe(value => view.GetElement<TextElement>(TEXT_KEY).SetText($"${value}"))
                .AddTo(disposables);

            // anchorManager.AddAnchor(string.Format(anchorElement.anchorKey, Key.value), anchorElement.anchor);
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            // anchorManager.RemoveAnchor(anchorElement.anchorKey);
        }
    }
}