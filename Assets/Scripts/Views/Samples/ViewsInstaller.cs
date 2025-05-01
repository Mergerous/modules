using System;
using Modules.Views;
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
                .WithParameter(settings)
                .WithParameter(container);

            builder.Register<VisibilityPresenter>(Lifetime.Transient);
            builder.Register<CustomTogglePresenter>(Lifetime.Transient);
            
            builder.RegisterFactory<string, ViewHandle>(container =>
            {
                ViewsManager viewsManager = container.Resolve<ViewsManager>();
                return key => new ViewHandle(key, viewsManager.CreateView, viewsManager.DestroyView);
            }, Lifetime.Singleton);
        }
    }
}
