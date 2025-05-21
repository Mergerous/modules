using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Consumables.Currencies
{
    [CreateAssetMenu]
    public sealed class CurrenciesConfigSo : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, CurrencyConfig> Configs { get; private set; }
    }
}
