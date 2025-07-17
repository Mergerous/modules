using System;
using JetBrains.Annotations;
using Modules.Views;

namespace Modules.Settings
{
    [UsedImplicitly]
    public sealed class SettingsPresenter : Presenter
    {
        private readonly ViewHandle handle;
        private readonly SettingsModel model;
        private readonly Func<SettingsItemPresenter> itemFactory;

        public SettingsPresenter(
            SettingsModel settingsModel,
            Func<SettingsItemPresenter> itemFactory,
            Func<string, ViewHandle> factory)
        {
            handle = factory(SettingsConstants.SETTINGS_VIEW_KEY);
            model = settingsModel;
            this.itemFactory = itemFactory;
        }

        public override void Subscribe()
        {
            base.Subscribe();
            ListElement listElement = handle.View.GetElement<ListElement>("list");
            foreach (ISettingsItemModel itemModel in model.ItemModels)
            {
                View toggleView = listElement.CreateInstance("default");
                SettingsItemPresenter itemPresenter = itemFactory();
                itemPresenter.Subscribe(toggleView, itemModel);
            }
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            handle.Dispose();
        }
    }
}
