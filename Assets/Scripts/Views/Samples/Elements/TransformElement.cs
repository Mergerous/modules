using System;
using UnityEngine;

namespace Modules.Views
{
    [Serializable]
    public sealed class TransformElement : Element
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}
