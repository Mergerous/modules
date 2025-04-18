using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public sealed class ShopItemData
    {
        public ShopTab tab;
        [SerializeReference] public IDescription description;
        [SerializeReference] public IRequirement requirement;
        [SerializeReference] public IGain[] gains;
    }
}