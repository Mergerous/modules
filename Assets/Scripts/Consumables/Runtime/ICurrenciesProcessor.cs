using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor
    {
        void AddCurrency(string key, int value)
        {
            throw new NotImplementedException();
        }

        Task AddCurrencyAsync(string key, int value, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        bool TryRemoveCurrency(string key, int value)
        {
            throw new NotImplementedException();
        }

        Task<bool> TryRemoveCurrencyAsync(string key, int value, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}