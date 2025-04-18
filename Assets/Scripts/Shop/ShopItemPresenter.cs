using Modules.Views;
using R3;

namespace Shop
{
    public sealed class ShopItemPresenter : Presenter
    {
        private readonly IShopProcessor shopProcessor;

        public ShopItemPresenter(IShopProcessor shopProcessor)
        {
            this.shopProcessor = shopProcessor;
        }
        
        public void Subscribe(View view, IShopContent shopContent)
        {
            base.Subscribe();
            view.GetElement<ButtonElement>("button").ClickObservable
                .Subscribe(_ => shopProcessor.Buy(shopContent))
                .AddTo(disposables);

            if (shopContent.Description is ShopDescription shopDescription)
            {
                view.GetElement<TextElement>("text").SetText(shopDescription.text);
            }
        }
    }
}