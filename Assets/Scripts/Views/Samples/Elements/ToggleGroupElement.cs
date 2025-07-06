using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Serializable]
    public sealed class ToggleGroupElement : Element
    {
        [field: SerializeField] public ToggleGroup Group { get; private set; }
    }
}
