using System;
using Modules.States;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Shop
{
    [Serializable]
    public sealed class ShopInstaller : IInstaller
    {
        [SerializeField] private ShopConfigSo config;
        public void Install(IContainerBuilder builder)
        {
            // Views
            //
            builder.Register<ShopPresenter>(Lifetime.Singleton);
            builder.Register<ShopItemPresenter>(Lifetime.Transient);
            builder.RegisterFactory<ShopItemPresenter>(container => container.Resolve<ShopItemPresenter>, Lifetime.Singleton);

            // States
            //
            builder.Register<IState, ShopState>(Lifetime.Singleton);
            
            // Models
            //
            builder.Register<ShopModel>(Lifetime.Singleton).WithParameter(config);
            
            // Core
            //
            builder.Register<IShopProcessor, ShopManager>(Lifetime.Singleton);
        }
    }
}