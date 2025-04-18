using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public sealed class ShopDescription : IDescription
    {
        [TextArea]
        public string text;
    }
}
