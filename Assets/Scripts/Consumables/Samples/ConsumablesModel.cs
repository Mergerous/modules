using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class ConsumablesModel : ICurrenciesContent
    {
        private readonly Dictionary<string, CurrencyModel> currencyModels;
        private readonly ConsumablesConfigSo config;
        
        public ConsumablesData Data { get; }

        public ConsumablesModel(Func<ConsumablesData> dataFactory, ConsumablesConfigSo config)
        {
            this.config = config;
            Data = dataFactory();
            currencyModels = new Dictionary<string, CurrencyModel>();
        }

        private CurrencyModel GetOrCreateModel(CurrencyData data)
        {
            if (currencyModels.TryGetValue(data.key, out CurrencyModel currencyModel) 
                || TryCreateModel(new CurrencyData(data.key, default), out currencyModel))
            {
                return currencyModel;
            }

            return default;
        }
        
        private bool TryCreateModel(CurrencyData currencyData, out CurrencyModel currencyModel)
        {
            int index = Data.currencyData.FindIndex(currency => currency.key == currencyData.key);

            if (index >= 0)
            {
                currencyData = Data.currencyData[index];
            }
            else
            {
                Data.currencyData.Add(currencyData);
            }
            
            if (config.ConsumableConfigs.TryGetValue(currencyData.key, out ConsumableConfig record))
            {
                currencyModel = new CurrencyModel(currencyData, record);
                currencyModels.TryAdd(currencyData.key, currencyModel);
                return true;
            }

            currencyModel = default;
            return false;
        }
        
        public T GetCurrency<T>(string key) where T : class, ICurrencyContent
        {
            return GetOrCreateModel(new CurrencyData(key, default)) as T;
        }
    }
}