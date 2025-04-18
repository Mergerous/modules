using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Consumables
{
    [CreateAssetMenu]
    public sealed class ConsumablesConfigSo : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, ConsumableConfig> ConsumableConfigs { get; private set; }
    }
}
