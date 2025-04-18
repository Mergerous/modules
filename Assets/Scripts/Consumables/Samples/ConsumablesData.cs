using System;
using System.Collections.Generic;

namespace Consumables
{
    [Serializable]
    public sealed class ConsumablesData
    {
        public List<CurrencyData> currencyData = new();

        public ConsumablesData Copy() => new()
        {
            currencyData = new List<CurrencyData>(currencyData),
        };
    }
}