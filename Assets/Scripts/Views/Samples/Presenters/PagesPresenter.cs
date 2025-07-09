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
        private readonly ListElement pageListElement;
        private readonly ListElement tabListElement;
        private readonly IPresenterFactory factory;

        public PagesPresenter(View view, IPresenterFactory factory)
        {
            this.factory = factory;
            itemPresenters = new Dictionary<string, PagePresenter>();
            pageListElement = view.GetElement<ListElement>("page_list");
            tabListElement = view.GetElement<ListElement>("tab_list");
        }

        public void AddTab<T>(string tabKey, Action<T> callback = default, string pageTemplateKey = "default")
            where T : PagePresenter
        {
            View tabView = tabListElement.CreateInstance("default");
            TabPresenter tabPresenter = factory.Create<TabPresenter>();
            tabPresenter.Subscribe(tabKey, tabView);

            tabPresenter.OnClicked
                .Select(isOn => (isOn, presenter: SelectPage<T>(tabKey, isOn, pageTemplateKey)))
                .Where(tuple => tuple.isOn)
                .Subscribe(tuple => callback?.Invoke(tuple.presenter))
                .AddTo(disposables);
        }

        private T SelectPage<T>(string pageKey, bool isSelected, string templateKey = "default")
            where T : PagePresenter
        {
            if (!itemPresenters.TryGetValue(pageKey, out PagePresenter itemPresenter))
            {
                View pageView = pageListElement.CreateInstance(templateKey);
                itemPresenter = factory.Create<T>();
                itemPresenter.Subscribe(pageView);
                itemPresenters.Add(pageKey, itemPresenter);
            }
            
            itemPresenter.Select(isSelected);

            return itemPresenter as T;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            itemPresenters.Clear();
        }
    }
}