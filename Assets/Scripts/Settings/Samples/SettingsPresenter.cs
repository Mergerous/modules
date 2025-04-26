using System;
using JetBrains.Annotations;
using Modules.Settings;
using Modules.Views;

namespace Settings
{
    [UsedImplicitly]
    public sealed class SettingsPresenter : Presenter
    {
        private readonly ViewHandle handle;
        private readonly SettingsModel model;
        private readonly Func<SettingsTogglePresenter> toggleFactory;
        private readonly Func<SettingsButtonPresenter> buttonFactory;

        public SettingsPresenter(
            SettingsModel settingsModel,
            Func<SettingsTogglePresenter> toggleFactory,
            Func<SettingsButtonPresenter> buttonFactory,
            Func<string, ViewHandle> factory)
        {
            handle = factory("settings_view");
            model = settingsModel;
            this.toggleFactory = toggleFactory;
            this.buttonFactory = buttonFactory;
        }

        public override void Subscribe()
        {
            base.Subscribe();
            ListElement listElement = handle.View.GetElement<ListElement>("list");
            foreach (ISettingsItemModel itemModel in model.ItemModels)
            {
                switch (itemModel)
                {
                    case IToggleContent toggleContent:
                        View toggleView = listElement.CreateInstance("toggle");
                        SettingsTogglePresenter togglePresenter = toggleFactory();
                        togglePresenter.Subscribe(toggleView, toggleContent.IsEnabled, itemModel);
                        break;
                    case IButtonContent buttonContent:
                        View buttonView = listElement.CreateInstance("button");
                        SettingsButtonPresenter buttonPresenter = buttonFactory();
                        buttonPresenter.Subscribe(buttonView, itemModel);
                        break;
                }
             
            }
        }
    }
}
