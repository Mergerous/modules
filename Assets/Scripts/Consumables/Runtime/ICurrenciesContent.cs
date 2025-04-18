namespace Consumables
{
    public interface ICurrenciesContent
    {
        T GetCurrency<T>(string key) where T : class, ICurrencyContent;
    }
}
