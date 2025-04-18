using System;
using Modules.Data;
using Modules.Debugging;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Consumables
{
    [Serializable]
    public sealed class ConsumablesInstaller : IInstaller
    {
        [SerializeField] private ConsumablesConfigSo config;
        
        public void Install(IContainerBuilder builder)
        {
            // Models
            //
            builder.Register<ICurrenciesContent, ConsumablesModel>(Lifetime.Singleton).AsSelf().WithParameter(config);
            
            // Core
            //
            builder.Register<ICurrencyProcessor, CurrencyManager>(Lifetime.Singleton).AsSelf();
            
            // Debugging
            //
            builder.Register<IDebuggable, CurrencyDebug>(Lifetime.Singleton);
            
            // Data
            //
            builder.RegisterFactory<ConsumablesData>(container => () => container
                .Resolve<DataManager>()
                .Load(ConsumableConstants.CONSUMABLES_DATA_SAVE_KEY, new ConsumablesData()), Lifetime.Singleton);
        }
    }
}