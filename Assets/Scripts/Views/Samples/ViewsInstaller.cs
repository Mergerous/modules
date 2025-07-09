using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Modules.Views
{
    [Serializable]
    public sealed class ViewsInstaller : IInstaller
    {
        [SerializeField] private ViewsSettings settings;
        [SerializeField] private ViewsContainer container;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ViewsManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(settings)
                .WithParameter(container);

            builder.Register<PagesPresenter>(Lifetime.Transient);
            builder.Register<TabPresenter>(Lifetime.Transient);
            builder.Register<VisibilityPresenter>(Lifetime.Transient);
            builder.Register<CustomTogglePresenter>(Lifetime.Transient);
            builder.RegisterFactory<CustomTogglePresenter>(container => container.Resolve<CustomTogglePresenter>, Lifetime.Singleton);
            builder.Register<ButtonPresenter>(Lifetime.Transient);
            builder.RegisterFactory<ButtonPresenter>(container => container.Resolve<ButtonPresenter>, Lifetime.Singleton);
            builder.Register<ImagePresenter>(Lifetime.Transient);
            builder.RegisterFactory<ImagePresenter>(container => container.Resolve<ImagePresenter>, Lifetime.Singleton);
            
            builder.Register<IPresenterFactory, PresenterFactory>(Lifetime.Transient);
            
            builder.RegisterFactory<string, ViewHandle>(resolver =>
            {
                IViewFactory viewFactory = resolver.Resolve<IViewFactory>();
                return key => new ViewHandle(key, viewFactory.CreateView, viewFactory.DestroyView);
            }, Lifetime.Singleton);
        }
    }
}
