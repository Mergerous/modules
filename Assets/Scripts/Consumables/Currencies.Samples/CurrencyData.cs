using System;

namespace Consumables.Currencies
{
    [Serializable]
    public sealed class CurrencyData : ICurrencyData
    {
        public string Key { get; set; }
        public string key;
        public int value;
        
        public CurrencyData(string key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }
}