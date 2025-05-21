using System;
using System.Collections.Generic;

namespace Consumables.Currencies
{
    [Serializable]
    public sealed class CurrenciesData
    {
        public List<CurrencyData> currencyData = new();

        public CurrenciesData Copy() => new()
        {
            currencyData = new List<CurrencyData>(currencyData),
        };
    }
}