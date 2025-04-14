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

            builder.RegisterFactory(CreateViewFactory, Lifetime.Singleton);
        }
        
        private Func<string, Cache<View>> CreateViewFactory(IObjectResolver container)
        {
            ViewsManager viewsManager = container.Resolve<ViewsManager>();
            return key => new Cache<View>(() => viewsManager.CreateView(key), view => viewsManager.DestroyView(view));
        }
    }
}
