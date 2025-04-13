using System;
using UnityEngine;

namespace Modules.UI.Views
{
    [Serializable]
    public sealed class CanvasElement : Element
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
    }
}