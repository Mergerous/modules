using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor<in T>
        where T : ICurrencyData
    {
        void AddCurrency(T data)
        {
            throw new NotImplementedException();
        }

        Task AddCurrencyAsync(T data, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        bool TryRemoveCurrency(T data)
        {
            throw new NotImplementedException();
        }

        Task<bool> TryRemoveCurrencyAsync(T data, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}