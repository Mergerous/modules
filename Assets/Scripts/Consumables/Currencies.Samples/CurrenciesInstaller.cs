using System;
using Modules.Data;
using Modules.Debugging;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Consumables.Currencies
{
    [Serializable]
    public sealed class CurrenciesInstaller : IInstaller
    {
        [SerializeField] private CurrenciesConfigSo config;
        
        public void Install(IContainerBuilder builder)
        {
            // Models
            //
            builder.Register<ICurrenciesContent<CurrencyModel>, CurrenciesModel>(Lifetime.Singleton)
                .WithParameter(config)
                .WithParameter(resolver => resolver.Resolve<DataManager>().Load(CurrenciesConstants.CURRENCIES_DATA_SAVE_KEY, new CurrenciesData()))
                .AsSelf();
            
            // Core
            //
            builder.Register<ICurrenciesProcessor, CurrenciesManager>(Lifetime.Singleton).AsSelf();
            
            // Debugging
            //
            builder.Register<IDebuggable, CurrenciesDebug>(Lifetime.Singleton);
        }
    }
}