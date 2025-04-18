using System;
using Consumables;

namespace Shop
{
    [Serializable]
    public sealed class ConsumablesRequirement : IRequirement
    {
        public CurrencyData currencyData;
    }
}