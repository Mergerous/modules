using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu]
    public sealed class ShopConfigSo : SerializedScriptableObject
    {
        [SerializeField] private ShopItemConfig[] configs;

        public IEnumerable<ShopItemConfig> Configs => configs;
    }
}