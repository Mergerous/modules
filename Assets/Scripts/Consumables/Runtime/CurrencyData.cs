using System;

namespace Consumables
{
    [Serializable]
    public sealed class CurrencyData
    {
        public string key;
        public int value;
        
        public CurrencyData(string key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }
}