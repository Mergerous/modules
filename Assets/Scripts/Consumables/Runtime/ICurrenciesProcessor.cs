using System.Threading;
using System.Threading.Tasks;

namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor
    {
        public void AddCurrency(string key, int value);

        public bool TryRemoveCurrency(string key, int value)
        {
            return default;
        }

        public async Task<bool> TryRemoveCurrencyAsync(string key, int value, CancellationToken token)
        {
            await Task.Yield();
            return default;
        }
    }
}