namespace Consumables.Currencies
{
    public interface ICurrenciesContent<out T> where T : ICurrencyContent
    {
        T GetCurrency(string key);
    }
}