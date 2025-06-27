using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor
    {
        public void AddCurrency(string key, int value)
        {
            throw new NotImplementedException();
        }

        public Task AddCurrencyAsync(string key, int value, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveCurrency(string key, int value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryRemoveCurrencyAsync(string key, int value, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}