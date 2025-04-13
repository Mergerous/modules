using System;
using Modules.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Views.Samples
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
        }
    }
}
