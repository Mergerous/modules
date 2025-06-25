using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Views
{
    [Serializable]
    public class RawImageElement : Element
    {
        [field: SerializeField] public RawImage Image { get; private set; }
    }
}