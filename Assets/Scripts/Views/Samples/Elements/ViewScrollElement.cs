using System;
using Modules.Views;
using UnityEngine;

namespace Samples.Scroll
{
    [Serializable]
    public sealed class ViewScrollElement : Element
    {
        [field: SerializeField] public CustomScroll Scroll { get; private set; }
    }
}
