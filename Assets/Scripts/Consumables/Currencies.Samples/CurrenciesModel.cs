using System.Collections.Generic;
using JetBrains.Annotations;

namespace Consumables.Currencies
{
    [UsedImplicitly]
    public sealed class CurrenciesModel : ICurrenciesContent<CurrencyModel>
    {
        private readonly Dictionary<string, CurrencyModel> currencyModels;
        private readonly CurrenciesConfigSo config;
        public readonly CurrenciesData data;

        public CurrenciesModel(CurrenciesData data, CurrenciesConfigSo config)
        {
            this.config = config;
            this.data = data;
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
            int index = data.currencyData.FindIndex(currency => currency.key == currencyData.key);

            if (index >= 0)
            {
                currencyData = data.currencyData[index];
            }
            else
            {
                data.currencyData.Add(currencyData);
            }
            
            if (this.config.Configs.TryGetValue(currencyData.key, out CurrencyConfig config))
            {
                currencyModel = new CurrencyModel(currencyData, config);
                currencyModels.TryAdd(currencyData.key, currencyModel);
                return true;
            }

            currencyModel = default;
            return false;
        }
        
        public CurrencyModel GetCurrency(string key)
        {
            return GetOrCreateModel(new CurrencyData(key, default));
        }
    }
}