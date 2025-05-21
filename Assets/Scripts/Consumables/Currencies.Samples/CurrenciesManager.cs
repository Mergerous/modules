using JetBrains.Annotations;
using Modules.Data;

namespace Consumables.Currencies
{
    [UsedImplicitly]
    public sealed class CurrenciesManager : ICurrenciesProcessor
    {
        private readonly CurrenciesModel currenciesModel;
        private readonly DataManager dataManager;

        public CurrenciesManager(CurrenciesModel currenciesModel, DataManager dataManager)
        {
            this.currenciesModel = currenciesModel;
            this.dataManager = dataManager;
        }

        public ICurrencyContent AddCurrency(string key, int value)
        {
            CurrencyModel content = currenciesModel.GetCurrency(key);
            content.Value += value;
            Save();
            return content;
        }

        public bool TryRemoveCurrency(string key, int value)
        {
            if (HasCurrency(key, value))
            {
                CurrencyModel content = currenciesModel.GetCurrency(key);
                content.Value -= value;
                Save();
                return true;
            }

            return false;
        }

        public bool HasCurrency(string key, int value)
        {
            CurrencyModel content = currenciesModel.GetCurrency(key);

            return content.Value >= value;
        }

        private void Save()
        {
            dataManager.Save(CurrenciesConstants.CURRENCIES_DATA_SAVE_KEY, currenciesModel.Data);
        }
    }
}
