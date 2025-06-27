using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumables.Currencies
{
    public interface ICurrenciesContent<T> where T : ICurrencyContent
    {
        T GetCurrency(string key)
        {
            throw new NotImplementedException();
        }

        Task<T> GetCurrencyAsync(string key, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}