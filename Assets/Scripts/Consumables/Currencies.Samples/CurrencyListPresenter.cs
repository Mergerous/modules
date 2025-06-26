using System;
using System.Collections.Generic;
using Consumables.Currencies;
using JetBrains.Annotations;
using Modules.Views;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class CurrencyListPresenter : Presenter
    {
        private readonly List<CurrencyItemPresenter> instances = new();
        private readonly ICurrenciesContent<CurrencyModel> consumablesContent;
        private readonly Func<CurrencyItemPresenter> factory;

        // public CurrencyListPresenter(ICurrenciesContent consumablesContent, Func<CurrencyItemPresenter> factory)
        // {
        //     this.consumablesContent = consumablesContent;
        //     this.factory = factory;
        // }

        public void Subscribe(View view, params string[] keys)
        {
            ListElement list = view.GetElement<ListElement>("list");
            
            foreach (string key in keys)
            {
                // CurrencyModel model = consumablesContent.GetCurrency<CurrencyModel>(key);
                View itemView = list.CreateInstance("default");
                CurrencyItemPresenter presenter = factory();
                
                // presenter.Subscribe(itemView, model);
                instances.Add(presenter);
            }
        }
    }
}
