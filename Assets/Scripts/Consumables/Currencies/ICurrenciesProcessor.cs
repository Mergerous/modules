namespace Consumables.Currencies
{
    public interface ICurrenciesProcessor
    {
        public ICurrencyContent AddCurrency(string key, int value);
        public bool TryRemoveCurrency(string key, int value);
    }
}