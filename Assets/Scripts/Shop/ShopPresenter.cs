using System;
using JetBrains.Annotations;
using Modules.States;
using Modules.Views;
using R3;

namespace Shop
{
    [UsedImplicitly]
    public sealed class ShopPresenter : Presenter
    {
        private readonly StatesManager statesManager;
        private readonly ShopModel model;
        private readonly ViewHandle handle;
        private readonly Func<ShopItemPresenter> itemFactory;

        public ShopPresenter(ShopModel model, StatesManager statesManager, 
            Func<string, ViewHandle> factory, Func<ShopItemPresenter> itemFactory)
        {
            this.model = model;
            this.itemFactory = itemFactory;
            this.statesManager = statesManager;
            handle = factory(ShopConstants.SHOP_VIEW_KEY);
        }

        public override void Subscribe()
        {
            base.Subscribe();
            
            handle.View.GetElement<ButtonElement>("back_button").ClickObservable
                .Subscribe(_ => statesManager.OpenPrevious())
                .AddTo(disposables);
            
            ListElement list = handle.View.GetElement<ListElement>("list");
            
            foreach (IShopContent shopContent in model.ItemModels)
            {
                View itemView = list.CreateInstance("default");
                ShopItemPresenter itemPresenter = itemFactory();
                itemPresenter.Subscribe(itemView, shopContent);
            }
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            handle.Dispose();
        }
    }
}