using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class ScrollElement : Element
    {
        [field: SerializeField] public Scroll Scroll { get; private set; }
    }
}
