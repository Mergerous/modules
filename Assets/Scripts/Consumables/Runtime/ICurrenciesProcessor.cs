using System;

namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor
    {
        public void AddCurrency(string key, int value);
        public void TryRemoveCurrency(string key, int value, Action<bool> callback = null);
    }
}