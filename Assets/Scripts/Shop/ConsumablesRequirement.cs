using System;
using Consumables.Currencies;
using UnityEngine.Serialization;

namespace Shop
{
    [Serializable]
    public sealed class ConsumablesRequirement : IRequirement
    {
        [FormerlySerializedAs("currenciesData")] public CurrencyData currencyData;
    }
}