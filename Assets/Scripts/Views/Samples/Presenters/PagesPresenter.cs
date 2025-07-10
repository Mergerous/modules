using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using R3;

namespace Modules.Views
{
    [UsedImplicitly]
    public sealed class PagesPresenter : Presenter
    {
        private readonly Dictionary<string, PagePresenter> itemPresenters;
        private readonly IPresenterFactory factory;
        
        private ListElement pageListElement;
        private ListElement tabListElement;

        public PagesPresenter(IPresenterFactory factory)
        {
            this.factory = factory;
            itemPresenters = new Dictionary<string, PagePresenter>();
        }

        public void Subscribe(View view)
        {
            base.Subscribe();
            pageListElement = view.GetElement<ListElement>("page_list");
            tabListElement = view.GetElement<ListElement>("tab_list");
        }

        public void AddTab<T>(string tabKey, string pageKey, Action<T> callback = default)
            where T : PagePresenter
        {
            View tabView = tabListElement[tabKey];
            TabPresenter tabPresenter = factory.Create<TabPresenter>();
            tabPresenter.Subscribe(tabView);

            tabPresenter.OnClicked
                .Select(isOn => (isOn, presenter: SelectPage<T>(tabKey, pageKey, isOn)))
                .Where(tuple => tuple.isOn)
                .Subscribe(tuple => callback?.Invoke(tuple.presenter))
                .AddTo(disposables);
        }

        private T SelectPage<T>(string tabKey, string pageKey, bool isSelected)
            where T : PagePresenter
        {
            if (!itemPresenters.TryGetValue(tabKey, out PagePresenter itemPresenter))
            {
                View pageView = pageListElement[pageKey];
                itemPresenter = factory.Create<T>();
                itemPresenter.Subscribe(pageView);
                itemPresenters.Add(tabKey, itemPresenter);
            }
            
            itemPresenter.Select(isSelected);

            return itemPresenter as T;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            factory.Dispose();
            itemPresenters.Clear();
        }
    }
}