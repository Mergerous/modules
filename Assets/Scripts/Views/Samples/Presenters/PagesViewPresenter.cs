using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.UI.Views;
using R3;
using Shop.Views;

namespace Modules.UI
{
    [UsedImplicitly]
    public sealed class PagesViewPresenter
    {
        private readonly Dictionary<string, PageViewPresenter> itemPresenters;
        private readonly ListElement pageListElement;
        private readonly ListElement tabListElement;
        private readonly Func<TabPresenter> factory;
        private readonly Func<Type,PageViewPresenter> itemFactory;

        public PagesViewPresenter(View view, Func<TabPresenter> factory, Func<Type,PageViewPresenter> itemFactory)
        {
            this.factory = factory;
            this.itemFactory = itemFactory;
            itemPresenters = new Dictionary<string, PageViewPresenter>();
            pageListElement = view.GetElement<ListElement>("page_list");
            tabListElement = view.GetElement<ListElement>("tab_list");
        }

        public void AddTab<T>(string tabKey, Action<T> callback)
            where T : PageViewPresenter
        {
            View tabView = tabListElement.CreateInstance("default");
            TabPresenter tabPresenter = factory();
            tabPresenter.Initialize(tabKey, tabView);
            
            // TODO ADD DISPOSABLE, MAKE STATIC
            tabPresenter.OnClicked
                .Select(isOn => (isOn, presenter: SelectPage<T>(tabKey, isOn)))
                .Where(tuple => tuple.isOn)
                .Subscribe(tuple => callback?.Invoke(tuple.presenter));
        }

        private T SelectPage<T>(string pageKey, bool isSelected)
            where T : PageViewPresenter
        {
            if (!itemPresenters.TryGetValue(pageKey, out PageViewPresenter itemPresenter))
            {
                View pageView = pageListElement.CreateInstance("default");
                itemPresenter = itemFactory(typeof(T));
                itemPresenter.Initialize(pageView);
                itemPresenters.Add(pageKey, itemPresenter);
            }
            
            itemPresenter.Select(isSelected);

            return itemPresenter as T;
        }
    }
}