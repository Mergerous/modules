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
            builder.Register<ICurrenciesContent, CurrenciesModel>(Lifetime.Singleton).AsSelf().WithParameter(config);
            
            // Core
            //
            builder.Register<ICurrenciesProcessor, CurrenciesManager>(Lifetime.Singleton).AsSelf();
            
            // Debugging
            //
            builder.Register<IDebuggable, CurrenciesDebug>(Lifetime.Singleton);
            
            // Data
            //
            builder.RegisterFactory<CurrenciesData>(container => () => container
                .Resolve<DataManager>()
                .Load(CurrenciesConstants.CURRENCIES_DATA_SAVE_KEY, new CurrenciesData()), Lifetime.Singleton);
        }
    }
}