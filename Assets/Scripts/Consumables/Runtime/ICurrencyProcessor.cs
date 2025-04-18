namespace Consumables
{
    public interface ICurrencyProcessor
    {
        T AddCurrency<T>(string key, int value) where T : class, ICurrencyContent;
        bool TryRemoveCurrency(string key, int value);
    }
}