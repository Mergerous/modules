using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public sealed class ShopItemConfig
    {
        [field: SerializeField] public ShopItemData Data { get; private set; }
    }
}