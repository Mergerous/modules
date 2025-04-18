using JetBrains.Annotations;
using Modules.Data;

namespace Consumables
{
    [UsedImplicitly]
    public sealed class CurrencyManager : ICurrencyProcessor
    {
        private readonly ConsumablesModel consumablesModel;
        private readonly DataManager dataManager;

        public CurrencyManager(ConsumablesModel consumablesModel, DataManager dataManager)
        {
            this.consumablesModel = consumablesModel;
            this.dataManager = dataManager;
        }

        public T AddCurrency<T>(string key, int value) where T : class, ICurrencyContent
        {
            ICurrencyContent content = consumablesModel.GetCurrency<ICurrencyContent>(key);
            content.Value += value;
            Save();
            return content as T;
        }

        public bool TryRemoveCurrency(string key, int value)
        {
            if (HasCurrency(key, value))
            {
                ICurrencyContent content = consumablesModel.GetCurrency<ICurrencyContent>(key);
                content.Value -= value;
                Save();
                return true;
            }

            return false;
        }

        public bool HasCurrency(string key, int value)
        {
            ICurrencyContent content = consumablesModel.GetCurrency<ICurrencyContent>(key);

            return content.Value >= value;
        }

        private void Save()
        {
            dataManager.Save(ConsumableConstants.CONSUMABLES_DATA_SAVE_KEY, consumablesModel.Data);
        }
    }
}
